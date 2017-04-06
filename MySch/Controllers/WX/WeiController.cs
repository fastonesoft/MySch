using MySch.Bll.Action;
using MySch.Bll.Func;
using MySch.Bll.Wei;
using MySch.Bll.WX;
using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ZXing;
using ZXing.Presentation;
using ZXing.QrCode;

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
                string posts = WXApi.GetBodyHtml();

                //有问题直接返回
                if (string.IsNullOrEmpty(posts)) return "";

                //记录请求数据
                WXLog.Add(posts);

                //封装请求类
                WX_Rec_Base rec = new WX_Rec_Base(posts, author);
                rec.XmlToObj();

                switch (rec.MsgType)
                {
                    //文本
                    case "text":
                        string content = rec.XmlElement("Content").ToUpper();

                        //首先检测是否完成登记
                        if (AutoXue.Binding(rec.FromUserName))
                        {
                            var text = new WX_Send_Text(rec, "已完成学生报名，不必重复动作！");
                            return text.ToXml(author);
                        }

                        WX_Command cmd2 = WX_Command.GetCommand(@"^\s*(\d{17}[0-9X])\s*$|^(1(3[0-9]|4[57]|5[0-35-9]|7[6-8]|8[0-9])\d{8})$", content);

                        //命令行解析：出错，给出提示
                        if (cmd2 == null)
                        {
                            var text = new WX_Send_Text(rec, "输入：学生身份证号码");
                            return text.ToXml(author);
                        }

                        //命令行解析：正确，报名
                        //string gd = MyWxApi.StudReg(cmd2.Name, cmd2.Value, rec.FromUserName);
                        string gd = cmd2.Name;

                        //正确：返回二维码
                        var pic = new WX_Send_Pic(rec);
                        pic.Add("石亮同学", "　　你的报名信息已记录，请点击“＋”，选择“拍摄”，从正上方清晰地拍摄【毕业证、户口簿、房产证】等原件证照，完善报名信息，然后携带手机到报名窗口出示条形码，审核相关报名资料！", "http://a.jysycz.cn/code?content=" + gd + "&r=" + DateTime.Now.Ticks.ToString(), "");
                        return pic.ToXml(author);

                    //图片
                    case "image":

                    //事件
                    case "event":
                        //关注类型subscribe
                        if (rec.XmlElement("Event") == "subscribe")
                        {
                            var text = new WX_Send_Text(rec, AutoXue.NormalCommand());
                            return text.ToXml(author);                            
                        }
                        else
                        {
                            return "";
                        }
                    //if (even.Event == "unsubscribe")
                    //if (even.Event == "SCAN")
                    default:
                        var ddtext = new WX_Send_Text(rec, AutoXue.NormalCommand());
                        return ddtext.ToXml(author);
                }
            }
            catch
            {
                return "";
            }
        }

        //图文图片
        public void Code(string content)
        {
            XingCode.CodeOutputStream(360, 200, content, 0, BarcodeFormat.CODE_128);
        }

        //我的关注
        public void MyCode()
        {
            XingCode.CodeOutputStream(240, 240, "http://weixin.qq.com/r/Q3WpsR7Ej0PwrVrS9yBR", 0, BarcodeFormat.QR_CODE);
        }




        //测试
        public ActionResult Test()
        {
            var cookies = AutoXue.GetCookies();
            var html = AutoXue.GetStudentHtml("321284200508150254", cookies);
            return Content(html);
        }

    }
}