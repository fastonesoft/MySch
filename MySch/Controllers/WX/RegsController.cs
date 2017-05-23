using MySch.Bll;
using MySch.Bll.Func;
using MySch.Bll.WX;
using MySch.Bll.WX.Form;
using MySch.Bll.WX.Model;
using MySch.Bll.Xue;
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
    public class RegsController : Controller
    {
        public ActionResult Index(WX_OAuth auth)
        {
            try
            {
                //读取code          
                var codeurl = WX_Url.OAuthCode(WX_App.AppID, WX_App.AppSecret, auth.code);
                var codes = HtmlHelp.GetHtml(codeurl, "UTF-8");

                //检测是否出错
                if (codes.Contains("errcode"))
                {
                    var error = Jsons.JsonEntity<WX_Error>(codes);
                    return Content(error.GetMessage());
                }
                else
                {
                    //解析网页的token
                    var token = Jsons.JsonEntity<WX_AccessTokenOauth>(codes);
                    token.create_time = DateTime.Now;
                    //缓存
                    token.ToSession();
                    //检查授权状态
                    if (token.scope == "snsapi_userinfo")
                    {
                        //读取用户信息
                        var userurl = WX_Url.UserInfor(token.access_token, token.openid);
                        var user = HtmlHelp.GetHtml(userurl, "UTF-8");
                        //序列化
                        var infor = Jsons.JsonEntity<WX_UserInfor>(user);
                        infor.codePage = Setting.Url(Request);
                        //检测是否绑定学生
                        infor.Check();
                        //缓存
                        infor.ToSession();

                        //显示网页
                        return View();
                    }
                    else
                    {
                        return Content("没有授权访问");
                    }
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// 返回js注册用的信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index()
        {
            try
            {
                //检查Session
                var wxtoken = WX_AccessToken.GetAccessToken();
                var infor = WX_UserInfor.GetSessionToken();

                //签名算法
                var signature = new WX_Signature(WX_App.AppID, WX_Jsticket.GetJsticket(wxtoken), infor.codePage, infor.idc, infor.name);
                signature.idc = infor.idc;
                signature.name = infor.name;

                //序列化
                return Json(signature);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Reg(string idc, string mobil)
        {
            try
            {
                var userinfor = WX_UserInfor.GetSessionToken();
                var res = AutoXue.RegStud(idc.ToUpper(), mobil, userinfor.openid);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllErrorEx { error = true, message = e.Message });
            }
        }

        //自行上传
        [HttpPost]
        public ActionResult UploadImageSelf(string mediaID, string uploadType)
        {
            try
            {
                //检测session
                var token = WX_AccessTokenOauth.GetSessionToken();
                var res = WXImage.SaveImageSelf(mediaID, uploadType, token.openid);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //带人上传
        [HttpPost]
        public ActionResult UploadImageOther(string mediaID, string uploadType, string otherID)
        {
            try
            {
                //检测session
                var token = WX_AccessTokenOauth.GetSessionToken();
                var res = WXImage.SaveImageOther(mediaID, uploadType, otherID);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //获取上传照片列表
        [HttpPost]
        public ActionResult GetImages()
        {
            try
            {
                //检测session
                var token = WX_AccessTokenOauth.GetSessionToken();
                var res = WXImage.GetUnloadedImages(token.openid);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetImagesByType(string idc)
        {
            try
            {
                //检测session
                var token = WX_AccessTokenOauth.GetSessionToken();
                //根据身份证，读取openid
                var stud = WX_UserStud.OpenID(idc);

                var res = WXImage.GetImagesByType(stud);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //////////////////////////////////////////////
        //审核
        public ActionResult Examine(WX_OAuth auth)
        {
            try
            {
                //读取code          
                var codeurl = WX_Url.OAuthCode(WX_App.AppID, WX_App.AppSecret, auth.code);
                var code = HtmlHelp.GetHtml(codeurl, "UTF-8");

                //检测是否出错
                if (code.Contains("errcode"))
                {
                    var error = Jsons.JsonEntity<WX_Error>(code);
                    return Content(error.GetMessage());
                }
                else
                {
                    //解析网页的token
                    var token = Jsons.JsonEntity<WX_AccessTokenOauth>(code);
                    token.create_time = DateTime.Now;
                    //缓存
                    token.ToSession();
                    //检查授权状态
                    if (token.scope == "snsapi_userinfo")
                    {
                        //读取用户信息
                        var userurl = WX_Url.UserInfor(token.access_token, token.openid);
                        var user = HtmlHelp.GetHtml(userurl, "UTF-8");
                        //序列化
                        var infor = Jsons.JsonEntity<WX_UserInfor>(user);
                        infor.codePage = Setting.Url(Request);

                        //缓存
                        infor.ToSession();

                        //显示网页
                        return View();
                    }
                    else
                    {
                        return Content("没有授权访问");
                    }
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}