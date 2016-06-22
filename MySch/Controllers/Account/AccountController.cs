using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Account
{
    public class AccountController : BaseController
    {
        //校务在线：容器
        //不能使用POST方式访问
        public ActionResult Index()
        {
            //var db = DataQuery<TAcc>.Entity("admin");
            //MyType<TAcc>.GetPropertyInfor(db, "ID");

            //string dd = MyGD.GetGD("Adminacc", "c32128402");
            //string ee = MyPwd.Password("c32128402", dd, MyMD5.MD5("stone.2"));
            //return Content(ee);

            return View();
        }

        //用户登录：窗体
        [HttpPost]
        public ActionResult Logon()
        {
            return View();
        }

        //用户登录：数据提交
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TAcc acc)
        {
            //封IP
            if (MyLogin.FixedOfIP(Request, acc))
            {
                //MyLogin.AddLog(Request, acc, "登录动作过频");
                //动作过频，不能记录
                return Json(new ErrorModel { error = true, message = "错误：登录动作过频，请稍等！" });
            }

            //1、检测用户名是否正确
            var db = DataQuery<TAcc>.Entity(a => a.ID == acc.ID);
            if (db == null)
            {
                MyLogin.AddLog(Request, acc, "未注册用户");
                return Json(new ErrorModel { error = true, message = "错误：未注册用户，无法登录！" });
            }
            //1.1、验证检测
            acc.GD = MySetting.GetGD("Adminacc", acc.ID);
            if (acc.GD != db.GD)
            {
                MyLogin.AddLog(Request, acc, "提交数据无法验证");
                return Json(new ErrorModel { error = true, message = "错误：提交数据无法通过验证！" });
            }

            //2、检测密码是否正确
            if (MyLogin.Password(acc.ID, acc.GD, acc.Pwd) != db.Pwd)
            {
                MyLogin.AddLog(Request, acc, "密码有误");
                return Json(new ErrorModel { error = true, message = "错误：密码有误，请重新输入！" });
            }

            if (db.Fixed)
            {
                //登录失败：添加日志
                MyLogin.AddLog(Request, db, "用户被冻结");
                return Json(new ErrorModel { error = true, message = "错误：用户被冻结，请联系管理员！" });
            }
            else
            {
                //登录成功：记录，并，退出
                MyLogin.SaveLog(Session, Request, db, "登录成功");
                return Json(new ErrorModel { error = false, message = string.Format("用户：{0}成功登录！", db.ID) });
            }
        }

        //用户登录：检测
        [HttpPost]
        public ActionResult Check()
        {
            var login = MyLogin.GetLogin(Session);
            if (login == null)
            {
                return View("Logon");
            }
            else
            {
                //已登录
                //要跳转的Action不能使用POST方式
                //return RedirectToAction("Index", "Client");
                ViewBag.UserName = login.Name;
                return View("~/Views/Home/Index.cshtml");
            }
        }

        //用户登录：退出
        [HttpPost]
        public ActionResult Logoff()
        {
            Session[MySetting.SESSION_LOGIN] = null;
            return Json(new ErrorModel { error = false, message = "退出成功" });
        }

    }
}