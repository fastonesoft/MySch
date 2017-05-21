using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_AccessTokenOauth
    {
        public string openid { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public DateTime create_time { get; set; }

        public static WX_AccessTokenOauth GetSessionToken()
        {
            var token = (WX_AccessTokenOauth)HttpContext.Current.Session["wx_accesstokenoauth"];
            if (token != null)
            {
                if (WX_Time.TimeDiffer(DateTime.Now, token.create_time) < token.expires_in)
                {
                    //有，且不超时，直接返回
                    return token;
                }
            }
            //有问题，返回为空
            throw new Exception("授权页面已过期");
        }

        //缓存
        public void ToSession()
        {
            HttpContext.Current.Session["wx_accesstokenoauth"] = this;
        }
    }


}