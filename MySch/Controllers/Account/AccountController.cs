using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.WX;
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
using System.Xml;

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
            var wxcrypt = new WXCryptMsg("WX1979ToKen", "wqbpIsxgyqLKWmnEbVlHmgTvj0BLSfNTBCAcxYhZRGf", "wx8e6ce1260ba9f214");

            /* 1. 对用户回复的数据进行解密。
            * 用户回复消息或者点击事件响应时，企业会收到回调消息，假设企业收到的推送消息：
            * 	POST /cgi-bin/wxpush? msg_signature=477715d11cdb4164915debcba66cb864d751f3e6&timestamp=1409659813&nonce=1372623149 HTTP/1.1
               Host: qy.weixin.qq.com
               Content-Length: 613
            *
            * 	<xml>
                   <ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName>
                   <Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt>
               </xml>
            */
            string sMsg = "";  //解析之后的明文
            int ret = 0;
            ret = wxcrypt.DecryptMsg("81c2c59af0287b521ea2dac028ea278cd807e41d", "1491321820", "1638150400", "<xml> <ToUserName><![CDATA[gh_23b54b508d0d]]></ToUserName> <Encrypt><![CDATA[AfiEHGxKoTkGWsY6hNsuxx/yLZ+/GhTroUKpE0bCAUwQmkuBxp+Y+Zidrp/MDuOKzzRUogL1hkT6iLOzW9PfdNWJjRtgL7UX9HW3XB9rrsgmBgvX1MUZGd9SEMV3nwMFc9ZmLQDbDOc/RiCsJme6U/c/YeO3tgsQstgk3Og830JUkKOVA4EGtG9cuC2jp6SjjLFA+Bm4x37XQGLRf3vmd2d9ThxoIxYx8abOrt8nw8GvuSY7LTnjx3qB9jkauEiseiShuf+oOJZPjnoDhestivjUjNDh3lmR/HHLp/rfUhJvwR2z+8WJ9Df7JDvX+UGQRu4QUaRYTY+5UvsVs81UaqWgsIhDXG7AaB8gp9toBIgUtRL0A3sG+u4uySbVrDW78aF4yujSqFgOzrjN+5JtMQsWHyZnScp7ZnGZo0zIze55nPgExXQbofR2zp1GBVC7xokoy8VIodvIGgFn3xlftA==]]></Encrypt> </xml> ", ref sMsg);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: Decrypt fail, ret: " + ret);
            }
            System.Console.WriteLine(sMsg);










            //公众平台上开发者设置的token, appID, EncodingAESKey
            string sToken = "QDG6eK";
            string sAppID = "wx5823bf96d3bd56c7";
            string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";

            WXCryptMsg wxcpt = new WXCryptMsg(sToken, sEncodingAESKey, sAppID);

            /* 1. 对用户回复的数据进行解密。
            * 用户回复消息或者点击事件响应时，企业会收到回调消息，假设企业收到的推送消息：
            * 	POST /cgi-bin/wxpush? msg_signature=477715d11cdb4164915debcba66cb864d751f3e6&timestamp=1409659813&nonce=1372623149 HTTP/1.1
               Host: qy.weixin.qq.com
               Content-Length: 613
            *
            * 	<xml>
                   <ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName>
                   <Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt>
               </xml>
            */
            string sReqMsgSig = "ef8cce14917c7c40fee8705dee5d1adfe65700cd";
            string sReqTimeStamp = "1491320141";
            string sReqNonce = "1222051110";
            string sReqData = "<xml>     <ToUserName><![CDATA[gh_23b54b508d0d]]></ToUserName>     <Encrypt><![CDATA[2yhM+oGNqnTkkgTKr6mbxOqS+HSH7WJkFKfczsxEzrapCNmzNisLlKPD2XJsFw1eZKTdpcdGCm8tkiN0RD3fHrYaaTzXHjPmtuzpjusHPPk4ZirWQ47Amo4zIz02fO/+/dS5hOO6qerd21aV9e8hpNpWmY0o8LhMGKaZ2qk1IKPWCrG58iDusy0lRIRj9BtG36t36mFSZ1WAiarQ7nhlufpoIDdngoJMadEzZhn7NUnFXOOtdy2faRxjqPFdphsMuhrHNXXMAUDioZR3F/YAau67RLzPTNCKnW4o+mxPtg2ndQ35ewVEnTx2e7hi5LBMIuBLsZ9tCAnqeeOToUFE2pzPJgO1+nhfZnpU2f8S4RMxob7v1RhBBOF2XRyYmSTWpjAsvX+QzDfmyzRpDPEP2xdjc1saBVQ7LrM6WBai87A=]]></Encrypt> </xml> ";
            string ssMsg = "";  //解析之后的明文
            int rset = 0;
            rset = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref ssMsg);
            if (rset != 0)
            {
                System.Console.WriteLine("ERR: Decrypt fail, ret: " + rset);
            }
            System.Console.WriteLine(sMsg);


            /*
             * 2. 企业回复用户消息也需要加密和拼接xml字符串。
             * 假设企业需要回复用户的消息为：
             * 		<xml>
             * 		<ToUserName><![CDATA[mycreate]]></ToUserName>
             * 		<FromUserName><![CDATA[wx5823bf96d3bd56c7]]></FromUserName>
             * 		<CreateTime>1348831860</CreateTime>
                    <MsgType><![CDATA[text]]></MsgType>
             *      <Content><![CDATA[this is a test]]></Content>
             *      <MsgId>1234567890123456</MsgId>
             *      </xml>
             * 生成xml格式的加密消息过程为：
             */
            string sRespData = "<xml><ToUserName><![CDATA[mycreate]]></ToUserName><FromUserName><![CDATA[wx582测试一下中文的情况，消息长度是按字节来算的396d3bd56c7]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[this is a test]]></Content><MsgId>1234567890123456</MsgId></xml>";
            string sEncryptMsg = ""; //xml格式的密文
            ret = wxcpt.EncryptMsg(sRespData, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
            System.Console.WriteLine("sEncryptMsg");
            System.Console.WriteLine(sEncryptMsg);

            /*测试：
             * 将sEncryptMsg解密看看是否是原文
             * */
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sEncryptMsg);
            XmlNode root = doc.FirstChild;
            string sig = root["MsgSignature"].InnerText;
            string enc = root["Encrypt"].InnerText;
            string timestamp = root["TimeStamp"].InnerText;
            string nonce = root["Nonce"].InnerText;
            string stmp = "";
            ret = wxcpt.DecryptMsg(sig, timestamp, nonce, sEncryptMsg, ref stmp);
            System.Console.WriteLine("stemp");
            System.Console.WriteLine(stmp + ret);



            return "";


            //CookieCollection cookies = null;
            ////一、做Get请求网页
            //string url = "http://localhost:13789/wei";
            //using (HttpWebResponse resp = MyHtml.GetResponse(url))
            //{
            //    cookies = resp.Cookies;
            //}

            //string posts = string.Empty;

            //posts += "<xml>     <ToUserName><![CDATA[gh_23b54b508d0d]]></ToUserName>     <Encrypt><![CDATA[9B2jNGtCNzO+bV6YdD7ENiTH5p/PkQuiWkTQoCHCLmrCcdPEXkq1uMW5q5uOLboB/jSrNfcy6D8L5vdRjoPPGQiB993uEUTr0wkes6N3DIO3w1coFzwVag9wqLRT8yqzYudHvHQzi7bHvOGYt5uszJkYDn3o3egAV0dTfojoYK5fBAtElWfiN15+bEC9Mt6zK8UBI2Uq4z1fJQs9+63D0wXK88K3++fDF1K6Goi//GlnvxfByHnLgUf4u07j9HA1rr7XOzCEQpvdb79YXTsPqjjE3utUuEZYTqURgtd+jMXApolYgkDb9DfaQCqFPG7Kr/P01YKEAZ0GI2PUCG1zSGvWBU2vBBESx1CVUoLrk9Gggv4KtDyd6kSr7ZiPlpbQhOBwdC1bKeqE6qmDLjQFt3uBlmOk2DQeCHswXa8m7c8=]]></Encrypt> </xml> ";

            //HttpWebResponse postresp = MyHtml.PostResponse(url, cookies, posts, Encoding.UTF8);
            //string html = MyHtml.GetHtml(postresp, Encoding.UTF8);
            //return html;
        }


    }
}