using MySch.Bll.Func;
using MySch.Bll.WX;
using MySch.Bll.WX.Model;
using MySch.Bll.Xue;
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
                WX_Log.Add(posts);

                //封装请求类
                WX_Rec_Base rec = new WX_Rec_Base(posts, author);
                rec.XmlToObj();

                //输入检测
                var input = WX_Command_Rec.GetFromOpenID(rec.FromUserName);


                switch (rec.MsgType.ToLower())
                {
                    //文本
                    case "text":
                        string content = rec.XmlElement("Content").ToUpper();

                        //正则抓取身份证号、手机号
                        WX_Command cmd = WX_Command.GetCommand(@"\s*(?<name>\d{17}[0-9X])\s*|\s*(?<name>1(3[0-9]|4[57]|5[0-35-9]|7[6-8]|8[0-9])\d{8})\s*", content);

                        //命令行解析：出错，给出提示
                        if (cmd == null)
                        {
                            var text = new WX_Send_Text(rec, "只接收【身份证号、手机号码】两种格式");
                            return text.ToXml(author);
                        }

                        if (cmd.Name.Length == 18)
                        {
                            //检查身份证，开始记录
                            var error = AutoXue.RegIDC(cmd.Name, rec.FromUserName);
                            if (error.error)
                            {
                                //返回出错信息
                                var etext = new WX_Send_Text(rec, error.message);
                                return etext.ToXml(author);
                            }

                            //准备回复消息
                            var epic = new WX_Send_News(rec);
                            epic.Add("报名步骤【一】", "", "", "");
                            epic.Add(string.Format("　　{0} 同学，你的身份证已记录，请执行步骤二，输入家长的手机号码", error.message), "", "http://a.jysycz.cn/image?name=wx_yes&r=" + (new Random()).NextDouble().ToString(), "");
                            return epic.ToXml(author);
                        }
                        else
                        {
                            //是电话
                            if (input.IDC == false)
                            {
                                //检查身份证号，提醒输入
                                var epic = new WX_Send_News(rec);
                                epic.Add("报名步骤【一】", "", "", "");
                                epic.Add("　　在最下方输入学生的身份证号，发送", "", "http://a.jysycz.cn/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                                return epic.ToXml(author);
                            }
                            else
                            {
                                //身份证已记录，开始记录电话
                                //如果电话一已记录，记录电话二
                                var error = AutoXue.RegMobil(cmd.Name, rec.FromUserName);
                                if (error.error)
                                {
                                    //返回出错信息
                                    var etext = new WX_Send_Text(rec, error.message);
                                    return etext.ToXml(author);
                                }

                                //准备回复消息
                                var epic = new WX_Send_News(rec);
                                epic.Add("报名步骤【二】", "", "", "");
                                epic.Add(string.Format("　　{0} 同学，你的手机号码已记录，请执行步骤三，从正上方清晰地拍摄报名所需的原件照片上传，不得少于三张", error.message), "", "http://a.jysycz.cn/image?name=wx_yes&r=" + (new Random()).NextDouble().ToString(), "");
                                return epic.ToXml(author);
                            }
                        }
                    //图片
                    case "image":
                        //var media = rec.XmlElement("MediaId");
                        //var image = new WX_Send_Image(rec, media);
                        //return image.ToXml(author);
                        var picurl = rec.XmlElement("PicUrl");
                        var mtext = new WX_Send_Text(rec, picurl);
                        return mtext.ToXml(author);
                    //事件
                    case "event":
                        //关注类型subscribe
                        if (rec.XmlElement("Event") == "subscribe")
                        {
                            var epic = new WX_Send_News(rec);
                            epic.Add("校务在线 - 报名步骤", "", "", "");
                            epic.Add("【一】在最下方输入学生的身份证号，发送", "", "http://a.jysycz.cn/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            epic.Add("【二】然后输入家长的手机号码一个，发送", "", "http://a.jysycz.cn/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            epic.Add("【三】点击右下角〖＋〗，然后选择〖拍摄〗，上传清晰的毕业证、户口簿、房产证等原件照片", "", "http://a.jysycz.cn/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            epic.Add("【四】至少传三张图片，才能显示条形码！有了条码后，请携带相关原件、手机到报名窗口审核", "", "http://a.jysycz.cn/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            return epic.ToXml(author);
                        }
                        else
                        {
                            return "";
                        }
                    //if (even.Event == "unsubscribe")
                    //if (even.Event == "SCAN")
                    default:
                        var dtext = new WX_Send_Text(rec, "现阶段，只能识别文本、图片两种格式");
                        return dtext.ToXml(author);
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

        //我的图片
        public void Image(string name)
        {
            var fileName = "~/Images/" + name + ".jpg";
            XingCode.CodeOutputStream(fileName);
        }


        //测试
        public ActionResult Test()
        {
            var cookies = AutoXue.GetCookies();
            var html = AutoXue.GetStudentHtml("321284200508150254", cookies);
            return Content(html);
        }

        public ActionResult Ht()
        {
            return View();
        }

    }
}