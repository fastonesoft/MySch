using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MySch.ModelsEx
{
    public class MySetting
    {
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

        public static string ControlerName(Controller control)
        {
            return control.RouteData.Values["controller"].ToString();
        }

        public static string ControllerUrl(Controller control)
        {
            return control.Request.RawUrl.ToString();
        }

        // GD
        public static string GetGD(string action, string id)
        {
            return GetMD5(action + "#" + GetMD5(id) + "#" + action);
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

        // IDS
        public static bool IDS(string ids)
        {
            long n = 0;
            if (long.TryParse(ids.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(ids.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                throw new Exception("身份证号：数字验证无法通过！");
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(ids.Remove(2)) == -1)
            {
                throw new Exception("身份证号：省份验证无法通过！");
            }
            string birth = ids.Substring(6, 8).Insert(6, "-").Insert(4, "-");

            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                throw new Exception("身份证号：生日验证无法通过！");
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

            char[] Ai = ids.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != ids.Substring(17, 1).ToLower())
            {
                throw new Exception("身份证号：校验码验证无法通过！");
            }
            return true;//符合GB11643-1999标准  
        }

        // Json
        public static T JsonsTo<T>(string jsons)
        {
            var javas = new JavaScriptSerializer();
            return javas.Deserialize<T>(jsons);
        }

        //时间戳
        public static int DateTimeToInt(DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt32((dateTime - start).TotalSeconds);
        }

    }
}