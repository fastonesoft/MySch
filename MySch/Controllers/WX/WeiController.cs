using MySch.Bll;
using MySch.Bll.Func;
using MySch.Bll.Log;
using MySch.Bll.WX;
using MySch.Bll.WX.Model;
using MySch.Bll.Xue;
using MySch.Helper;
using MySch.Models;
using System;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace MySch.Controllers.WX
{
    public class WeiController : Controller
    {
        [HttpGet]
        public ActionResult Index(WX_Author author)
        {
            if (WXApi.CheckSignature(author))
            {
                return Content(author.echostr);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public string Index(WX_Author_Ex author)
        {
            try
            {
                //检测数据是否来至
                if (!WXApi.CheckSignature(author)) return "";
                //检测数据体
                string posts = WXApi.GetMessage();
                //有问题直接返回
                if (string.IsNullOrEmpty(posts)) return "";

                //封装请求类
                WX_Rec_Base rec = new WX_Rec_Base(posts, author);
                rec.XmlToObj();
                switch (rec.MsgType.ToLower())
                {
                    //文本
                    case "text":
                        return "";
                    //图片
                    case "image":
                        return "";
                    //事件
                    case "event":
                        //关注类型subscribe
                        if (rec.XmlElement("Event") == "subscribe")
                        {
                            var epic = new WX_Send_News(rec);
                            epic.Add("实验初中 - 新生报名", "", "", "");

                            return epic.ToXml(author);
                        }
                        else
                        {
                            return "";
                        }
                    //if (even.Event == "unsubscribe")
                    //if (even.Event == "SCAN")
                    default:
                        return "";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}