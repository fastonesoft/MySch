using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MySch.Helper
{
    public class HtmlHelp
    {
        /// <summary>
        /// 网页抓取
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <returns></returns>
        public static HttpWebResponse GetResponse(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(new CookieCollection());
                req.AllowAutoRedirect = false;
                req.KeepAlive = false;
                req.Method = "GET";
                req.Referer = url;
                req.Timeout = 30000;

                return (HttpWebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static CookieCollection GetCookies(string url)
        {
            try
            {
                return GetResponse(url).Cookies;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 网页抓取
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="cookies">请求Cookies</param>
        /// <returns></returns>
        public static HttpWebResponse GetResponse(string url, CookieCollection cookies)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(cookies);
                req.AllowAutoRedirect = false;
                req.KeepAlive = false;
                req.Method = "GET";
                //req.Referer = url;
                req.Timeout = 30000;

                return (HttpWebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 读取网页图片
        /// </summary>
        /// <param name="resp">请求回应</param>
        /// <returns></returns>
        public static Bitmap GetBitmap(HttpWebResponse resp)
        {
            try
            {
                Stream reads = resp.GetResponseStream();
                return new Bitmap(reads);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Bitmap GetBitmap(string url, CookieCollection cookies)
        {
            try
            {
                var resp = GetResponse(url, cookies);
                return GetBitmap(resp);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 读取网页内容
        /// </summary>
        /// <param name="resp">请求回应</param>
        /// <param name="encoding">网页编码</param>
        /// <returns></returns>
        public static string GetHtml(HttpWebResponse resp, Encoding encoding)
        {
            try
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream(), encoding);
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetHtml(HttpWebResponse resp, string encodingName)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);
                return GetHtml(resp, encoding);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetHtml(string url, CookieCollection cookies, Encoding encoding)
        {
            try
            {
                var resp = GetResponse(url, cookies);

                StreamReader sr = new StreamReader(resp.GetResponseStream(), encoding);
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetHtml(string url, CookieCollection cookies, string encodingName)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);
                return GetHtml(url, cookies, encoding);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static string GetHtml(string url, Encoding encoding)
        {
            try
            {
                var resp = GetResponse(url);

                StreamReader sr = new StreamReader(resp.GetResponseStream(), encoding);
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetHtml(string url, string encodingName)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);
                return GetHtml(url, encoding);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }


        /// 提交回应数据
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="cookies">请求Cookies</param>
        /// <param name="data">提交数据</param>
        /// <param name="encoding">页面编码</param>
        /// <param name="redirect">是否跳转</param>
        /// <returns></returns>
        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, Encoding encoding, bool redirect)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(cookies);
                req.AllowAutoRedirect = redirect;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";

                //准备数据
                byte[] posts = encoding.GetBytes(data);
                req.ContentLength = posts.Length;

                //写入提交
                using (Stream postwriter = req.GetRequestStream())
                {
                    postwriter.Write(posts, 0, posts.Length);
                }

                return (HttpWebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, string encodingName, bool redirect)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);
                return PostResponse(url, cookies, data, encoding, redirect);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, Encoding encoding)
        {
            try
            {
                return PostResponse(url, cookies, data, encoding, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, string encodingName)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);
                return PostResponse(url, cookies, data, encoding);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string PostHtml(string url, CookieCollection cookies, string data, string encodingName)
        {
            try
            {
                HttpWebResponse resp = PostResponse(url, cookies, data, encodingName);
                return GetHtml(resp, encodingName);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public static string PostHtml(string url, CookieCollection cookies, string data, Encoding encoding)
        {
            try
            {
                HttpWebResponse resp = PostResponse(url, cookies, data, encoding);
                return GetHtml(resp, encoding);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public static string PostHtml(string url, CookieCollection cookies,string data, string encodingName, bool redirect)
        {
            try
            {
                HttpWebResponse resp = PostResponse(url, cookies, data, encodingName, redirect);
                return GetHtml(resp, encodingName);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public static string PostHtml(string url, CookieCollection cookies, string data, Encoding encoding, bool redirect)
        {
            try
            {
                HttpWebResponse resp = PostResponse(url, cookies, data, encoding, redirect);
                return GetHtml(resp, encoding);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        /// <summary>
        /// 数据字典 转 提交数据
        /// </summary>
        /// <param name="dicts">数据字典</param>
        /// <param name="encoding">数据编码</param>
        /// <returns></returns>
        public static string DictToPostData(Dictionary<string, string> dicts, Encoding encoding)
        {
            string res = string.Empty;
            foreach (var dict in dicts)
            {
                string encodeValue = HttpUtility.UrlEncode(dict.Value, encoding);
                res += string.Format("{0}={1}&", dict.Key, encodeValue);
            }
            //不空，清除尾部多余“&”
            return res == string.Empty ? string.Empty : res.Substring(0, res.Length - 1);
        }
    }
}