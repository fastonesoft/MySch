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

namespace MySch.Controllers.User
{
    public class StudRegController : RoleController
    {
        //学生注册首页
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        //public void GetImage()
        ////using (HttpWebResponse resp = MyHtml.GetResponse("http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp"))
        //{
        //    Response.ContentType = "image/jpeg";
        //    Response.Clear();
        //    Response.BufferOutput = true;


        //    using (HttpWebResponse resp = MyHtml.GetResponse("http://jcjy.etec.edu.cn/studman2/genImageCode"))
        //    {
        //        //string encodingName = resp.ContentEncoding;
        //        //return Content(MyHtml.GetHtml(resp, Encoding.GetEncoding("GBK")));

        //        Bitmap bit = MyHtml.GetBitmap(resp);
        //        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
        //        bit.Dispose();
        //        Response.Flush();
        //    }
        //}

        //[HttpPost]StudRegValid reg
        //public ActionResult PostResult()
        //{
        //    try
        //    {
        //        //提交数据验证不过
        //        if (!ModelState.IsValid) return Json(new ErrorModel { error = true, message = "提交数据有误" });

        //        CookieCollection cookies = null;
        //        //一、做Get请求网页
        //        string url = "http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp";
        //        using (HttpWebResponse resp = MyHtml.GetResponse(url))
        //        {
        //            cookies = resp.Cookies;
        //        }


        //        //二、做验证图片请求
        //        //模板图片读取
        //        Bitmap[] srcBit = new Bitmap[26];
        //        for (int i = 0; i < 26; i++)
        //        {
        //            srcBit[i] = new Bitmap(Server.MapPath(string.Format("~/Images/vbit/{0}.bmp", Convert.ToChar(Convert.ToInt16('a') + i))));
        //        }
        //        //读取图片
        //        Bitmap dest = null;
        //        string valid = string.Empty;
        //        string imageurl = "http://jcjy.etec.edu.cn/studman2/genImageCode?rnd=" + DateTime.Now.Ticks.ToString();
        //        //循环读取图片  直到识别出 5 个字符
        //        do
        //        {
        //            using (HttpWebResponse resp = MyHtml.GetResponse(imageurl, cookies))
        //            {
        //                dest = MyHtml.GetBitmap(resp);
        //            }
        //            valid = MyImageCode.GetValidedCode(dest, srcBit);
        //        } while (valid.Length != 5);


        //        //三、做Post请求提交数据
        //        Random rnd = new Random();
        //        Dictionary<string, string> dicts = new Dictionary<string, string>();
        //        dicts.Add("name", "石彧诚");
        //        dicts.Add("cid", "321284200508150254");
        //        dicts.Add("randomCode", valid);
        //        dicts.Add("v", rnd.NextDouble().ToString());

        //        string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
        //        HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
        //        string html = MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));

        //        Regex reg = new Regex(@"<td>([\u4e00-\u9fa5]+|\d{17}[0-9X]|[A-Z]\d{17}[0-9X])</td>");

        //        MatchCollection matchs = reg.Matches(html);


        //        int count = matchs.Count;
        //        for (int i = 0; i < count; i++)
        //        {
        //           html += matchs[i].Groups[1];
        //        }

        //        return Content(html);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Query(StudQueValid que)
        {
            try
            {
                //提交数据验证不过
                if (!ModelState.IsValid) return Json(new ErrorModel { error = true, message = "提交数据有误" });

                CookieCollection cookies = null;
                //一、做Get请求网页
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
                    if (errorcount > 30) return Json(new ErrorModel { error = true, message = "请确认验证码是否变更" });
                } while (valid.Length != 5);

                //三、准备Post请求数据
                Random rnd = new Random();
                Dictionary<string, string> dicts = new Dictionary<string, string>();
                dicts.Add("name", que.Name);
                dicts.Add("cid", que.ID);
                dicts.Add("randomCode", valid);
                dicts.Add("v", rnd.NextDouble().ToString());
                string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
                //提交请求
                HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
                string html = MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));
                //分析返回数据
                Regex regx = new Regex(@"<td>([\u4e00-\u9fa5]+|\d{17}[0-9X]|[A-Z]\d{17}[0-9X])</td>");
                MatchCollection matchs = regx.Matches(html);

                //如果没有找到数据，则返回提示
                if (matchs.Count == 0) return Json(new ErrorModel { error = true, message = "无学籍记录！检查身份证与姓名是否正确" });
       
                //排除重复身份证号
                string id = matchs[2].Groups[1].ToString();
                var db = DataQuery<TStudReg>.Entity(a => a.ID == id);
                if (db != null) return Json(new ErrorModel { error = true, message = "该身份证号学籍已注册" });

                //根据返回数据 -> 创建学生报名记录
                TStudReg stud = new TStudReg();
                stud.fromSch = matchs[0].Groups[1].ToString();
                stud.Name = matchs[1].Groups[1].ToString();
                stud.ID = id;
                stud.fromGrade = matchs[3].Groups[1].ToString();
                stud.nationID = matchs[4].Groups[1].ToString();
                stud.readState = matchs[5].Groups[1].ToString();
                stud.isProblem = matchs[6].Groups[1].ToString() == "是" ? true : false;
                stud.GD = Guid.NewGuid().ToString("N");
                //
                stud.schChoose = false;
                stud.studNo = null;
                stud.Memo = null;
                //
                stud.Reged = false;

                //添加
                DataADU<TStudReg>.Add(stud);

                //返回给浏览器显示到网格
                return Json(stud);
            }
            catch (Exception e)
            {
                return Json(new ErrorModel { error = true, message = e.Message });
            }
        }

        //根据身份证号查询学生
        [HttpPost]
        public ActionResult Get(string id)
        {
            var db = DataQuery<TStudReg>.Entity(a => a.ID == id);

            return db == null ? Json(new ErrorModel { error = true, message = "查询数据出错" }) : Json(db);
        }

        //学生注册
        [HttpPost]
        public ActionResult Reg(string gd)
        {
            var db = DataQuery<TStudReg>.Entity(a => a.GD == gd);

            if (db == null)
            {
                return Json(new ErrorModel { error = true, message = "查询数据出错" });
            }
            else
            {
                return View(db);
            }
        }

        //编号查询
        [HttpPost]
        public ActionResult Search(string sid)
        {
            var db = DataQuery<TStudReg>.Expression(a => a.studNo.Contains(sid));
            return Json(db);
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
        public ActionResult Manu(StudManuValid manu)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new ErrorModel { error = true, message = "提交数据有误" });

                //检测身份证是否重复
                var db = DataQuery<TStudReg>.Entity(a => a.ID == manu.MID);
                if (db != null) return Json(new ErrorModel { error = true, message = "身份证号已注册" });

                TStudReg stud = new TStudReg();
                stud.fromSch = manu.fromSch;
                stud.Name = manu.MName;
                stud.ID = manu.MID;
                stud.fromGrade = manu.fromGrade;
                stud.nationID = null;
                stud.readState = "手动";
                stud.isProblem = true;
                stud.GD = Guid.NewGuid().ToString("N");
                //
                stud.schChoose = false;
                stud.studNo = null;
                stud.Memo = null;
                //
                stud.Reged = false;

                //添加
                DataADU<TStudReg>.Add(stud);

                //返回给浏览器显示到网格
                return Json(stud);
            }
            catch (Exception e)
            {
                return Json(new ErrorModel { error = true, message = e.Message });
            }

        }
    }
}