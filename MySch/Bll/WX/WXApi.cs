using MySch.Bll;
using MySch.Bll.Func;
using MySch.Bll.WX.Model;
using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace MySch.Bll.WX
{
    public class WXApi
    {
        /// <summary>
        /// 签名：参数方式验证
        /// </summary>
        /// <param name="token">与第三方共同持有</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce"></param>
        /// <param name="signature">加密后的数据</param>
        /// <returns></returns>
        public static bool CheckSignature(string token, string timestamp, string nonce, string signature)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = Setting.GetSHA1(tmpStr);
            tmpStr = tmpStr.ToLower();

            return tmpStr == signature ? true : false;
        }

        /// <summary>
        /// 签名：对象封装方式验证
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public static bool CheckSignature(WX_Author au)
        {
            if (au == null) return false;

            return CheckSignature("WX1979ToKen", au.timestamp, au.nonce, au.signature);
        }

        /// <summary>
        /// 读取消息请求体内容
        /// </summary>
        /// <returns></returns>
        public static string GetBodyHtml()
        {
            //检测数据体
            Stream stream = HttpContext.Current.Request.InputStream;
            Byte[] bytes = new Byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(bytes).Replace(" ", "");
        }
    }


}