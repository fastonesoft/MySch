using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.ModelsEx
{
    public class MyLogin
    {
        #region 登录：成功记录 public static void SaveLog(HttpSessionStateBase se, HttpRequestBase re, Acc acc)
        public static void SaveLog(HttpSessionStateBase se, HttpRequestBase re, TAcc acc, string message)
        {
            LoginModel mo = new LoginModel(re.UserHostAddress, re.UserHostName, re.Browser.Browser, acc);
            se[MySetting.SESSION_LOGIN] = mo;

            //记录成功登录时间
            var d = new TLogin
            {
                Brower = re.Browser.Browser,
                IP = re.UserHostAddress,
                loginTime = DateTime.Now,
                Name = acc.ID,
                Pwd = acc.Pwd,
                loginMsg = message
            };
            DataADU<TLogin>.Add(d);
        }
        #endregion

        #region 登录：日志添加 public static void AddLog(HttpRequestBase re, Acc acc, string message)
        public static void AddLog(HttpRequestBase re, TAcc acc, string message)
        {
            var db = new TLogin
            {
                Brower = re.Browser.Browser,
                IP = re.UserHostAddress,
                loginTime = DateTime.Now,
                Name = acc.ID,
                Pwd = acc.Pwd,
                loginMsg = message
            };
            DataADU<TLogin>.Add(db);
        }
        #endregion

        #region 登录：信息读取 public static MoLogin GetLogin(HttpSessionStateBase se)
        public static LoginModel GetLogin(HttpSessionStateBase se)
        {
            return se[MySetting.SESSION_LOGIN] as LoginModel;
        }
        #endregion

        #region 登录：客户端信息 public static MoBrowser GetBrowser(HttpRequestBase re)
        public static BrowserModel GetBrowser(HttpRequestBase re)
        {
            return new BrowserModel(re.UserHostAddress, re.UserHostName, re.Browser.Browser);
        }
        #endregion

        #region 登录：检测当前IP是否被封 public static bool FixedOfIP(HttpRequestBase re, Acc acc)
        public static bool FixedOfIP(HttpRequestBase re, TAcc acc)
        {
            //同IP登录错误不得超过10次
            //五分钟超过10次，封停
            var logintime = DateTime.Now.AddMinutes(-5);
            var ip = DataQuery<TLogin>.Expression(a => a.loginTime > logintime && a.IP == re.UserHostAddress);
            if (ip.Count() >= 5)
            {
                return true;
            }
            else
            {
                //没有超过，放行
                return false;
            }
        }
        #endregion

        public static string Password(string id, string gd, string password)
        {
            return MySetting.GetMD5(gd + "#" + id + "#" + password);
        }


    }
}