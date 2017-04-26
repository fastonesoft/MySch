﻿using MySch.Bll.Func;
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
                        WX_Command cmd = WX_Command.GetCommand(@"^(?<name>\d{17}[0-9X])$|^(?<name>1(3[0-9]|4[57]|5[0-35-9]|7[6-8]|8[0-9])\d{8})$", content);

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
                            epic.Add(string.Format("　　{0}，你的身份证号已保存，请执行步骤二，输入家长的手机号码", error.message), "", "http://a.jysycz.cn/image?name=wx_yes&r=" + (new Random()).NextDouble().ToString(), "");
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
                                int count = 0;
                                var error = AutoXue.RegMobil(cmd.Name, rec.FromUserName, out count);
                                if (error.error)
                                {
                                    //返回出错信息
                                    var etext = new WX_Send_Text(rec, error.message);
                                    return etext.ToXml(author);
                                }

                                if (input.Image == 0)
                                {
                                    //准备回复消息
                                    var epic = new WX_Send_News(rec);
                                    epic.Add("报名步骤【二】", "", "", "");
                                    epic.Add(string.Format("　　{0}，你已提交{1}个号码，如果还有，请重复步骤二，最多保存两个！\n　　保存完毕，执行步骤三，从正上方清晰地拍摄报名原件照片并上传，不得少于三张！", error.message, count), "", "http://a.jysycz.cn/image?name=wx_yes&r=" + (new Random()).NextDouble().ToString(), "");
                                    return epic.ToXml(author);
                                }
                                else
                                {
                                    //如果已经上传过图片，就不要提示了
                                    return "";
                                }
                            }
                        }

                    //图片
                    case "image":
                        //TODO：审核成功，不必再上传图片


                        if (input.IDC && input.Mobil >= 1)
                        {
                            //条件：身份证与绑定，并添加了联系电话，才能上传照片
                            var picurl = rec.XmlElement("PicUrl");
                            //下载图片
                            var sname = string.Empty;
                            var sidc = string.Empty;
                            var sid = string.Empty;
                            var errorimage = AutoXue.RegImage(picurl, rec.FromUserName, out sname, out sidc, out sid);

                            if (errorimage.error)
                            {
                                //出错
                                var etext = new WX_Send_Text(rec, errorimage.message);
                                return etext.ToXml(author);
                            }
                            else
                            {
                                //不错，3张的时候，提示二维码，多了不再提示
                                if (int.Parse(errorimage.message) % 3 == 0)
                                {
                                    //二维码
                                    var epic = new WX_Send_News(rec);
                                    epic.Add("报名步骤【四】", string.Format("{0}：\n　　如果照片没有上传结束，请重复步骤三\n　　原件照片已经全部上传，请携带手机和毕业证、户口簿、房产证等原件到报名窗口，出示条形码审核", sname), string.Format("http://a.jysycz.cn/code?content={0}&r={1}", sidc, (new Random()).NextDouble().ToString()), "");
                                    return epic.ToXml(author);
                                }
                                else
                                {
                                    //提示
                                    //var mtext = new WX_Send_Text(rec, string.Format("您已上传了 {0} 张图片", errorimage.message));
                                    //return mtext.ToXml(author);
                                    if (int.Parse(errorimage.message) > 3)
                                    {
                                        var epic = new WX_Send_News(rec);
                                        epic.Add("提示", "", "", "");
                                        epic.Add(string.Format("你已上传了{0}张照片。", errorimage.message), "", string.Format("http://a.jysycz.cn/picture?name={0}&r={1}", sid, (new Random()).NextDouble().ToString()), "");
                                        return epic.ToXml(author);
                                    }
                                    else
                                    {
                                        var epic = new WX_Send_News(rec);
                                        epic.Add("报名步骤【三】", "", "", "");
                                        epic.Add(string.Format("你已上传了{0}张照片。", errorimage.message), "", string.Format("http://a.jysycz.cn/picture?name={0}&r={1}", sid, (new Random()).NextDouble().ToString()), "");
                                        return epic.ToXml(author);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //提示
                            var mtext = new WX_Send_Text(rec, "注意：只有在输入学生身份证号及联系电话以后，上传的照片才有效");
                            return mtext.ToXml(author);
                        }
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

        //图文缩略图
        public void Nail(string name)
        {
            WXImage.CutForSquare(name, 200);
        }

        //图片浏览
        public void picture(string name)
        {
            var fileName = "~/Upload/XueImages/" + name + ".jpg";
            XingCode.CodeOutputStream(fileName);
        }

        public ActionResult oauth()
        {
            return View();
        }

    }
}