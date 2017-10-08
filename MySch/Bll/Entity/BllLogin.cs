using MySch.Bll.Custom;
using MySch.Bll.WX.Model;
using MySch.Core;
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
        /// 登录：信息读取
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static WX_OAuserInfor GetLogin(HttpSessionStateBase session)
        {
            var infor = (WX_OAuserInfor)HttpContext.Current.Session["wx_userinfor"];
            if (infor == null) throw new Exception("页面请求已过期，重新登录");
            if (infor.IDS == string.Empty) throw new Exception("不是教师，不能进行这些操作");

            return infor;
        }

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //public static BrowserModel GetBrowser(HttpRequestBase req)
        //{
        //    return new BrowserModel(req.UserHostAddress, req.UserHostName, req.Browser.Browser);
        //}

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