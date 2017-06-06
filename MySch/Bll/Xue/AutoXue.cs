using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.Model;
using MySch.Bll.Xue.Model;
using MySch.Dal;
using MySch.Helper;
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
        public static string RegStudent(string idc, string mobil1, string reguid)
        {
            try
            {
                //读取网页数据
                var cookies = GetCookies();
                var html = GetStudentHtml(idc, cookies);

                Regex regx = new Regex(@"<td>([（）\u4e00-\u9fa5]+|\d{17}[\dxX]|\d{4})</td>");
                MatchCollection matchs = regx.Matches(html);

                //无学生记录
                if (matchs.Count == 0) throw new Exception("省学籍库无记录，请检查身份证");

                var reg = new BllStudentReg();
                reg.Memo = matchs[0].Groups[1].ToString();
                reg.Name = matchs[1].Groups[1].ToString();
                reg.IDC = matchs[2].Groups[1].ToString();
                reg.FromSch = matchs[3].Groups[1].ToString();
                reg.StepIDS = matchs[6].Groups[1].ToString();

                //设置添加条件
                if (reg.Memo != "小学学籍库" && reg.StepIDS != "2011") throw new Exception("不是小学应届毕业生，无法报名");

                if (DataCRUD<Student>.Count(a => a.IDC == idc) > 0) throw new Exception("该身份证号的学生已注册");
                if (DataCRUD<Student>.Count(a => a.RegUID == reguid) > 0) throw new Exception("一个微信号只能绑定一个身份证");

                //添加
                reg.ID = Guid.NewGuid().ToString("N");
                reg.StepIDS = "3212840201201701";
                reg.AccIDS = "32128402";
                reg.RegUID = reguid;
                //取最大值，没有，则为0
                var max = DataCRUD<Student>.Max(a => a.StepIDS == reg.StepIDS, a => a.IDS);
                int max_ids = string.IsNullOrEmpty(max) ? 0 : int.Parse(max.Replace(reg.StepIDS, ""));
                reg.IDS = reg.StepIDS + (++max_ids).ToString("D4");
                reg.Mobil1 = mobil1;
                reg.ToAdd();
                return reg.Name;
            }
            catch (Exception e)
            {
                throw e;
            }
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
                    Bitmap dest = HtmlHelp.GetBitmap(validUrl + DateTime.Now.Ticks.ToString(), cookies);
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
            HttpWebResponse postresp = HtmlHelp.PostResponse(url, cookies, postdata, "GBK");

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
                CookieCollection cookies = HtmlHelp.GetCookies(url);

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
                CookieCollection cookies = HtmlHelp.GetCookies(url);

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
                    var html = HtmlHelp.GetHtml("http://xjgl.jse.edu.cn/studman2/studman/studentBrowseAct!queryStudent.action", xuecookies, "GBK");

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
                return HtmlHelp.GetHtml(url, cookies, "GBK");
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
                return HtmlHelp.GetHtml(jsonurl, cookies, "GBK");
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
        public static string StudReg11(string Name, string ID, string openID)
        {
            try
            {
                //检测身份证号是否有效
                IDC.Check(ID);

                CookieCollection cookies = null;
                //一、做Get请求网页
                string url = "http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp";
                using (HttpWebResponse resp = HtmlHelp.GetResponse(url))
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
                    using (HttpWebResponse resp = HtmlHelp.GetResponse(imageurl, cookies))
                    {
                        dest = HtmlHelp.GetBitmap(resp);
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
                string postdata = HtmlHelp.DictToPostData(dicts, "GBK");
                //提交请求
                HttpWebResponse postresp = HtmlHelp.PostResponse(url, cookies, postdata, "GBK");
                string html = HtmlHelp.GetHtml(postresp, "GBK");
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
                stud.Memo = null;
                //
                stud.Examed = false;
                //
                stud.RegUID = openID;

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