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
        public bool isteach { get; set; }
        public bool istudent { get; set; }

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

        //检测用户类型web
        public void CheckUser()
        {
            try
            {
                //教师检测
                var teach = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (teach != null)
                {
                    isteach = true;
                    istudent = false;
                    username = teach.Name;
                    return;
                }
                //家长检测
                var parent = DataCRUD<Student>.Entity(a => a.RegUID == unionid);
                if (parent != null)
                {
                    isteach = false;
                    istudent = true;
                    idc = parent.IDC;
                    name = parent.Name;
                    exam = parent.Examed;
                    regno = parent.RegNo;
                    username = parent.Name + " 家长";
                    return;
                }
                //游客
                isteach = false;
                istudent = false;
                username = "游客";

                //缓存
                ToSession();
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

        public void AddTeach(string name)
        {
            try
            {
                var count = DataCRUD<TAcc>.Count(a => a.ID == unionid);
                if (count > 0) throw new Exception("已经是注册用户");

                var teach = new TAcc
                {
                    ID = unionid,
                    IDS = Guid.NewGuid().ToString("N"),
                    Name = name,
                    NickName = nickname,
                    AccTypeIDS = 0,
                    RegTime = DateTime.Now,
                    Passed = false,
                    Fixed = false,
                    Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", unionid, 0, "false", "false")),
                    ParentID = "o47ZhvxoQA9QOOgDSZ5hGaea4xdI",
                };

                DataCRUD<TAcc>.Add(teach);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string  ExamUser()
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                var valid = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", unionid, entity.AccTypeIDS, entity.Passed.ToString(), entity.Fixed.ToString()));

                return valid;

                //if (entity.Valided != valid) throw new Exception(valid);
                //"帐号数据异常"
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}