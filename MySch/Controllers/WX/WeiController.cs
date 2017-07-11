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
                            epic.Add("实验初中 - 报名须知", "报名注册须出具以下材料原件：\n　　一、父母（或其法定监护人）身份证；\n　　二、户口簿；\n　　三、住房产权（不动产）证；\n　　四、小学毕业证书（或学籍证明）。\n\n【报名步骤】\n　　选择“学生相关”，点击“报名注册”，按要求填写联系电话、学生身份证号，完成学生注册，显示审核二维码；\n　　然后点击“学生照片”绿色按钮，拍摄一张报名学生清晰的、蓝色背景的照片，上传；\n　　7月11-13日携带报名材料原件到实验初中报名窗口出示二维码审核。", "", "");

                            return epic.ToXml(author);
                        }
                        //菜单点击
                        if (rec.XmlElement("Event") == "CLICK")
                        {
                            if (rec.XmlElement("EventKey") == "reg_about_1")
                            {
                                var epic = new WX_Send_News(rec);
                                epic.Add("实验初中 - 报名须知", "报名注册须出具以下材料原件：\n　　一、父母（或其法定监护人）身份证；\n　　二、户口簿；\n　　三、住房产权（不动产）证；\n　　四、小学毕业证书（或学籍证明）。\n\n【报名步骤】\n　　选择“学生相关”，点击“报名注册”，按要求填写联系电话、学生身份证号，完成学生注册，显示审核二维码；\n　　然后点击“学生照片”绿色按钮，拍摄一张报名学生清晰的、蓝色背景的照片，上传；\n　　7月11-13日携带报名材料原件到实验初中报名窗口出示二维码审核。", "", "");

                                return epic.ToXml(author);
                            }
                        }
                        return "";
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