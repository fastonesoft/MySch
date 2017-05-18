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
            var appid = "wx0f49a9991c53e2a4";
            var secret = "a3663df809650680e518a1508c4156b8";

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
                WX_AccessTokenOauth token = Jsons.JsonEntity<WX_AccessTokenOauth>(codes);
                token.create_time = DateTime.Now;
                token.ToSession();

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

                    return View();
                }
                else
                {
                    return Content("没有授权访问");
                }
            }
        }

        public ActionResult UploadImage(string mediaID)
        {
            try
            {
                //检测session
                var token = WX_AccessTokenOauth.GetSessionToken();

                if (token != null)
                {
                    WXImage.SaveUnloadImage(mediaID, token.openid);
                    //
                    return Json(new BllError { error = false, message = "图片上传成功" });
                }
                else
                {
                    return Json(new BllError { error = true, message = "页面已过期" });
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        public ActionResult GetImages()
        {
            try
            {
                //检测session
                var token = WX_AccessTokenOauth.GetSessionToken();
                if (token != null)
                {
                    return Json(WXImage.GetUnloadedImages(token.openid));
                }
                else
                {
                    return Json(new BllError { error = true, message = "页面已过期" });
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}