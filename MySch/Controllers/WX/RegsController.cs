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
                var codeurl = WX_Url.OAuthCode(WX_App.gAppID, WX_App.gAppSecret, auth.code);
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
                        var userurl = WX_Url.OAuserInfor(token.access_token, token.openid);
                        var user = HtmlHelp.GetHtml(userurl, "UTF-8");

                        //序列化
                        var infor = Jsons.JsonEntity<WX_OAuserInfor>(user);
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
        public ActionResult Jssdk()
        {
            try
            {
                //检查中控
                var wxtoken = WX_AccessToken.GetAccessToken();
                var token = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //签名算法
                var signature = new WX_Signature(WX_App.gAppID, WX_Jsticket.GetJsticket(wxtoken), infor.codePage, infor.idc, infor.name);
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

        //注册
        [HttpPost]
        public ActionResult Reg(string idc, string mobil)
        {
            try
            {
                var token = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = AutoXue.RegStud(idc.ToUpper(), mobil, infor.unionid);
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
                //检测页面、用户
                var token = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = WX_UploadImage.SaveImageSelf(mediaID, uploadType, infor.unionid);
                return Json(new BllError { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //////////////////////////////////////////////

        //审核页面
        public ActionResult Examine(WX_OAuth auth)
        {
            try
            {
                //读取code          
                var codeurl = WX_Url.OAuthCode(WX_App.gAppID, WX_App.gAppSecret, auth.code);
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
                        var userurl = WX_Url.OAuserInfor(token.access_token, token.openid);
                        var user = HtmlHelp.GetHtml(userurl, "UTF-8");
                        //序列化
                        var infor = Jsons.JsonEntity<WX_OAuserInfor>(user);
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

        //图片分类
        [HttpPost]
        public ActionResult GetImagesTypeByIdc(string idc)
        {
            try
            {
                //检测页面、用户
                var token = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //根据身份证获取图片列表
                var res = WX_UploadImage.GetImagesTypeByIdc(idc);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //图片删除
        [HttpPost]
        public ActionResult DeleteImage(string url)
        {
            try
            {
                //检测页面、用户
                var token = WX_AccessTokenOauth.GetSessionToken();
                var user = WX_OAuserInfor.GetFromSession();

                var id = url.Split('=');


            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //带人上传
        [HttpPost]
        public ActionResult UploadImageOther(string mediaID, string uploadType, string Other)
        {
            try
            {
                //检测页面、用户
                var token = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //代别人上传
                var res = WX_UploadImage.SaveImageForOther(mediaID, uploadType, Other);
                return Json(new BllError { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //审核动作
        [HttpPost]
        public ActionResult Examine(string ID, bool Choose)
        {
            try
            {
                //检测页面、用户
                var token = WX_AccessTokenOauth.GetSessionToken();
                var user = WX_OAuserInfor.GetFromSession();

                //审核
                WX_Examine.Examine(ID, Choose, user.unionid);
                return Json(new BllError { error = false, message = "审核提交成功" });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}