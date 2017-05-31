using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.WX.Model;
using MySch.Helper;
using System;
using System.Text;
using System.Web.Mvc;

namespace MySch.Controllers.Account
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            if (WX_OAuserInfor.HasSession())
            {
                //已登录
                return View("Main");
            }
            else
            {
                //首页
                return View();
            }
        }

        public ActionResult Login(WX_OAuth auth)
        {
            //授权          
            var codeurl = WX_Url.OAuthCode(WX_Const.netAppID, WX_Const.netAppSecret, auth.code);
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
                if (token.scope == "snsapi_login")
                {
                    //读取用户信息
                    var userurl = WX_Url.OAuserInfor(token.access_token, token.openid);
                    var user = HtmlHelp.GetHtml(userurl, "UTF-8");

                    //序列化
                    var infor = Jsons.JsonEntity<WX_OAuserInfor>(user);
                    infor.codePage = Setting.Url(Request);
                    //检测是否绑定学生
                    infor.BindingStud();
                    //缓存
                    infor.ToSession();

                    //显示网页
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    return Content("没有授权访问");
                }
            }

        }

        //用户登录：检测
        [HttpPost]
        public ActionResult Check()
        {
            var login = BllLogin.GetLogin(Session);
            if (login == null)
            {
                var acc = new BllAcc
                {
                    ID = Guid.NewGuid().ToString("N"),
                };
                return View("Logon", acc);
            }
            else
            {
                //已登录
                //要跳转的Action不能使用POST方式
                return RedirectToAction("Index", "Client");
            }
        }

        //用户登录：退出
        [HttpPost]
        public ActionResult Logoff()
        {
            Session[Setting.SESSION_LOGIN] = null;
            Session.Abandon();
            return Json(new BllError { error = false, message = "退出成功" });
        }
    }
}