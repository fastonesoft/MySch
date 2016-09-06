using MySch.Bll;
using MySch.Bll.Entity;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MySch.Controllers.Account
{
    public class AccountController : BaseController
    {
        //校务在线：容器
        //不能使用POST方式访问
        public ActionResult Index()
        {
            //var db = DataCRUD<TAcc>.Entity("admin");
            //MyType<TAcc>.GetPropertyInfor(db, "ID");

            //string dd = ApiSetting.GetGD("AdminUser", "32128402");
            //string ee = MyLogin.Password("32128402", dd, ApiSetting.GetMD5("stone.2.net"));
            //return Content(dd + "-" + ee);

            //string   tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile("asdfasdfasd", "SHA1");
            //string ee = ApiSetting.GetSHA1("asdfasdfasd");
            //return Content(tmpStr + "-" + ee);

            //var db = DataCRUD<TStudReg>.All();
            //foreach (var d in db)
            //{
            //    string id = d.ID;
            //    var log = DataCRUD<TLog>.Entitys(a => a.Value.Contains(id));
            //    if (log.Count() > 0)
            //    {
            //        WX_Rec_Base rec = new WX_Rec_Base();
            //        rec.XmlInit(log.First().Value);
            //        rec.XmlToObj();

            //        //将openID写入学生表
            //        d.OpenID = rec.FromUserName;
            //        DataCRUD<TStudReg>.Update(d);
            //    }
            //}

            //return RedirectToAction("Reg11","Account");

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
        public ActionResult Login(BllAcc acc)
        {
            try
            {
                //封IP
                if (BllLogin.FixedOfIP(Request, acc))
                {
                    //MyLogin.AddLog(Request, acc, "登录动作过频");
                    //动作过频，不能记录
                    return Json(new BllError { error = true, message = "错误：登录动作过频，请稍等！" });
                }

                //1、检测用户名是否正确
                var db = BllAcc.GetEntity<BllAcc>(a => a.IDS == acc.IDS);
                if (db == null)
                {
                    BllLogin.AddLog(Request, acc, "未注册用户");
                    return Json(new BllError { error = true, message = "错误：未注册用户，无法登录！" });
                }
                //1.1、验证检测
                acc.ID = Setting.GetGD("AdminUser", acc.IDS);
                if (acc.ID != db.ID)
                {
                    BllLogin.AddLog(Request, acc, "提交数据无法验证");
                    return Json(new BllError { error = true, message = "错误：提交数据无法通过验证！" });
                }

                //2、检测密码是否正确
                if (BllLogin.Password(acc.IDS, acc.ID, acc.Pwd) != db.Pwd)
                {
                    BllLogin.AddLog(Request, acc, "密码有误");
                    return Json(new BllError { error = true, message = "错误：密码有误，请重新输入！" });
                }

                if (db.Fixed)
                {
                    //登录失败：添加日志
                    BllLogin.AddLog(Request, db, "用户被冻结");
                    return Json(new BllError { error = true, message = "错误：用户被冻结，请联系管理员！" });
                }
                else
                {
                    //登录成功：记录，并，退出
                    BllLogin.SaveLog(Session, Request, db, "登录成功");
                    return Json(new BllError { error = false, message = string.Format("用户：{0}成功登录！", db.IDS) });
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //用户登录：检测
        [HttpPost]
        public ActionResult Check()
        {
            var login = BllLogin.GetLogin(Session);
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
                return View("Main");
            }
        }

        //用户登录：退出
        [HttpPost]
        public ActionResult Logoff()
        {
            Session[Setting.SESSION_LOGIN] = null;
            return Json(new BllError { error = false, message = "退出成功" });
        }

        //用户主操作页面
        [HttpPost]
        public ActionResult Main()
        {
            return View();
        }


        //本机测试用的，
        public string Reg11()
        {
            CookieCollection cookies = null;
            //一、做Get请求网页
            string url = "http://localhost:13789/wei";
            using (HttpWebResponse resp = MyHtml.GetResponse(url))
            {
                cookies = resp.Cookies;
            }

            string posts = string.Empty;

            posts += "<xml><ToUserName><![CDATA[gh_23b54b508d0d]]></ToUserName> <FromUserName><![CDATA[olXXEjgAP_rorn1NXmYotM555WxmtIc]]></FromUserName> <CreateTime>1468467491</CreateTime> <MsgType><![CDATA[text]]></MsgType> <Content><![CDATA[信息登记#程佳伟#321284200508150254]]></Content> <MsgId>6307019849539175383</MsgId> </xml>";

            HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, posts, Encoding.UTF8);
            string html = MyHtml.GetHtml(postresp, Encoding.UTF8);
            return html;
        }


    }
}