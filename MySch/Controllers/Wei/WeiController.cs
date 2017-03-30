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

namespace MySch.Controllers.Wei
{
    public class WeiController : Controller
    {
        [HttpGet]
        public ActionResult Index(WX_Author author)
        {
            return MyWxApi.CheckSignature(author) ? Content(author.echostr) : Code(300, 300, "http://weixin.qq.com/r/Q3WpsR7Ej0PwrVrS9yBR");
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
                        string content = rec.XmlElement("Content");

                        //首先将输入的格式规则
                        WX_Command cmd = WX_Command.GetCommand(@"([\u4e00-\u9fa5]+)(.*)", content);

                        //命令行解析出错，给出提示
                        if (cmd == null)
                        {
                            var text = new WX_Send_Text(rec, MyWxApi.NormalCommand());
                            return text.ToXml();
                        }
                        else
                        {
                            //解析命令
                            switch (cmd.Name)
                            {
                                case "新生报名":
                                    //首先检测是否完成登记
                                    if (MyWxApi.Binding(rec.FromUserName))
                                    {
                                        var text = new WX_Send_Text(rec, "已完成学生报名，不必重复动作！");
                                        return text.ToXml();
                                    }
                                    else
                                    {

                                        WX_Command cmd2 = WX_Command.GetCommand(@"[\s#-+]+([\u4e00-\u9fa5]+)[\s#-+]+([\dxX]+)", cmd.Value);
                                        //命令行解析：出错，给出提示
                                        if (cmd2 == null)
                                        {
                                            var text = new WX_Send_Text(rec, "新生报名格式：新生报名#学生姓名#身份证号");
                                            return text.ToXml();
                                        }
                                        else
                                        {
                                            //命令行解析：正确，报名
                                            string gd = MyWxApi.StudReg(cmd2.Name, cmd2.Value, rec.FromUserName);

                                            //正确：返回二维码
                                            var pic = new WX_Send_Pic(rec);
                                            pic.Add("报名信息", "学生报名信息已记录，请按公示时间携带相关证件到指定地点审核！", "http://58.222.0.150/wei/code?gd=" + gd, "");
                                            return pic.ToXml();
                                        }
                                    }
                                case "信息登记":
                                    //首先检测是否完成登记
                                    if (MyWxApi.Binding(rec.FromUserName))
                                    {
                                        var text = new WX_Send_Text(rec, "已完成学生信息登记，不必重复动作！");
                                        return text.ToXml();
                                    }
                                    else
                                    {
                                        WX_Command cmd3 = WX_Command.GetCommand(@"[\s#-+]+([\u4e00-\u9fa5]+)[\s#-+]+([\dxX]+)", cmd.Value);
                                        //命令行解析：出错，给出提示
                                        if (cmd3 == null)
                                        {
                                            var text = new WX_Send_Text(rec, "信息登记格式：信息登记#学生姓名#身份证号");
                                            return text.ToXml();
                                        }
                                        else
                                        {
                                            //命令行解析：正确，查询学生报名信息表
                                            MyWxApi.StudBinding(cmd3.Name, cmd3.Value, rec.FromUserName);

                                            //给出结果显示
                                            var text = new WX_Send_Text(rec, string.Format("已完成 {0} 同学的信息登记，可以进行其它相关查询", cmd3.Name));
                                            return text.ToXml();
                                        }
                                    }
                                case "录取情况":
                                    //首先检测是否完成登记
                                    if (!MyWxApi.Binding(rec.FromUserName))
                                    {
                                        var text = new WX_Send_Text(rec, "未进行：信息登记操作，暂不能查询！");
                                        return text.ToXml();
                                    }
                                    else
                                    {
                                        //录取情况
                                        var text = new WX_Send_Text(rec, MyWxApi.StudInfor(rec.FromUserName));
                                        return text.ToXml();
                                    }
                                case "录取人数":
                                    //首先检测是否完成登记
                                    if (!MyWxApi.Binding(rec.FromUserName))
                                    {
                                        var text = new WX_Send_Text(rec, "未进行：信息登记操作，暂不能查询！");
                                        return text.ToXml();
                                    }
                                    else
                                    {
                                        //录取人数
                                        var text = new WX_Send_Text(rec, MyWxApi.StudCount(rec.FromUserName));
                                        return text.ToXml();
                                    }
                                default:
                                    var dtext = new WX_Send_Text(rec, MyWxApi.NormalCommand());
                                    return dtext.ToXml();
                            }
                        }

                    //图片
                    case "image":
                        var itext = new WX_Send_Text(rec, "图片上传应用还在设计当中！");
                        return itext.ToXml();

                    //事件
                    case "event":
                        //关注类型subscribe
                        if (rec.XmlElement("Event") == "subscribe")
                        {
                            var text = new WX_Send_Text(rec, "欢迎关注：校务在线助手");
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


        public void Code(string gd)
        {
            var db = DataCRUD<TStudReg>.Entity(a => a.ID == gd);
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

            Bitmap bitmap = xing.Write(db.IDS);

            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        }

        public ActionResult Code(int width, int height, string content)
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

            return File(ms.GetBuffer(), "image/jpeg", new Guid().ToString("N"));
        }

    }
}