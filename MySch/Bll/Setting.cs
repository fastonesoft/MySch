using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll
{
    public class Setting
    {
        //登录帐号类型：老师，学生？
        public const string SESSION_LOGIN_TYPE = "LoginType";
        public const string SESSION_VALIDATE_CODE_LOGIN = "ValidateImageCodeLogin";

        /// 验证码长度
        public const int SESSION_VALIDATE_LEN = 6;
        /// 登录用户名存放地
        public const string SESSION_LOGIN = "SessionLogin";
        /// 临时保存是否管理员的标志
        public const string SESSION_TEMP_ISMASTER = "SessionTempIsMaster";

        // Action
        public static string ActionUrl(Controller control)
        {
            string con = control.RouteData.Values["controller"].ToString();
            string act = control.RouteData.Values["action"].ToString();
            return string.Format("/{0}/{1}", con, act);
        }

        public static string ActionUrl(ActionExecutingContext filterContext)
        {
            string acname = filterContext.ActionDescriptor.ActionName;
            string coname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            return string.Format("/{0}/{1}", coname, acname);
        }
        public static string ActionUrl(ActionExecutedContext filterContext)
        {
            string acname = filterContext.ActionDescriptor.ActionName;
            string coname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            return string.Format("/{0}/{1}", coname, acname);
        }
        public static string ActionName(ActionExecutedContext filterContext)
        {
            return filterContext.ActionDescriptor.ActionName;
        }
        public static string ActionName(ActionExecutingContext filterContext)
        {
            return filterContext.ActionDescriptor.ActionName;
        }

        public static string Url(HttpRequestBase request)
        {
            return request.Url.ToString();
        }

        public static string ControlerName(Controller control)
        {
            return control.RouteData.Values["controller"].ToString();
        }

        public static string ControllerUrl(Controller control)
        {
            return control.Request.RawUrl.ToString();
        }

        public static string GetSHA1(string str)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);

            return str_sha1_out.Replace("-", "");
        }

        // MD5
        public static string GetMD5(string str)
        {
            return GetMD5(str, Encoding.UTF8);
        }

        public static string GetMD5(string str, Encoding charEncoder)
        {
            byte[] bytesOfStr = MD5Raw(str, charEncoder);
            int bLen = bytesOfStr.Length;

            StringBuilder pwdBuilder = new StringBuilder(32);
            for (int i = 0; i < bLen; i++)
            {
                pwdBuilder.Append(bytesOfStr[i].ToString("x2"));
            }
            return pwdBuilder.ToString();
        }

        private static byte[] MD5Raw(string str, Encoding charEncoder)
        {
            System.Security.Cryptography.MD5 MD5 = System.Security.Cryptography.MD5.Create();
            return MD5.ComputeHash(charEncoder.GetBytes(str));
        }

        //时间 -> 整数
        public static int DateTimeToInt(DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt32((dateTime - start).TotalSeconds);
        }

    }
}