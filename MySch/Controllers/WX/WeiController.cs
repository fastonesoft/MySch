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
                            epic.Add("校务在线 - 报名步骤", "", "", "");
                            epic.Add("【一】在最下方输入学生的身份证号，发送", "", "http://a.jysycz.cn/image/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            epic.Add("【二】然后输入家长的手机号码一个，发送", "", "http://a.jysycz.cn/image/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            //epic.Add("【三】点击右下角〖＋〗，然后选择〖拍摄〗，上传清晰的毕业证、户口簿、房产证等原件照片", "", "http://a.jysycz.cn/image/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");
                            //epic.Add("【四】至少传三张图片，才能显示条形码！有了条码后，请携带相关原件、手机到报名窗口审核", "", "http://a.jysycz.cn/image/image?name=wx_no&r=" + (new Random()).NextDouble().ToString(), "");

                            var appid = WX_Const.goneAppID;
                            var url = HttpUtility.UrlEncode("http://a.jysycz.cn/regs/");
                            var url2 = HttpUtility.UrlEncode("http://a.jysycz.cn/regs/examine");
                            var state = "pub";

                            epic.Add("点击本条信息，打开照片上传页面", "", "", string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, url, state));
                            epic.Add("点击本条信息，审核", "", "", string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, url2, state));
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