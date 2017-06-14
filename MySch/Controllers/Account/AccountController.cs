﻿using MySch.Bll;
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
            try
            {
                if (WX_OAuserInfor.HasNoSession())
                {
                    //首页
                    return View();
                }
                else
                {
                    var infor = WX_OAuserInfor.GetFromSession();
                    infor.CheckUser();

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

        //用户登录：退出
        [HttpPost]
        public ActionResult Logoff()
        {
            Session[Setting.SESSION_LOGIN] = null;
            Session.Abandon();
            return Json(new BllError { error = false, message = "退出成功" });
        }

        //用户检测
        public ActionResult Check()
        {
            var infor = WX_OAuserInfor.GetFromSession();
            infor.CheckUser();
            if (infor.isteach || infor.istudent)
            {
                return Content("");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Update(string tname)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();

                //提交审核
                infor.AddTeach(tname);
                return Json(new BllError { error = false, message = tname });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}