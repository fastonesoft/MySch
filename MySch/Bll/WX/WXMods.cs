using MySch.Bll;
using MySch.Bll.Wei;
using MySch.Bll.WX;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace MySch.Bll.WX
{
    //验证类
    public class WX_Author
    {
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string signature { get; set; }
        public string echostr { get; set; }
    }

    public class WX_Author_Ex : WX_Author
    {
        //作不同的参数，用以区分不同的Control
        public string encrypt_type { get; set; }
        public string msg_signature { get; set; }
    }

    //消息基类
    public class WX_Message_Base
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public int CreateTime { get; set; }
        public string MsgType { get; set; }
    }

    //消息接收类
    public class WX_Rec_Base : WX_Message_Base
    {
        private XmlElement _element;

        public WX_Rec_Base()
        {

        }

        /// <summary>
        /// 解密，接收的消息
        /// </summary>
        /// <param name="xml_crypt"></param>
        /// <param name="author"></param>
        public WX_Rec_Base(string xml_crypt, WX_Author_Ex author)
        {
            try
            {
                var wxcrypt = new WXCryptMsg("WX1979ToKen", "wqbpIsxgyqLKWmnEbVlHmgTvj0BLSfNTBCAcxYhZRGf", "wx8e6ce1260ba9f214");
                string xml = string.Empty;

                if (author.encrypt_type == "aes")
                {
                    //密文
                    wxcrypt.DecryptMsg(author.msg_signature, author.timestamp, author.nonce, xml_crypt, ref xml);
                }
                else
                {
                    //明文
                    xml = xml_crypt;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                _element = doc.DocumentElement;
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

    /// <summary>
    /// 文本消息接收类
    /// </summary>
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

    /// <summary>
    /// 图片消息接收类
    /// </summary>
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

    /// <summary>
    /// 事件消息接收类
    /// </summary>
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

    //消息发送基本类
    public class WX_Send_Base : WX_Message_Base
    {
        public virtual string ToXml(string con)
        {
            this.CreateTime = Setting.DateTimeToInt(DateTime.Now);

            string xml = string.Empty;
            xml += string.Format("<ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            xml += string.Format("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            xml += string.Format("<CreateTime>{0}</CreateTime>", CreateTime);
            xml += string.Format("<MsgType><![CDATA[{0}]]></MsgType>", MsgType);
            return string.Format(con, xml);
        }

        public string ToXml(WX_Author_Ex author)
        {
            //明文
            var xml = ToXml("<xml>{0}</xml>");

            if (author.encrypt_type == "aes")
            {
                string wxmsg = string.Empty;
                var wxcrypt = new WXCryptMsg("WX1979ToKen", "wqbpIsxgyqLKWmnEbVlHmgTvj0BLSfNTBCAcxYhZRGf", "wx8e6ce1260ba9f214");
                wxcrypt.EncryptMsg(xml, author.timestamp, author.nonce, ref wxmsg);
                return wxmsg;
            }
            else
            {
                return xml;
            }
        }

    }

    /// <summary>
    /// 文本消息发送类
    /// </summary>
    public class WX_Send_Text : WX_Send_Base
    {
        public string Content { get; set; }
        public WX_Send_Text(WX_Rec_Base rec, string content)
        {
            this.MsgType = "text";
            this.FromUserName = rec.ToUserName;
            this.ToUserName = rec.FromUserName;
            //
            this.Content = content;
        }

        public override string ToXml(string con)
        {
            //基本数据
            string xml = base.ToXml("{0}");
            //新增属性
            xml += string.Format("<Content><![CDATA[{0}]]></Content>", Content);
            //
            return string.Format(con, xml);
        }
    }


    /// <summary>
    /// 单一图文描述
    /// </summary>
    public class WX_Send_Pic_Item
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// 图文消息发送
    /// </summary>
    public class WX_Send_Pic : WX_Send_Base
    {
        List<WX_Send_Pic_Item> _Items = new List<WX_Send_Pic_Item>();

        public WX_Send_Pic(WX_Rec_Base rec)
        {
            this.MsgType = "news";
            this.FromUserName = rec.ToUserName;
            this.ToUserName = rec.FromUserName;
        }

        public void Add(string title, string description, string picurl, string url)
        {
            var item = new WX_Send_Pic_Item { Title = title, Description = description, PicUrl = picurl, Url = url };
            Add(item);
        }

        public void Add(WX_Send_Pic_Item picItem)
        {
            _Items.Add(picItem);
        }

        public void Adds(List<WX_Send_Pic_Item> items)
        {
            _Items = items;
        }

        public override string ToXml(string con)
        {
            //构造图文列表
            string items = "";
            foreach (var item in _Items)
            {
                items += "<item>";
                items += string.Format("<Title><![CDATA[{0}]]></Title> ", item.Title);
                items += string.Format("<Description><![CDATA[{0}]]></Description>", item.Description);
                items += string.Format("<PicUrl><![CDATA[{0}]]></PicUrl>", item.PicUrl);
                items += string.Format("<Url><![CDATA[{0}]]></Url>", item.Url);
                items += "</item>";
            }
            //将图文数据打包
            string xml = base.ToXml("{0}");
            xml += string.Format("<ArticleCount>{0}</ArticleCount>", _Items.Count());
            xml += "<Articles>";
            xml += items;
            xml += "</Articles>";
            xml += "<FuncFlag>0</FuncFlag>";
            return string.Format(con, xml);
        }
    }

    //命令记录
    public class WX_Command_Rec
    {
        public bool IDS { get; set; }
        public bool Phone { get; set; }
        public int Picture { get; set; }
        public bool Showed { get; set; }

        public void SaveToSession(HttpSessionStateBase session)
        {
            session["InputSession"] = this;
        }

        public static WX_Command_Rec GetFromSession(HttpSessionStateBase session)
        {
            var input = (WX_Command_Rec)session["InputSession"];

            if (input == null)
                input = new WX_Command_Rec { IDS = false, Phone = false, Picture = 0, Showed = false };

            return input;
        }

    }

    //命令行
    public class WX_Command
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public static WX_Command GetCommand(string regs, string command)
        {
            try
            {
                Regex regex = new Regex(regs);
                Match match = regex.Match(command);
                //
                return match.Success ? new WX_Command { Name = match.Groups[1].ToString() } : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}