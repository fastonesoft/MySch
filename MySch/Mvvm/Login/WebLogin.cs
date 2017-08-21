using MySch.Bll.WX.Model;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Login
{
    public class WebOAuth
    {
        public string state { get; set; }
        public string code { get; set; }
        public void Login()
        {
            //授权登录
            var oaken = WX_Url.OAccessToKen(WX_Const.webAppID, WX_Const.webAppSecret, code);
            //读取信息
            var infor = WX_Url.WebOAuserInfor(oaken.access_token, oaken.openid);
            //
            infor.CheckUser();
            infor.ToSession();
        }
    }

    public class WebOAuthInfor
    {
        //登录信息
        public string openid { get; set; }
        public string nickname { get; set; }
        public string unionid { get; set; }

        //用户姓名
        public string username { get; set; }
        public bool isparents { get; set; }

        //检测用户类型
        public void CheckUser()
        {
            try
            {
                var acc = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (acc == null) {
                    //不是教师
                    var student = DataCRUD<Student>.Entity(a => a.RegUID == unionid);
                    //
                    username = student == null ? "游客" : student.Name + " 的家长";
                    isparents = true;
                }
                else
                {
                    //是注册用户
                    username = acc.Name;
                    isparents = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToSession()
        {
            HttpContext.Current.Session["web_userinfor"] = this;
        }
    }
}