using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_UserInfor
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }

        //授权页面URL
        public string codePage { get; set; }
        //绑定的学生
        public string idc { get; set; }
        public string name { get; set; }

        public static WX_UserInfor GetSessionToken()
        {
            var infor = (WX_UserInfor)HttpContext.Current.Session["wx_userinfor"];
            if (infor != null)
            {
                return infor;
            }
            //有问题，返回为空
            throw new Exception("页面请求已过期");
        }

        //缓存
        public void ToSession()
        {
            HttpContext.Current.Session["wx_userinfor"] = this;
        }

        //检测是否绑定微信
        public void Check()
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.OpenID == openid);
                if (entity != null)
                {
                    name = entity.Name;
                    idc =  entity.IDC;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}