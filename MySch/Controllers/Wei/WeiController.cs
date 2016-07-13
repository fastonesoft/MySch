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
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ZXing;
using ZXing.Presentation;
using ZXing.QrCode;

namespace MySch.Controllers.Wei
{
    public class WeiController : Controller
    {
        [HttpGet]
        public string Index(WX_Author au)
        {
            return MyWxApi.CheckSignature(au) ? au.echostr : "";
        }

        [HttpPost]
        public string Index(WX_Author_Ex au)
        {
            //检测数据是否来至  微信
            if (!MyWxApi.CheckSignature(au)) return "";

            //检测数据体
            Stream stream = Request.InputStream;
            Byte[] bytes = new Byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            string posts = Encoding.UTF8.GetString(bytes);

            //有问题直接返回
            if (string.IsNullOrEmpty(posts)) return "";

            //每个都记录
            TLog log = new TLog();
            log.CreateTime = DateTime.Now;
            log.GD = Guid.NewGuid().ToString("N");
            log.Value = posts;
            DataADU<TLog>.Add(log);

            //封装请求类
            WX_Rec_Base rec = new WX_Rec_Base();
            rec.XmlInit(posts);
            rec.XmlToObj();

            //结束
            string res = string.Empty;
            switch (rec.MsgType)
            {
                //文本
                case "text":
                    string content = rec.XmlElement("Content");
                    ErrorModel error = MyWxApi.StudReg(content);

                    if (error.error)
                    {
                        //出错：给出提示
                        WX_Send_Text text = new WX_Send_Text(rec);
                        text.Init(error.message);

                        res = text.ToXml();
                    }
                    else
                    {
                        //正确：返回二维码
                        WX_Send_Pic pic = new WX_Send_Pic(rec);
                        pic.Init("报名信息", "姓名和身份证号已完成学籍查询，请到窗口出示二维码", "http://58.222.0.150/wei/code?gd=" + error.message, "");

                        res = pic.ToXml();
                    }
                    break;
                //图片
                case "image":
                    WX_Send_Text image = new WX_Send_Text(rec);
                    image.Init("你输的是：图片");

                    res = image.ToXml();
                    break;
                //事件
                case "event":
                    //关注类型subscribe
                    if (rec.XmlElement("Event") == "subscribe")
                    {
                        WX_Send_Text even = new WX_Send_Text(rec);

                        string msg = string.Empty;
                        msg += "欢迎关注：实验初中报名平台\n";

                        even.Init(msg);
                        res = even.ToXml();
                    }
                    else
                    {
                        res = "";
                    }
                    //if (even.Event == "unsubscribe")
                    //if (even.Event == "SCAN")
                    break;
                default:
                    res = "";
                    break;
            }


            //记录输出
            TLog logr = new TLog();
            logr.CreateTime = DateTime.Now;
            logr.GD = Guid.NewGuid().ToString("N");
            logr.Value = res;
            DataADU<TLog>.Add(logr);


            return res;
        }


        public void Code(string gd)
        {
            var db = DataQuery<TStudReg>.Entity(a => a.GD == gd);
            if (db == null) Response.End();

            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 360,
                Height = 200
            };
            ZXing.BarcodeWriter xing = new ZXing.BarcodeWriter();
            xing.Format = BarcodeFormat.QR_CODE;
            xing.Options = options;

            Bitmap bitmap = xing.Write(db.ID);

            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        }

    }
}