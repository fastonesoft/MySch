using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MySch.Models
{
    public class MyWxApi
    {
        #region 验证微信签名
        public static bool CheckSignature(string token, string timestamp, string nonce, string signature)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = "";//FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            return tmpStr == signature ? true : false;
        }

        public static bool CheckSignature(WX_AuthorModel au)
        {
            if (au == null) return false;

            return CheckSignature("WX1979ToKen", au.timestamp, au.nonce, au.signature);
        }
        
        #endregion

    }
}