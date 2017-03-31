using MySch.Bll.Action;
using MySch.Bll.Wei;
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
            if (MyWxApi.CheckSignature(author))
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
                if (!MyWxApi.CheckSignature(author)) return "";

                //检测数据体
                Stream stream = Request.InputStream;
                Byte[] bytes = new Byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                string posts = Encoding.UTF8.GetString(bytes);

                //有问题直接返回
                if (string.IsNullOrEmpty(posts)) return "";

                //记录请求数据
                WXLog.Add(posts);

                //封装请求类
                WX_Rec_Base rec = new WX_Rec_Base(posts);
                rec.XmlToObj();


                switch (rec.MsgType)
                {
                    //文本
                    case "text":
                        string content = rec.XmlElement("Content").ToUpper();

                        //首先检测是否完成登记
                        if (MyWxApi.Binding(rec.FromUserName))
                        {
                            var text = new WX_Send_Text(rec, "已完成学生报名，不必重复动作！");
                            return text.ToXml();
                        }

                        WX_Command cmd2 = WX_Command.GetCommand(@"^\s*(\d{17}[0-9X])\s*$", content);

                        //命令行解析：出错，给出提示
                        if (cmd2 == null)
                        {
                            var text = new WX_Send_Text(rec, "输入：学生身份证号码");
                            return text.ToXml();
                        }

                        //命令行解析：正确，报名
                        //string gd = MyWxApi.StudReg(cmd2.Name, cmd2.Value, rec.FromUserName);
                        string gd = cmd2.Name;

                        //正确：返回二维码
                        var pic = new WX_Send_Pic(rec);
                        pic.Add("石亮同学", "　　你的报名信息已记录，请点击“＋”，选择“拍摄”，从正上方清晰地拍摄【毕业证、户口簿、房产证】等原件证照，完善报名信息，然后携带手机到报名窗口出示二维码，人工审核！", "http://a.jysycz.cn/code?content=" + gd, "");
                        return pic.ToXml();

                    //图片
                    case "image":

                    //事件
                    case "event":
                        //关注类型subscribe
                        if (rec.XmlElement("Event") == "subscribe")
                        {
                            var text = new WX_Send_Text(rec, MyWxApi.NormalCommand());
                            return text.ToXml();
                        }
                        else
                        {
                            return "";
                        }
                    //if (even.Event == "unsubscribe")
                    //if (even.Event == "SCAN")
                    default:
                        var ddtext = new WX_Send_Text(rec, MyWxApi.NormalCommand());
                        return ddtext.ToXml();
                }
            }
            catch
            {
                return "";
            }
        }


        public void Code(string content)
        {
            CodeCust(360, 200, content);
        }

        public void CodeCust(int width, int height, string content)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
            };
            ZXing.BarcodeWriter xing = new ZXing.BarcodeWriter();
            xing.Format = BarcodeFormat.QR_CODE;
            xing.Options = options;

            Bitmap bitmap = xing.Write(content);

            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        }


        public ActionResult CodeFile(int width, int height, string content)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
            };
            ZXing.BarcodeWriter xing = new ZXing.BarcodeWriter();
            xing.Format = BarcodeFormat.QR_CODE;
            xing.Options = options;

            Bitmap bitmap = xing.Write(content);

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);

            return File(ms.GetBuffer(), "image/jpeg", Guid.NewGuid().ToString("N") + ".jpg");
        }

        public void MyCode()
        {
            CodeCust(300, 300, "http://weixin.qq.com/r/Q3WpsR7Ej0PwrVrS9yBR");
        }




        //测试
        public ActionResult Test()
        {
            var cookies = AutoXue.GetCookies();
            var html = AutoXue.GetStudent("321284200508150254", cookies);
            return Content(html);
        }

    }
}