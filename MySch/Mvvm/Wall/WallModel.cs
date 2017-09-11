using MySch.Bll.WX.Model;
using MySch.Core;
using MySch.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Wall
{
    public class WallOAuth
    {
        public string code { get; set; }
        public string state { get; set; }



        public WallOAuthInfor OAuthInfor()
        {
            var oaken = WX_Url.OAccessToKen(WX_Const.goneAppID, WX_Const.goneAppSecret, code);
            //读取用户信息
            var inforurl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", oaken.access_token, oaken.openid);
            var infors = HtmlHelp.GetHtml(inforurl, "UTF-8");
            return Jsons.JsonEntity<WallOAuthInfor>(infors);
        }

    }

    public class WallOAuthInfor
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string unionid { get; set; }
        public string headimgurl { get; set; }

        public string username { get; set; }
        public string pageurl { get; set; }
    }
}