using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.Model;
using MySch.Bll.Xue.Model;
using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll.Xue
{
    public class AutoXue
    {

        /// <summary>
        /// 学生报名
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="openID"></param>
        public static BllError RegIDC(string idc, string openID)
        {
            try
            {
                //身份证检测
                IDC.Check(idc);

                //检查自身
                if (DataCRUD<Student>.Count(a => a.OpenID == openID && a.IDC == idc) == 1)
                {
                    return new BllError { error = true, message = "已完成学生与帐号的绑定，无需重复操作" };
                }

                //身份证，查询，是否已注册
                if (DataCRUD<Student>.Count(a => a.OpenID != openID && a.IDC == idc) > 0)
                {
                    return new BllError { error = true, message = "身份证号：该身份证号的学生已注册！" };
                }

                //这里条件是，身份证没有记录过的，给出的提示
                if (DataCRUD<Student>.Count(a => a.OpenID == openID) > 0)
                {
                    return new BllError { error = true, message = "注意：一个微信号只能绑定一个身份证" };
                }

                //读取网页数据
                var cookies = GetCookies();
                var html = GetStudentHtml(idc, cookies);

                Regex regx = new Regex(@"<td>([（）\u4e00-\u9fa5]+|\d{17}[0-9X]|\d{4})</td>");
                MatchCollection matchs = regx.Matches(html);

                //有数据，记录
                if (matchs.Count != 0)
                {
                    var reg = new BllStudentReg();
                    reg.Memo = matchs[0].Groups[1].ToString();
                    reg.Name = matchs[1].Groups[1].ToString();
                    reg.IDC = matchs[2].Groups[1].ToString();
                    reg.FromSch = matchs[3].Groups[1].ToString();
                    reg.StepIDS = matchs[6].Groups[1].ToString();

                    //设置添加条件
                    if (reg.Memo == "小学学籍库" && reg.StepIDS == "2011")
                    {
                        reg.ID = Guid.NewGuid().ToString("N");
                        reg.StepIDS = "3212840201201701";
                        reg.AccIDS = "32128402";
                        //绑定用户
                        reg.OpenID = openID;
                        //取最大值，没有，则为0
                        var max = DataCRUD<Student>.Max(a => a.StepIDS == reg.StepIDS, a => a.IDS);
                        int max_ids = string.IsNullOrEmpty(max) ? 0 : int.Parse(max.Replace(reg.StepIDS, ""));
                        //自增
                        reg.IDS = reg.StepIDS + (++max_ids).ToString("D4");

                        reg.ToAdd();
                        return new BllError { error = false, message = reg.Name };
                    }
                    else
                    {
                        return new BllError { error = true, message = "身份证未添加！不是小学应届毕业生。请到窗口咨询！" };
                    }
                }
                else
                {
                    return new BllError { error = true, message = "身份证未添加！省学籍库无记录，请到窗口咨询！" };
                }
            }
            catch (Exception e)
            {
                return new BllError { error = true, message = e.Message };
            }
        }

        public static BllError RegMobil(string mobil, string openID)
        {
            try
            {
                var db = DataCRUD<Student>.Entity(a => a.OpenID == openID);
                if (db == null)
                {
                    return new BllError { error = true, message = "用户与学生信息未绑定，无法添加电话！" };
                }
                else
                {
                    //保存电话
                    db.Mobil1 = string.IsNullOrEmpty(db.Mobil1) ? mobil : db.Mobil1;
                    db.Mobil2 = !string.IsNullOrEmpty(db.Mobil1) && db.Mobil1 != mobil ? mobil : db.Mobil2;
                    DataCRUD<Student>.Update(db);

                    return new BllError { error = false, message = db.Name };
                }
            }
            catch (Exception e)
            {
                return new BllError { error = true, message = e.Message };
            }
        }

        public static BllError RegImage()
        {
            return new BllError { error = true, message = "" };
        }

        /// <summary>
        /// 验证码，自动验证
        /// TODO：
        /// </summary>
        /// <param name="validUrl"></param>
        /// <param name="cookies"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string Valid(string validUrl, CookieCollection cookies, int error)
        {
            try
            {
                //a-z
                Bitmap[] srcBit = new Bitmap[26];
                for (int i = 0; i < 26; i++)
                {
                    srcBit[i] = new Bitmap(HttpContext.Current.Server.MapPath(string.Format("~/Images/vbit/{0}.bmp", Convert.ToChar(Convert.ToInt16('a') + i))));
                }

                //循环读取图片  直到识别出 5 个字符
                int count = 0;
                string valid = string.Empty;
                do
                {
                    Bitmap dest = MyHtml.GetBitmap(validUrl + DateTime.Now.Ticks.ToString(), cookies);
                    valid = MyImageCode.GetValidedCode(dest, srcBit);

                    count++;
                    //异常退出
                    if (count > error) throw new Exception("验证图片识别艰难，请检查是否变化！");
                } while (valid.Length != 5);

                //返回验证码
                return valid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 提交数据登录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookies"></param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public static CookieCollection PostCookies(string url, CookieCollection cookies, string postdata)
        {
            HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));

            return postresp.Cookies;
        }

        /// <summary>
        /// 自动验证
        /// </summary>
        /// <param name="url"></param>
        /// <param name="validUrl"></param>
        /// <param name="postUrl"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static CookieCollection AutoLogin(string url, string validUrl, string postUrl, string name, string pwd)
        {
            try
            {
                //打开登录
                CookieCollection cookies = MyHtml.GetCookies(url);

                //识别验证码
                string code = Valid(validUrl, cookies, 30);

                //整理数据
                Dicts dicts = new Dicts();
                dicts.Add("loginName", name);
                dicts.Add("pwd", pwd);
                dicts.Add("randomCode", code);
                dicts.Add("returnURL", "");
                dicts.Add("appId", "uids");
                dicts.Add("encrypt", "1");
                dicts.Add("reqId", "");
                dicts.Add("req", "");
                var posts = dicts.ToPost("GBK");

                //提交登录
                return PostCookies(postUrl, cookies, posts);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 手动验证
        /// </summary>
        /// <param name="url"></param>
        /// <param name="code"></param>
        /// <param name="postUrl"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static CookieCollection ManaLogin(string url, string code, string postUrl, string name, string pwd)
        {
            try
            {
                //打开登录
                CookieCollection cookies = MyHtml.GetCookies(url);

                //整理数据
                Dicts dicts = new Dicts();
                dicts.Add("loginName", name);
                dicts.Add("pwd", pwd);
                dicts.Add("randomCode", code);
                dicts.Add("returnURL", "");
                dicts.Add("appId", "uids");
                dicts.Add("encrypt", "1");
                dicts.Add("reqId", "");
                dicts.Add("req", "");
                var posts = dicts.ToPost("GBK");

                //提交登录
                return PostCookies(postUrl, cookies, posts);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 后台读取登录Cookies
        /// </summary>
        /// <returns></returns>
        public static CookieCollection GetCookies()
        {
            try
            {
                //检测是否存在记录
                CookieCollection xuecookies = (CookieCollection)HttpContext.Current.Application["xuecookies"];
                if (xuecookies == null)
                {
                    xuecookies = AutoXue.AutoLogin("http://xjgl.jse.edu.cn/uids/index.jsp",
                        "http://xjgl.jse.edu.cn/uids/genImageCode?rnd=" + DateTime.Now.Ticks.ToString(),
                        "http://xjgl.jse.edu.cn/uids/login.jsp", "c32128441402", "==QTuhWMaVlWoN2MSFXYR1TP");

                    //保存
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["xuecookies"] = xuecookies;
                    HttpContext.Current.Application.UnLock();
                }
                else
                {
                    //检测是否可以连接
                    var html = MyHtml.GetHtml("http://xjgl.jse.edu.cn/studman2/studman/studentBrowseAct!queryStudent.action", xuecookies, Encoding.GetEncoding("GBK"));

                    //如果过期，重新连接
                    if (html.Contains("没有权限"))
                    {
                        xuecookies = AutoXue.AutoLogin("http://xjgl.jse.edu.cn/uids/index.jsp",
                            "http://xjgl.jse.edu.cn/uids/genImageCode?rnd=" + DateTime.Now.Ticks.ToString(),
                            "http://xjgl.jse.edu.cn/uids/login.jsp", "c32128441402", "==QTuhWMaVlWoN2MSFXYR1TP");

                        //保存
                        HttpContext.Current.Application.Lock();
                        HttpContext.Current.Application["xuecookies"] = xuecookies;
                        HttpContext.Current.Application.UnLock();
                    }
                }

                return xuecookies;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetStudentHtml(string idc, CookieCollection cookies)
        {
            try
            {
                var url = string.Format("http://xjgl.jse.edu.cn/studman2/studman/studentBrowseAct!queryStudent.action?studentForm.cid={0}", idc);
                return MyHtml.GetHtml(url, cookies, Encoding.GetEncoding("GBK"));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetStudentHtml(string name, string idc, CookieCollection cookies)
        {
            try
            {
                name = HttpUtility.UrlEncode(name, Encoding.GetEncoding("GBK"));
                var jsonurl = string.Format("http://58.213.155.172/studman2/studman/historyAct-getHistoryInfo.action?studName={0}&cid={1}", name, idc);
                return MyHtml.GetHtml(jsonurl, cookies, Encoding.GetEncoding("GBK"));
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        /// <summary>
        /// 自动报名：以查找省网学籍方式，记录学生信息
        /// </summary>
        /// <param name="ID">身份证号</param>
        /// <param name="Name">学生姓名</param>
        /// <param name="openID">ID</param>
        /// <returns></returns>
        public static string StudReg(string Name, string ID, string openID)
        {
            try
            {
                //检测身份证号是否有效
                IDC.Check(ID);

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
                    srcBit[i] = new Bitmap(HttpContext.Current.Server.MapPath(string.Format("~/Images/vbit/{0}.bmp", Convert.ToChar(Convert.ToInt16('a') + i))));
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
                    if (errorcount > 30) throw new Exception("请确认验证码是否变更");
                } while (valid.Length != 5);

                //三、准备Post请求数据
                Random rnd = new Random();
                Dictionary<string, string> dicts = new Dictionary<string, string>();
                dicts.Add("name", Name);
                dicts.Add("cid", ID);
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
                if (matchs.Count == 0) throw new Exception("无学籍记录！检查身份证与姓名是否正确");
                //排除重复身份证号
                string id = matchs[2].Groups[1].ToString();
                var db = DataCRUD<Student>.Entity(a => a.IDS == id);
                if (db != null) throw new Exception("该身份证号学籍已注册");
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
                //
                stud.OpenID = openID;

                //添加
                DataCRUD<Student>.Add(stud);
                //返回
                return stud.ID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}