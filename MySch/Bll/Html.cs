using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;

namespace MySch.Bll
{
    public class AutoLoginStud
    {
        public static void Logon(string url)
        {
            //打开登录
            CookieCollection cookies = MyHtml.GetCookies(url);
            Bitmap bitmap = MyHtml.GetBitmap(url, cookies);
        }
    }
}