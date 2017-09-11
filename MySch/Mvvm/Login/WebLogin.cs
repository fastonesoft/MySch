using MySch.Bll.WX.Model;
using MySch.Core;
using MySch.Dal;
using MySch.Helper;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Login
{
    public class OAuth
    {
        public string state { get; set; }
        public string code { get; set; }
        public void Login()
        {
            //授权登录
            var oaken = WX_Url.OAccessToKen(WX_Const.webAppID, WX_Const.webAppSecret, code);
            //读取信息
            //var infor = WX_Url.WebOAuserInfor(oaken.access_token, oaken.openid);
            ////
            //infor.CheckUser();
            //infor.ToSession();
            //0最大
            //http://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJjXrNDJ8j4nsiaWdMSeZTiaAUsXZBW8xBiaR9ibArRpKTRjVzicatheA9icVUrRcLSjjBcHYfgXyUQuxiaw/0
            //http://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJjXrNDJ8j4nsiaWdMSeZTiaAUsXZBW8xBiaR9ibArRpKTRjVzicatheA9icVUrRcLSjjBcHYfgXyUQuxiaw/46
            //http://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJjXrNDJ8j4nsiaWdMSeZTiaAUsXZBW8xBiaR9ibArRpKTRjVzicatheA9icVUrRcLSjjBcHYfgXyUQuxiaw/64
            //http://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJjXrNDJ8j4nsiaWdMSeZTiaAUsXZBW8xBiaR9ibArRpKTRjVzicatheA9icVUrRcLSjjBcHYfgXyUQuxiaw/96
            //http://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTJjXrNDJ8j4nsiaWdMSeZTiaAUsXZBW8xBiaR9ibArRpKTRjVzicatheA9icVUrRcLSjjBcHYfgXyUQuxiaw/132
        }
    }



    public class WebOAuthInfor
    {
        //登录信息
        public string openid { get; set; }
        public string nickname { get; set; }
        public string unionid { get; set; }
        public string headimgurl { get; set; }
        //用户姓名
        public string username { get; set; }

        public static WebOAuthInfor WebOAuserInfor(string oaToken, string openid)
        {
            var inforurl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", oaToken, openid);
            var infors = HtmlHelp.GetHtml(inforurl, "UTF-8");
            return Jsons.JsonEntity<WebOAuthInfor>(infors);
        }


        //检测用户类型
        public void CheckUser()
        {
            try
            {
                var acc = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (acc == null) {
                    //不是教师
                    var student = DataCRUD<Student>.Entity(a => a.RegUID == unionid);
                    //
                    username = student == null ? "游客" : student.Name + " 的家长";
                }
                else
                {
                    //是注册用户
                    username = acc.Name;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToSession()
        {
            HttpContext.Current.Session["web_userinfor"] = this;
        }
    }
}