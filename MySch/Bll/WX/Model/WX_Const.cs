using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_App
    {
        public const string AppID = "wx0f49a9991c53e2a4";
        public const string AppSecret = "a3663df809650680e518a1508c4156b8";
    }

    public class WX_Url
    {
        //网页授权，临时凭证
        public static string OAuthCode(string appid, string secret, string code)
        {
            return string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
        }

        //获取服务器授权
        public static string AccessToken(string appid, string secret)
        {
            return string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
        }

        //获取用户信息
        public static string UserInfor(string token, string openid)
        {
            return string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", token, openid);
        }

        //获取媒体文件
        public static string MediaFile(string token, string mediaID)
        {
            return string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", token, mediaID);
        }
    }
}