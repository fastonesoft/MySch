using MySch.Bll.Model;
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

namespace MySch.Bll.Action
{
    public class AutoXue
    {

        /// <summary>
        /// 查询方式，获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="validUrl"></param>
        /// <param name="postUrl"></param>
        /// <param name="name"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static XueQuery Query(string url, string validUrl, string name, string ids)
        {
            //打开登录
            CookieCollection cookies = MyHtml.GetCookies(url);

            //识别验证码
            string code = Valid(validUrl, cookies, 30);

            //整理数据
            Dicts dicts = new Dicts();
            dicts.Add("name", name);
            dicts.Add("cid", ids);
            dicts.Add("randomCode", code);
            dicts.Add("v", (new Random()).NextDouble().ToString());

            string html = MyHtml.PostResponse(url, dicts.ToPost("GBK"), "GBK", cookies);

            Regex regx = new Regex(@"<td>([()\u4e00-\u9fa5]+|\d{17}[0-9X]|[A-Z]\d{17}[0-9X])</td>");
            MatchCollection matchs = regx.Matches(html);

            //如果没有找到数据，则返回提示
            if (matchs.Count == 0)
                return null;
            else
            {
                XueQuery stud = new XueQuery();
                stud.Name = matchs[1].Groups[1].ToString();
                stud.FromSch = matchs[0].Groups[1].ToString();
                stud.FromGrade = matchs[3].Groups[1].ToString();
                stud.ReadState = matchs[5].Groups[1].ToString();

                return stud;
            }
        }

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

        public static string Post(string url, CookieCollection cookies, Dictionary<string, string> dicts)
        {
            string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
            HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
            return MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));
        }

        public static string Post(string url, CookieCollection cookies, Dictionary<string, string> dicts, string requrl)
        {
            string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
            HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));

            CookieCollection cook = postresp.Cookies;
            return MyHtml.GetHtml(requrl, cook, Encoding.GetEncoding("GBK"));
        }

        public static CookieCollection PostCookies(string url, CookieCollection cookies, string postdata)
        {
            HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));

            return postresp.Cookies;
        }

        public static CookieCollection Login(string url, string validUrl, string postUrl, string name, string pwd)
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
                dicts.Add("appId", "");
                dicts.Add("encrypt", "1");
                var posts = dicts.ToPost("GBK");

                //提交登录
                return PostCookies(postUrl, cookies, posts);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetStudent(string name, string ids, CookieCollection cookies)
        {
            try
            {
                name = HttpUtility.UrlEncode(name, Encoding.GetEncoding("GBK"));
                var jsonurl = string.Format("http://58.213.155.172/studman2/studman/historyAct-getHistoryInfo.action?studName={0}&cid={1}", name, ids);
                return MyHtml.GetHtml(jsonurl, cookies, Encoding.GetEncoding("GBK"));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}