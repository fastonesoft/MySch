using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_OAuserInfor
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string unionid { get; set; }

        //用户类型
        public string username { get; set; }

        //授权页面URL
        public string codePage { get; set; }
        //绑定的学生
        public string idc { get; set; }
        public string name { get; set; }
        public string regno { get; set; }
        public bool exam { get; set; }

        //缓存
        public void ToSession()
        {
            HttpContext.Current.Session["wx_userinfor"] = this;
        }

        //检测用户类型
        public void CheckUser()
        {
            try
            {
                //教师检测
                var teach = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (teach != null)
                {
                    username = teach.Name;
                    return;
                }
                //家长检测
                var parent = DataCRUD<Student>.Entity(a => a.RegUID == unionid);
                if (parent != null)
                {
                    username = parent.Name + " 家长";
                    return;
                }
                //游客
                username = "游客";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //检测是否绑定微信
        public void BindingStud()
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.RegUID == unionid);
                if (entity != null)
                {
                    idc = entity.IDC;
                    name = entity.Name;
                    exam = entity.Examed;
                    regno = entity.RegNo;
                }
                //不需要提示出错
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //////////////////////////////////////////////

        public static WX_OAuserInfor GetFromSession()
        {
            var infor = (WX_OAuserInfor)HttpContext.Current.Session["wx_userinfor"];
            if (infor != null)
            {
                return infor;
            }
            //有问题，返回为空
            throw new Exception("页面请求已过期");
        }

        public static bool HasNoSession()
        {
            return (WX_OAuserInfor)HttpContext.Current.Session["wx_userinfor"] == null ? true : false;
        }


    }
}