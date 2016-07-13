using MySch.Dal;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace MySch.Models
{
    public class MyWxApi
    {
        #region 验证微信签名

        //验证微信签名
        public static bool CheckSignature(string token, string timestamp, string nonce, string signature)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = MySetting.GetSHA1(tmpStr);
            tmpStr = tmpStr.ToLower();

            return tmpStr == signature ? true : false;
        }

        public static bool CheckSignature(WX_Author au)
        {
            if (au == null) return false;

            return CheckSignature("WX1979ToKen", au.timestamp, au.nonce, au.signature);
        }

        #endregion

        #region 自动报名

        public static ErrorModel StudReg(string content)
        {
            try
            {
                //首先将输入的格式规则
                Regex aregx = new Regex(@"([\u4e00-\u9fa5]+)(\d+[0-9xX])");
                Match match = aregx.Match(content);

                //匹配不成，没有返回数据
                if (!match.Success) return new ErrorModel { error = true, message = "请输入：学生姓名和身份证号（不需分隔）" };

                //成功则取出Name、ID
                string Name = match.Groups[1].ToString();
                string ID = match.Groups[2].ToString().ToUpper();

                //检测身份证号是否有效
                MySetting.IDS(ID);

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
                    srcBit[i] = new Bitmap(HttpContext.Current.Server.MapPath(string.Format("~/Images/vbit/{0}.bmp", Convert.ToChar(Convert.ToInt16('a') + i))));
                }
                //读取图片
                Bitmap dest = null;
                string valid = string.Empty;
                string imageurl = "http://jcjy.etec.edu.cn/studman2/genImageCode?rnd=" + DateTime.Now.Ticks.ToString();
                //异常循环记录器
                int errorcount = 0;
                //循环读取图片  直到识别出 5 个字符
                do
                {
                    using (HttpWebResponse resp = MyHtml.GetResponse(imageurl, cookies))
                    {
                        dest = MyHtml.GetBitmap(resp);
                    }
                    valid = MyImageCode.GetValidedCode(dest, srcBit);
                    //循环记录
                    errorcount++;
                    //异常退出
                    if (errorcount > 30) return new ErrorModel { error = true, message = "请确认验证码是否变更" };
                } while (valid.Length != 5);

                //三、准备Post请求数据
                Random rnd = new Random();
                Dictionary<string, string> dicts = new Dictionary<string, string>();
                dicts.Add("name", Name);
                dicts.Add("cid", ID);
                dicts.Add("randomCode", valid);
                dicts.Add("v", rnd.NextDouble().ToString());
                string postdata = MyHtml.DictToPostData(dicts, Encoding.GetEncoding("GBK"));
                //提交请求
                HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, postdata, Encoding.GetEncoding("GBK"));
                string html = MyHtml.GetHtml(postresp, Encoding.GetEncoding("GBK"));
                //分析返回数据
                Regex regx = new Regex(@"<td>([\u4e00-\u9fa5]+|\d{17}[0-9X]|[A-Z]\d{17}[0-9X])</td>");
                MatchCollection matchs = regx.Matches(html);

                //如果没有找到数据，则返回提示
                if (matchs.Count == 0) return new ErrorModel { error = true, message = "无学籍记录！检查身份证与姓名是否正确" };

                //排除重复身份证号
                string id = matchs[2].Groups[1].ToString();
                var db = DataQuery<TStudReg>.Entity(a => a.ID == id);
                if (db != null) return new ErrorModel { error = true, message = "该身份证号学籍已注册" };

                //根据返回数据 -> 创建学生报名记录
                TStudReg stud = new TStudReg();
                stud.fromSch = matchs[0].Groups[1].ToString();
                stud.Name = matchs[1].Groups[1].ToString();
                stud.ID = id;
                stud.fromGrade = matchs[3].Groups[1].ToString();
                stud.nationID = matchs[4].Groups[1].ToString();
                stud.readState = matchs[5].Groups[1].ToString();
                stud.isProblem = matchs[6].Groups[1].ToString() == "是" ? true : false;
                stud.GD = Guid.NewGuid().ToString("N");
                //
                stud.schChoose = false;
                stud.studNo = null;
                stud.Memo = null;
                //
                stud.Reged = false;

                //添加
                DataADU<TStudReg>.Add(stud);
                //返回
                return new ErrorModel { error = false, message = stud.GD };
            }
            catch (Exception e)
            {
                return new ErrorModel { error = true, message = e.Message };
            }
        }

        #endregion
    }

    public class WX_Author
    {
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string signature { get; set; }
        public string echostr { get; set; }
    }

    public class WX_Author_Ex : WX_Author
    {
        //临时用
    }

    //消息基类
    public class WX_Message_Base
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
    }

    #region 消息接收类

    public class WX_Rec_Base : WX_Message_Base
    {
        private XmlElement _element;
        public XmlElement Element { get { return _element; } }

        public void XmlInit(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                _element = doc.DocumentElement;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void XmlInit(XmlElement ele)
        {
            try
            {
                _element = ele;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string XmlElement(string name)
        {
            try
            {
                return _element.SelectSingleNode(name).InnerText;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void XmlToObj()
        {
            try
            {
                this.FromUserName = XmlElement("FromUserName");
                this.ToUserName = XmlElement("ToUserName");
                this.CreateTime = int.Parse(XmlElement("CreateTime"));
                this.MsgType = XmlElement("MsgType");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class WX_Rec_Text : WX_Rec_Base
    {
        public string Content { get; set; }
        public override void XmlToObj()
        {
            try
            {
                base.XmlToObj();
                this.Content = XmlElement("Content");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class WX_Rec_Image : WX_Rec_Base
    {
        public string PicUrl { get; set; }
        public string MediaId { get; set; }

        public override void XmlToObj()
        {
            try
            {
                base.XmlToObj();
                this.PicUrl = XmlElement("PicUrl");
                this.MediaId = XmlElement("MediaId");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class WX_Rec_Event : WX_Rec_Base
    {
        public string Event { get; set; }
        public string EventKey { get; set; }

        public override void XmlToObj()
        {
            try
            {
                base.XmlToObj();
                this.Event = XmlElement("Event");
                this.EventKey = XmlElement("EventKey");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class WX_Rec_Event_Code : WX_Rec_Event
    {
        public string Ticket { get; set; }

        public override void XmlToObj()
        {
            try
            {
                base.XmlToObj();
                this.Ticket = XmlElement("Ticket");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    #endregion


    //消息发送基本类
    public class WX_Send_Base : WX_Message_Base
    {
        public virtual string ToXml(string con)
        {
            this.CreateTime = MySetting.DateTimeToInt(DateTime.Now);

            string xml = string.Empty;
            xml += string.Format("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            xml += string.Format("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            xml += string.Format("<CreateTime>{0}</CreateTime>", CreateTime);
            xml += string.Format("<MsgType><![CDATA[{0}]]></MsgType>", MsgType);
            return string.Format(con, xml);
        }

        public virtual string ToXml()
        {
            return ToXml("<xml>{0}</xml>");
        }

    }

    public class WX_Send_Text : WX_Send_Base
    {
        public string Content { get; set; }
        public WX_Send_Text(WX_Rec_Base rec)
        {
            this.MsgType = "text";
            this.FromUserName = rec.ToUserName;
            this.ToUserName = rec.FromUserName;
        }

        public void Init(string content)
        {
            this.Content = content;
        }

        public override string ToXml(string con)
        {
            string xml = base.ToXml("{0}");
            xml += string.Format("<Content><![CDATA[{0}]]></Content>", Content);
            return string.Format(con, xml);
        }
    }

    public class WX_Send_Pic : WX_Send_Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }

        public WX_Send_Pic(WX_Rec_Base rec)
        {
            this.MsgType = "news";
            this.FromUserName = rec.ToUserName;
            this.ToUserName = rec.FromUserName;
        }

        public void Init(string title, string description, string picurl, string url)
        {
            this.Title = title;
            this.Description = description;
            this.PicUrl = picurl;
            this.Url = url;
        }

        public override string ToXml(string con)
        {
            string xml = base.ToXml("{0}");
            xml += string.Format("<ArticleCount>{0}</ArticleCount>", 1);
            xml += "<Articles>";
            xml += "<item>";
            xml += string.Format("<Title><![CDATA[{0}]]></Title> ", Title);
            xml += string.Format("<Description><![CDATA[{0}]]></Description>", Description);
            xml += string.Format("<PicUrl><![CDATA[{0}]]></PicUrl>", PicUrl);
            xml += string.Format("<Url><![CDATA[{0}]]></Url>", Url);
            xml += "</item>";
            xml += "</Articles>";
            xml += "<FuncFlag>0</FuncFlag>";
            return string.Format(con, xml);
        }
    }


}