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
                var req = (HttpWebRequest)WebRequest.Create(url);

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
                var req = (HttpWebRequest)WebRequest.Create(url);

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
                var reads = resp.GetResponseStream();
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

        public static string GetHtml(HttpWebResponse resp, string encodingName)
        {
            try
            {
                var encoding = Encoding.GetEncoding(encodingName);
                var sr = new StreamReader(resp.GetResponseStream(), encoding);
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
                var resp = GetResponse(url, cookies);
                var encoding = Encoding.GetEncoding(encodingName);
                var sr = new StreamReader(resp.GetResponseStream(), encoding);
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
                var resp = GetResponse(url);
                var encoding = Encoding.GetEncoding(encodingName);
                var sr = new StreamReader(resp.GetResponseStream(), encoding);
                return sr.ReadToEnd();
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
        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, string encodingName, bool redirect)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);

                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(cookies);
                req.AllowAutoRedirect = redirect;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";

                //准备数据
                var encoding = Encoding.GetEncoding(encodingName);
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

        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, string encodingName)
        {
            try
            {
                return PostResponse(url, cookies, data, encodingName, false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 获取网页文本
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookies"></param>
        /// <param name="data"></param>
        /// <param name="encodingName"></param>
        /// <param name="redirect"></param>
        /// <returns></returns>
        public static string PostHtml(string url, CookieCollection cookies, string data, string encodingName, bool redirect)
        {
            try
            {
                var resp = PostResponse(url, cookies, data, encodingName, redirect);
                return GetHtml(resp, encodingName);
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
                var resp = PostResponse(url, cookies, data, encodingName);
                return GetHtml(resp, encodingName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string PostHtml(string url, string data, string encodingName)
        {
            try
            {
                return PostHtml(url, new CookieCollection(), data, encodingName);
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
        public static string DictToPostData(Dictionary<string, string> dicts,  string  encodingName)
        {
            var res = string.Empty;
            var encoding = Encoding.GetEncoding(encodingName);
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