using MySch.Bll.Func;
using MySch.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_OAuth_Base
    {
        public string state { get; set; }
    }

    public class WX_OAuth : WX_OAuth_Base
    {
        public string code { get; set; }

        public WX_OAuserInfor GoneLogin()
        {
            var oaken = WX_Url.OAccessToKen(WX_Const.goneAppID, WX_Const.goneAppSecret, code);
            //读取用户信息
            var infor = WX_Url.OAuserInfor(oaken.access_token, oaken.openid);

            return infor;
        }


        //要考虑下次是否把这个功能拆并了
        public WX_OAuserInfor WebLogin()
        {
            var oaken = WX_Url.OAccessToKen(WX_Const.webAppID, WX_Const.webAppSecret, code);
            //读取用户信息
            var infor = WX_Url.OAuserInfor(oaken.access_token, oaken.openid);

            return infor;
        }
    }
}