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
            if (WX_OAuserInfor.HasNoSession())
            {
                //首页
                return View();
            }
            else
            {
                //已登录
                return View("Main");
            }
        }

        public ActionResult Login(WX_OAuth auth)
        {
            try
            {
                var user = auth.WebLogin();
                user.codePage = Setting.Url(Request);
                //检测是否绑定学生
                user.BindingStud();
                //缓存
                user.ToSession();

                //显示网页
                return RedirectToAction("Index", "Account");
            }
            catch (Exception e)
            {
                return Content(e.Message);
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