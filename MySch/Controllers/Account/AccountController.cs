using MySch.Bll;
using MySch.Bll.Action;
using MySch.Bll.Entity;
using MySch.Bll.Func;
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

            //string ee = BllLogin.Password("32128402", "stone.2.net");
            //return Content(ee);

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
            //return Content(AutoXue.Login("http://58.213.155.172/uids/index.jsp",
            //    "http://58.213.155.172/uids/genImageCode?rnd=",
            //    "http://58.213.155.172/uids/login!login.action",
            //    "http://58.213.155.172/studman2/studman/stud_report/preStudentReportNewAct.action", "c32128441402", "==QTuhWMaVlWoN2MSFXYR1TP"));

            return View();
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

        //用户登录：数据提交
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(BllAcc acc)
        {
            try
            {
                //帐号类型检测
                BllError res = IDC.IDS(acc.IDS);
                if (!res.error)
                {
                    //用学生身份登录：IDS是身份证，Pwd是身份证后6位
                    var stud = BllStudent.GetEntity<BllStudent>(a => a.IDC == acc.IDS);
                    if (stud == null) return Json(new BllError { error = true, message = "错误：该身份证号没有注册！" });
                    acc.Name = stud.Name;

                    //密码处理
                    var pwd = acc.IDS.Substring(acc.IDS.Length - 6, 6);
                    pwd = BllLogin.Password(acc.IDS, pwd);
                    pwd = BllLogin.Repassword(acc.ID, pwd);
                    if (pwd == acc.Pwd)
                    {                        
                        //登录成功：记录，并，退出
                        BllLogin.SaveLog(Session, Request, acc, "登录成功", true);
                        return Json(new BllError { error = false, message = string.Format("用户：{0}成功登录！", acc.IDS) });
                    }
                    else
                    {
                        BllLogin.AddLog(Request, acc, "密码有误");
                        return Json(new BllError { error = true, message = "错误：密码有误，请重新输入！" });
                    }
                }

                //封IP
                if (BllLogin.FixedOfIP(Request, acc))
                {
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

                //2、检测密码是否正确
                if (BllLogin.Repassword(acc.ID, db.Pwd) != acc.Pwd)
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
                    BllLogin.SaveLog(Session, Request, db, "登录成功", false);
                    return Json(new BllError { error = false, message = string.Format("用户：{0}成功登录！", db.IDS) });
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
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