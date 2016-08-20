using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllLogin : Bll<TLogin>
    {
        public string ID { get; set; }
        public int IDS { get; set; }
        public string Brower { get; set; }
        public string IP { get; set; }
        public System.DateTime loginTime { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string loginMsg { get; set; }



        /// <summary>
        /// 登录：成功记录
        /// </summary>
        /// <param name="session"></param>
        /// <param name="req"></param>
        /// <param name="acc"></param>
        /// <param name="message"></param>
        public static void SaveLog(HttpSessionStateBase session, HttpRequestBase req, BllAcc acc, string message)
        {
            LoginModel mo = new LoginModel(req.UserHostAddress, req.UserHostName, req.Browser.Browser, acc);
            session[Setting.SESSION_LOGIN] = mo;

            //记录成功登录时间
            var d = new BllLogin
            {
                ID = Guid.NewGuid().ToString("N"),
                Brower = req.Browser.Browser,
                IP = req.UserHostAddress,
                loginTime = DateTime.Now,
                Name = acc.IDS,
                Pwd = acc.Pwd,
                loginMsg = message
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
                Brower = req.Browser.Browser,
                IP = req.UserHostAddress,
                loginTime = DateTime.Now,
                Name = acc.IDS,
                Pwd = acc.Pwd,
                loginMsg = message
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
            var ips = BllLogin.GetEntitys<List<BllLogin>>(a => a.loginTime > logintime && a.IP == req.UserHostAddress);

            return ips.Count() >= 5;
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="id">帐号名</param>
        /// <param name="gd">帐号编号</param>
        /// <param name="password">密文MD5形式</param>
        /// <returns></returns>
        public static string Password(string id, string gd, string password)
        {
            return Setting.GetMD5(gd + "#" + id + "#" + password);
        }

    }
}