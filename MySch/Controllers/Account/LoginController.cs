using MySch.Bll;
using MySch.Bll.WX.Model;
using MySch.Mvvm.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Account
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            try
            {
                //var infornot = new WX_OAuserInfor
                //{
                //    openid = "o47ZhvxoQA9QOOgDSZ5hGaea4xdI",
                //    unionid = "o47ZhvxoQA9QOOgDSZ5hGaea4xdI",
                //    nickname = "石亮",
                //};
                //infornot.CheckUser();
                //infornot.ToSession();
                //ViewBag.ShowMenu = infornot.unionid == "o47ZhvxoQA9QOOgDSZ5hGaea4xdI" || infornot.unionid == "o47ZhvzWPWSNS26vG_45Fuz5JMZk" ? true : false;
                //ViewBag.UserName = infornot.username;
                //ViewBag.NickName = infornot.nickname;
                //return View("Main");

                //二维码登录
                if (WX_OAuserInfor.HasNoSession())
                {
                    //首页
                    return View();
                }
                else
                {
                    var infor = WX_OAuserInfor.GetFromSession();
                    infor.CheckUser();

                    ViewBag.ShowMenu = infor.unionid == "o47ZhvxoQA9QOOgDSZ5hGaea4xdI" || infor.unionid == "o47ZhvzWPWSNS26vG_45Fuz5JMZk" ? true : false;
                    ViewBag.UserName = infor.username;
                    ViewBag.NickName = infor.nickname;
                    //已登录
                    return View("Main");
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        //回调接口
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
                return RedirectToAction("Index", "Login");
            }
            catch (Exception e)
            {
                return Content(e.Message);
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