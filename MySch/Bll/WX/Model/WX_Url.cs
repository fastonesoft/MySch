using MySch.Core;
using MySch.Helper;
using MySch.Mvvm.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Url
    {
        //网页授权，临时凭证
        public static WX_AccessTokenOauth OAccessToKen(string appid, string secret, string code)
        {
            var codeurl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            var codes = HtmlHelp.GetHtml(codeurl, "UTF-8");
            //检测是否出错
            if (codes.Contains("errcode"))
            {
                var error = Jsons.JsonEntity<WX_Error>(codes);
                throw new Exception(error.GetMessage());
            }

            //解析网页的token
            var oaken = Jsons.JsonEntity<WX_AccessTokenOauth>(codes);
            //检查授权状态
            if (oaken.scope != "snsapi_userinfo" && oaken.scope != "snsapi_login") throw new Exception("没有授权访问");

            //缓存
            oaken.create_time = DateTime.Now;
            oaken.ToSession();
            return oaken;
        }

        //获取用户信息
        public static WX_OAuserInfor OAuserInfor(string oaToken, string openid)
        {
            var inforurl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", oaToken, openid);
            var infors = HtmlHelp.GetHtml(inforurl, "UTF-8");
            var infor = Jsons.JsonEntity<WX_OAuserInfor>(infors);

            infor.ToSession();
            return infor;
        }

        public static WebOAuthInfor WebOAuserInfor(string oaToken, string openid)
        {
            var inforurl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", oaToken, openid);
            var infors = HtmlHelp.GetHtml(inforurl, "UTF-8");
            return Jsons.JsonEntity<WebOAuthInfor>(infors);
        }

        //获取服务器授权
        public static WX_AccessToken AccessToken(string appid, string secret)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
            var jsons = HtmlHelp.GetHtml(url, "UTF-8");
            var token = Jsons.JsonEntity<WX_AccessToken>(jsons);
            token.create_time = DateTime.Now;
            return token;
        }

        //获取媒体文件
        public static void MediaFile(string token, string mediaID, string filePath)
        {
            var url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", token, mediaID);
            //保存
            using (var web = new WebClient())
            {
                web.DownloadFile(url, filePath);
            }
        }

        public static string MenuCreate(string token, string data)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", token);
            return HtmlHelp.PostHtml(url, data, "UTF-8");
        }

        public static string MenuView(string appid, string redirect_url, string state)
        {
            return string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, redirect_url, state);
        }

        public static WX_Jsticket Jsticket(string token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", token);
            var jsons = HtmlHelp.GetHtml(url, "UTF-8");
            return Jsons.JsonEntity<WX_Jsticket>(jsons);
        }
    }
}