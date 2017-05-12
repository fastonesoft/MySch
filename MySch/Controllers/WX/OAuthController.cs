using MySch.Bll;
using MySch.Bll.Func;
using MySch.Bll.WX;
using MySch.Bll.WX.Model;
using MySch.Helper;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.WX
{
    public class OAuthController : Controller
    {
        public ActionResult Index(WX_OAuth auth)
        {
            //读取code
            //var appid = "wx01df6a9fe809485f";
            //var secret = "c2ac6bc689b690f54d72f8479a26714b";
            //测试平台的
            var appid = "wx01df6a9fe809485f";
            var secret = "c2ac6bc689b690f54d72f8479a26714b";

            var codeurl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, auth.code);
            var codes = HtmlHelp.GetHtml(codeurl, "UTF-8");

            //检测是否出错
            if (codes.Contains("errcode"))
            {
                WX_Error error = Jsons.JsonEntity<WX_Error>(codes);
                return Content(error.GetMessage());
            }
            else
            {
                //解析网页的token
                AccessTokenOauth token = Jsons.JsonEntity<AccessTokenOauth>(codes);
                if (token.scope == "snsapi_userinfo")
                {
                    var inforurl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", token.access_token, token.openid);
                    var infor = HtmlHelp.GetHtml(inforurl, "UTF-8");
                    WX_UserInfor userinfor = Jsons.JsonEntity<WX_UserInfor>(infor);

                    ViewBag.openid = userinfor.openid;
                    ViewBag.nickname = userinfor.nickname;

                    //中控token
                    var wxtoken = WX_AccessToken.GetAccessToken();

                    //签名算法
                    var signature = new WX_Signature(appid, WX_Jsticket.GetJsticket(wxtoken), Setting.Url(Request));

                    ViewBag.appid = signature.appId;
                    ViewBag.timestamp = signature.timestamp;
                    ViewBag.noncestr = signature.noncestr;
                    ViewBag.signature = signature.signature;
                    ViewBag.accesstoken = wxtoken;

                    return View();
                }
                else
                {
                    return Content("没有授权访问");
                }
            }
        }

        public void UploadImage(string accessToken, string mediaID, string openID)
        {
            WXImage.SaveUnloadImage(accessToken, mediaID, openID);
        }
    }
}