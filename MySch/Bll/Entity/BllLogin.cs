using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllLogin : BllEntity<TLogin>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string IP { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Brower { get; set; }
        public string LoginMsg { get; set; }
        public System.DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录：成功记录
        /// </summary>
        /// <param name="session"></param>
        /// <param name="req"></param>
        /// <param name="acc"></param>
        /// <param name="message"></param>
        public static void SaveLog(HttpSessionStateBase session, HttpRequestBase req, BllAcc acc, string message, bool student)
        {
            var mo = new LoginModel(req.UserHostAddress, req.UserHostName, req.Browser.Browser, acc, student);
            session[Setting.SESSION_LOGIN] = mo;

            //记录成功登录时间
            var d = new BllLogin
            {
                ID = Guid.NewGuid().ToString("N"),
                IDS = Guid.NewGuid().ToString("N"),
                IP = req.UserHostAddress,
                Name = acc.IDS,
                Pwd = acc.Pwd,
                Brower = req.Browser.Browser,
                LoginMsg = message,
                LoginTime = DateTime.Now,
            };
            d.ToAdd();
        }

        /// <summary>
        /// 登录：日志添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="acc"></param>
        /// <param name="message"></param>
        public static void AddLog(HttpRequestBase req, BllAcc acc, string message)
        {
            var db = new BllLogin
            {
                ID = Guid.NewGuid().ToString("N"),
                IDS = Guid.NewGuid().ToString("N"),
                IP = req.UserHostAddress,
                Name = acc.IDS,
                Pwd = acc.Pwd,
                LoginMsg = message,
                Brower = req.Browser.Browser,
                LoginTime = DateTime.Now,
            };
            db.ToAdd();
        }

        /// <summary>
        /// 登录：信息读取
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static LoginModel GetLogin(HttpSessionStateBase session)
        {
            return session[Setting.SESSION_LOGIN] as LoginModel;
        }

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static BrowserModel GetBrowser(HttpRequestBase req)
        {
            return new BrowserModel(req.UserHostAddress, req.UserHostName, req.Browser.Browser);
        }

        /// <summary>
        /// 登录：检测当前IP是否被封
        /// </summary>
        /// <param name="req"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public static bool FixedOfIP(HttpRequestBase req, BllAcc acc)
        {
            //同IP登录错误不得超过10次
            //五分钟超过10次，封停
            var logintime = DateTime.Now.AddMinutes(-5);
            var ips = BllLogin.GetEntitys<List<BllLogin>>(a => a.LoginTime > logintime && a.IP == req.UserHostAddress);

            return ips.Count() >= 5;
        }

        /// <summary>
        /// 明文加密
        /// </summary>
        /// <param name="ids">帐号</param>
        /// <param name="pwd">密码明文</param>
        /// <returns></returns>
        public static string Password(string ids, string pwd)
        {
            return Setting.GetMD5(Setting.GetMD5(pwd) + ids);
        }

        public static string Repassword(string id, string pwdEncoded)
        {
            return Setting.GetMD5(pwdEncoded + id);
        }
    }
}