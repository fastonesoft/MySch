﻿using MySch.Bll.WX.Model;
using MySch.Core;
using System;
using System.IO;
using System.Text;
using System.Web;

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

            return CheckSignature("WX1979ToKen1979", au.timestamp, au.nonce, au.signature);
        }

        /// <summary>
        /// 读取消息请求体内容
        /// </summary>
        /// <returns></returns>
        public static string GetMessage()
        {
            //检测数据体
            Stream stream = HttpContext.Current.Request.InputStream;
            Byte[] bytes = new Byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(bytes).Replace(" ", "");
        }

        public static long GetTimestamp(DateTime time)
        {
            DateTime start = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - start).TotalSeconds;
        }

        public static long GetTimestamp()
        {
            return GetTimestamp(DateTime.Now);
        }

    }
}