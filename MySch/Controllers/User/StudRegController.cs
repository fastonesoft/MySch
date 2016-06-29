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

        public void GetImage()
        //using (HttpWebResponse resp = MyHtml.GetResponse("http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp"))
        {
            Response.ContentType = "image/jpeg";
            Response.Clear();
            Response.BufferOutput = true;


            using (HttpWebResponse resp = MyHtml.GetResponse("http://jcjy.etec.edu.cn/studman2/genImageCode"))
            {
                //string encodingName = resp.ContentEncoding;
                //return Content(MyHtml.GetHtml(resp, Encoding.GetEncoding("GBK")));

                Bitmap bit = MyHtml.GetBitmap(resp);
                bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                bit.Dispose();
                Response.Flush();

            }
        }

        public ActionResult PostResult()
        {
            string url = "http://jcjy.etec.edu.cn/studman2/cidGetInfo.jsp";
            try
            {
                using (HttpWebResponse resp = MyHtml.GetResponse(url))
                {
                    CookieCollection cookies = resp.Cookies;
                }

            }
            catch (Exception e)
            {
                json
            }
        }

        public ActionResult ShowImage()
        {
            return View();
        }
    }
}