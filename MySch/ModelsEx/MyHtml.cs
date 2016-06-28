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
        public static ArrayList GetHtmlData(string postUrl, CookieContainer cookie)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            ArrayList list = new ArrayList();
            request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0";
            request.CookieContainer = cookie;
            request.KeepAlive = true;

            request.CookieContainer = cookie;
            try
            {
                //获取服务器返回的资源   
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        cookie.Add(response.Cookies);
                        //保存Cookies   
                        list.Add(cookie);
                        list.Add(reader.ReadToEnd());
                        list.Add(Guid.NewGuid().ToString());//图片名   
                    }
                }
            }
            catch (WebException ex)
            {
                list.Clear();
                list.Add("发生异常/n/r");
                WebResponse wr = ex.Response;
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add("5");
                list.Add("发生异常：" + ex.Message);
            }
            return list;
        }

        public static bool DowloadCheckImg(string Url, CookieContainer cookCon, string savePath)
        {
            bool bol = true;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            //属性配置   
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            webRequest.MaximumResponseHeadersLength = -1;
            webRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322)";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "GET";
            webRequest.Headers.Add("Accept-Language", "zh-cn");
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            webRequest.KeepAlive = true;
            webRequest.CookieContainer = cookCon;
            try
            {
                //获取服务器返回的资源   
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (Stream sream = webResponse.GetResponseStream())
                    {
                        List<byte> list = new List<byte>();
                        while (true)
                        {
                            int data = sream.ReadByte();
                            if (data == -1)
                                break;
                            list.Add((byte)data);
                        }
                        File.WriteAllBytes(savePath, list.ToArray());
                    }
                }
            }
            catch (WebException ex)
            {
                bol = false;
            }
            catch (Exception ex)
            {
                bol = false;
            }
            return bol;
        }
        public static ArrayList PostData(string postData, string postUrl, CookieContainer cookie)
        {
            ArrayList list = new ArrayList();
            HttpWebRequest request;
            HttpWebResponse response;
            ASCIIEncoding encoding = new ASCIIEncoding();
            request = WebRequest.Create(postUrl) as HttpWebRequest;
            byte[] b = encoding.GetBytes(postData);
            request.UserAgent = "Mozilla/4.0";
            request.Method = "POST";
            request.CookieContainer = cookie;
            request.ContentLength = b.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(b, 0, b.Length);
            }

            try
            {
                //获取服务器返回的资源   
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        if (response.Cookies.Count > 0)
                            cookie.Add(response.Cookies);
                        list.Add(cookie);
                        list.Add(reader.ReadToEnd());
                    }
                }
            }
            catch (WebException wex)
            {
                WebResponse wr = wex.Response;
                using (Stream st = wr.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(st, System.Text.Encoding.Default))
                    {
                        list.Add(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                list.Add("发生异常/n/r" + ex.Message);
            }
            return list;
        }


        //-----------------------------------------------
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

                //req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0";

                return (HttpWebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static HttpWebResponse GetResponse(HttpWebRequest req)
        {
            try
            {
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

        public static string GetHtml(HttpWebResponse resp)
        {
            try
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.Default);
                return sr.ReadToEnd();
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

        public static CookieCollection GetCookies(HttpWebResponse resp)
        {
            try
            {
                return resp.Cookies;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static HttpWebResponse PostResponse(string url, CookieCollection cookies)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(cookies);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            return (HttpWebResponse)req.GetResponse();
        }
    }
}