using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MySch.ModelsEx
{
    public class MyHtml
    {
        //网页抓取分解
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

        public static HttpWebResponse PostResponse(string url, CookieCollection cookies, string data, Encoding encoding)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(cookies);
                req.AllowAutoRedirect = false;
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