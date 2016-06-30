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

        [HttpPost]
        public ActionResult AddReg()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddToKen(StudRegValid reg)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        //验证成功，抓取网络数据

        //    }
        //}

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

        [HttpPost]
        public ActionResult PostResult(StudRegValid reg)
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
                //循环读取图片  直到识别出 5 个字符
                do
                {
                    using (HttpWebResponse resp = MyHtml.GetResponse(imageurl, cookies))
                    {
                        dest = MyHtml.GetBitmap(resp);
                    }
                    valid = MyImageCode.GetValidedCode(dest, srcBit);
                } while (valid.Length != 5);


                //三、做Post请求提交数据
                Random rnd = new Random();
                Dictionary<string, string> dicts = new Dictionary<string, string>();
                dicts.Add("name", reg.Name);
                dicts.Add("cid", reg.ID);
                dicts.Add("randomCode", valid);
                dicts.Add("v", rnd.NextDouble().ToString());

                string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
                //HttpWebResponse postresp = MyHtml.GetResponse(url + "?" + postdata, cookies);

                HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
                MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));



                return Content();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult ShowImage()
        {
            return View();
        }
    }
}