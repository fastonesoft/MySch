using MySch.Bll;
using MySch.Bll.Func;
using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Stud
{
    public class StudRegController : RoleController
    {
        //学生注册首页
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QueryToken(StudQueValid que)
        {
            try
            {
                //提交数据验证不过
                if (!ModelState.IsValid) return Json(new BllError { error = true, message = "提交数据有误" });

                //身份证号检测
                IDC.Check(que.IDS);

                //一、做Get请求网页
                CookieCollection cookies = null;
                string url = "http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp";
                using (HttpWebResponse resp = MyHtml.GetResponse(url))
                {
                    cookies = resp.Cookies;
                }

                //二、做验证图片请求
                //模板图片读取
                Bitmap[] srcBit = new Bitmap[26];
                for (int i = 0; i < 26; i++)
                {
                    srcBit[i] = new Bitmap(Server.MapPath(string.Format("~/Images/vbit/{0}.bmp", Convert.ToChar(Convert.ToInt16('a') + i))));
                }
                //读取图片
                Bitmap dest = null;
                string valid = string.Empty;
                string imageurl = "http://jcjy.etec.edu.cn/studman2/genImageCode?rnd=" + DateTime.Now.Ticks.ToString();
                //异常循环记录器
                int errorcount = 0;
                //循环读取图片  直到识别出 5 个字符
                do
                {
                    using (HttpWebResponse resp = MyHtml.GetResponse(imageurl, cookies))
                    {
                        dest = MyHtml.GetBitmap(resp);
                    }
                    valid = MyImageCode.GetValidedCode(dest, srcBit);
                    //循环记录
                    errorcount++;
                    //异常退出
                    if (errorcount > 30) return Json(new BllError { error = true, message = "请确认验证码是否变更" });
                } while (valid.Length != 5);

                //三、准备Post请求数据
                Random rnd = new Random();
                Dictionary<string, string> dicts = new Dictionary<string, string>();
                dicts.Add("name", que.Name);
                dicts.Add("cid", que.IDS);
                dicts.Add("randomCode", valid);
                dicts.Add("v", rnd.NextDouble().ToString());
                string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
                //提交请求
                HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
                string html = MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));
                //分析返回数据
                Regex regx = new Regex(@"<td>([()\u4e00-\u9fa5]+|\d{17}[0-9X]|[A-Z]\d{17}[0-9X])</td>");
                MatchCollection matchs = regx.Matches(html);

                //如果没有找到数据，则返回提示
                if (matchs.Count == 0) return Json(new BllError { error = true, message = "无学籍记录！检查身份证与姓名是否正确" });

                //排除重复身份证号
                string id = matchs[2].Groups[1].ToString();
                var db = DataCRUD<Student>.Entity(a => a.IDS == id);
                if (db != null) return Json(new BllError { error = true, message = "该身份证号学籍已注册" });

                //根据返回数据 -> 创建学生报名记录
                Student stud = new Student();
                stud.FromSch = matchs[0].Groups[1].ToString();

                stud.Name = matchs[1].Groups[1].ToString();
                stud.IDS = id;
                stud.ID = Guid.NewGuid().ToString("N");
                //
                stud.SchChoose = false;
                stud.RegNo = null;
                stud.Memo = null;
                //
                stud.Reged = false;

                //添加
                DataCRUD<Student>.Add(stud);

                //返回给浏览器显示到网格
                return Json(EasyUI<Student>.DataGrid(stud));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //根据身份证号查询学生
        [HttpPost]
        public ActionResult Get(string id)
        {
            try
            {
                //身份证检测
                IDC.IDS(id);

                var db = DataCRUD<Student>.Entitys(a => a.IDS == id);

                //返回：easyui datagrid数据格式
                var res = db == null ? null : new { total = db.Count(), rows = db };
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //学生编号
        [HttpPost]
        public ActionResult Edit(string id)
        {
            var db = DataCRUD<Student>.Entity(a => a.ID == id);

            if (db == null)
            {
                return Json(new BllError { error = true, message = "查询数据出错" });
            }
            else
            {
                //已经编号，无需重编
                if (db.RegNo != null) return Json(new BllError { error = true, message = "已经编号，无需重编" });
                //
                var res = new StudEditValid { ID = db.ID, Name = db.Name, RegNo = db.RegNo, Memo = db.Memo, SchChoose = db.SchChoose };
                return View(res);
            }
        }

        //编号提交
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(StudEditValid stud)
        {
            try
            {
                //提交数据验证不过
                if (!ModelState.IsValid) return Json(new BllError { error = true, message = "提交数据有误" });

                //检测编号
                var db = DataCRUD<Student>.Entity(a => a.RegNo == stud.RegNo);
                if (db != null)
                {
                    //不是同一条记录，提示重复
                    if (db.ID != stud.ID) return Json(new BllError { error = true, message = "编号不得重复设置！" });
                }

                //查询
                Student reg = DataCRUD<Student>.Entity(a => a.ID == stud.ID);
                if (reg == null) return Json(new BllError { error = true, message = "查询数据出错" });
                //修改
                reg.RegNo = stud.RegNo;
                reg.Memo = stud.Memo;
                reg.SchChoose = stud.SchChoose;
                //提交
                DataCRUD<Student>.Update(reg);
                //返回
                return Json(reg);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //学生注册
        [HttpPost]
        public ActionResult Reg(string id)
        {
            var db = DataCRUD<Student>.Entity(a => a.ID == id);

            if (db == null)
            {
                return Json(new BllError { error = true, message = "查询数据出错" });
            }
            else
            {
                var res = new StudRegValid { ID = db.ID, Name = db.Name, Mobil1 = db.Mobil1, Mobil2 = db.Mobil2, Name1 = db.Name1, Name2 = db.Name2, Home = db.Home, Birth = db.Birth };
                return View(res);
            }
        }

        //提交注册
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegToken(StudRegValid stud)
        {
            try
            {
                //提交数据验证不过
                if (!ModelState.IsValid) return Json(new BllError { error = true, message = "提交数据有误" });

                //查询
                Student reg = DataCRUD<Student>.Entity(a => a.ID == stud.ID);
                if (reg == null) return Json(new BllError { error = true, message = "查询数据出错" });
                //修改
                reg.Mobil1 = stud.Mobil1;
                reg.Mobil2 = stud.Mobil2;
                reg.Name1 = stud.Name1;
                reg.Name2 = stud.Name2;
                reg.Home = stud.Home;
                reg.Birth = stud.Birth;
                //注册
                reg.Reged = true;
                //提交
                DataCRUD<Student>.Update(reg);
                //返回
                return Json(reg);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


        //编号查询
        [HttpPost]
        public ActionResult Search(string id)
        {

            Regex regx = new Regex(@"^%(\d+)$|^(\d*-?\d*)$|^(\d+)%$");
            Match match = regx.Match(id);
            if (match.Success)
            {
                string left = match.Groups[1].ToString();
                string all = match.Groups[2].ToString();
                string right = match.Groups[3].ToString();

                var db = left.Length > 0 ? DataCRUD<Student>.Entitys(a => a.RegNo.StartsWith(left)).OrderBy(a => a.RegNo) :
                    right.Length > 0 ? DataCRUD<Student>.Entitys(a => a.RegNo.EndsWith(right)).OrderBy(a => a.RegNo) :
                    all.Length > 0 ? DataCRUD<Student>.Entitys(a => a.RegNo.Contains(all)).OrderBy(a => a.RegNo) : null;

                //返回：easyui datagrid数据格式
                var res = db == null ? null : new { total = db.Count(), rows = db };
                return Json(res);
            }
            else
            {
                return Json(new BllError { error = true, message = "查询格式：前缀%*，包含*，后缀*%" });
            }
        }

        //手动添加窗口
        [HttpPost]
        public ActionResult Manu()
        {
            return View();
        }

        //手动添加数据
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManuToken(StudManuValid manu)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new BllError { error = true, message = "提交数据有误" });

                //身份证号检测
                IDC.IDS(manu.IDS);

                //检测身份证是否重复
                var db = DataCRUD<Student>.Entity(a => a.IDS == manu.IDS);
                if (db != null) return Json(new BllError { error = true, message = "身份证号已注册" });

                Student stud = new Student();
                stud.FromSch = manu.FromSch;
                stud.Name = manu.Name;
                stud.IDS = manu.IDS;
                stud.ID = Guid.NewGuid().ToString("N");
                //
                stud.SchChoose = false;
                stud.RegNo = null;
                stud.Memo = null;
                //
                stud.Reged = false;

                //添加
                DataCRUD<Student>.Add(stud);

                //返回给浏览器显示到网格
                return Json(EasyUI<Student>.DataGrid(stud));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Print(IEnumerable<Student> rows)
        {
            ViewBag.StudNo = DataCRUD<TPrint>.Entity(a => a.Name == "No");
            ViewBag.StudName = DataCRUD<TPrint>.Entity(a => a.Name == "Name");
            ViewBag.School = DataCRUD<TPrint>.Entity(a => a.Name == "School");

            return View(rows);
        }

        [HttpPost]
        public ActionResult PrintPos(IEnumerable<TPrint> pos)
        {
            try
            {
                foreach (var d in pos)
                {
                    DataCRUD<TPrint>.Update(d);
                }
                return Json(new BllError { error = false, message = "打印位置已修改，关闭窗口重来！" });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


    }
}