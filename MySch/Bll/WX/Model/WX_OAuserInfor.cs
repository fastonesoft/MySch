using MySch.Core;
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
        public string IDS { get; set; }
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
        public string examuid { get; set; }
        public string rexamuid { get; set; }

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
                    IDS = teach.IDS;
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
                    examuid = entity.ExamUID;
                    rexamuid = entity.ExamUIDe;
                }
                //不需要提示出错
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //检测是否通过审核

        public bool CheckPassed()
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);

                if (entity == null) throw new Exception("未注册用户，无法使用");
                if (!entity.Passed) throw new Exception("未通过审核，无法使用");
                if (entity.Fixed) throw new Exception("帐号已冻结，无法使用");

                return entity.Passed;
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
                    RoleGroupIDS = 0,
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

        public void ValidUser()
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (entity == null) return;

                var valid = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", unionid, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));
                if (entity.Valided != valid) throw new Exception("帐号数据异常");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ExamUser(string unionid)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (entity == null) throw new Exception("异常数据查询");

                //更新
                entity.Passed = true;
                entity.Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));
                DataCRUD<TAcc>.Update(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object ExamGrid(string unionid, int page, int rows)
        {
            try
            {
                int gets, total;
                //读取：分页实体对象
                var pages = DataCRUD<TAcc>.TakePage<DateTime>(a => a.ParentID == unionid, a => a.RegTime, page, rows, out gets, out total);

                //转换：实体对象 - 表示数据
                var pages_bll = Jsons.JsonEntity<List<TAcc>>(pages);

                //输出：转换成DataGrid的数据
                return EasyUI<TAcc>.DataGrids(pages_bll, total);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static TAcc PassExam(string examunion, string regunion)
        {
            try
            {
                if (examunion != "o47ZhvxoQA9QOOgDSZ5hGaea4xdI") throw new Exception("不是管理员，不好操作");

                var entity = DataCRUD<TAcc>.Entity(a => a.ID == regunion);
                if (entity == null) throw new Exception("数据查询异常");

                entity.Passed = true;
                entity.Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));

                DataCRUD<TAcc>.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static TAcc UnExam(string examunion, string regunion)
        {
            try
            {
                if (examunion != "o47ZhvxoQA9QOOgDSZ5hGaea4xdI") throw new Exception("不是管理员，不好操作");

                var entity = DataCRUD<TAcc>.Entity(a => a.ID == regunion);
                if (entity == null) throw new Exception("数据查询异常");

                entity.Passed = false;
                entity.Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));

                DataCRUD<TAcc>.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static TAcc Fixed(string examunion, string regunion)
        {
            try
            {
                if (examunion != "o47ZhvxoQA9QOOgDSZ5hGaea4xdI") throw new Exception("不是管理员，不好操作");

                var entity = DataCRUD<TAcc>.Entity(a => a.ID == regunion);
                if (entity == null) throw new Exception("数据查询异常");

                entity.Fixed = true;
                entity.Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));

                DataCRUD<TAcc>.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static TAcc UnFixed(string examunion, string regunion)
        {
            try
            {
                if (examunion != "o47ZhvxoQA9QOOgDSZ5hGaea4xdI") throw new Exception("不是管理员，不好操作");

                var entity = DataCRUD<TAcc>.Entity(a => a.ID == regunion);
                if (entity == null) throw new Exception("数据查询异常");

                entity.Fixed = false;
                entity.Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));

                DataCRUD<TAcc>.Update(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //班主任，暂时放5
        public static void CheckExamRoleMs(string unionid)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (entity == null) throw new Exception("不是教师");

                if (!entity.Passed) throw new Exception("帐号未通过审核");
                if (entity.Fixed) throw new Exception("帐号已冻结");
                //班主任，设置2
                if (entity.RoleGroupIDS < 2) throw new Exception("没有审核权限");

                //检查有没有班级
                var ban = DataCRUD<TBan>.Entity(a => a.MasterIDS == unionid);
                if (ban == null) throw new Exception("你好像没有带班主任");

                var valid = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));
                if (valid != entity.Valided) throw new Exception("帐号数据异常");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //备课组长，权限3
        public static void CheckExamRoleBk(string unionid)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (entity == null) throw new Exception("不是教师");

                if (!entity.Passed) throw new Exception("帐号未通过审核");
                if (entity.Fixed) throw new Exception("帐号已冻结");
                if (entity.RoleGroupIDS < 3) throw new Exception("没有审核权限");

                var valid = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));
                if (valid != entity.Valided) throw new Exception("帐号数据异常");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //年级组长，权限4
        public static void CheckExamRoleGo(string unionid)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (entity == null) throw new Exception("不是教师");

                if (!entity.Passed) throw new Exception("帐号未通过审核");
                if (entity.Fixed) throw new Exception("帐号已冻结");
                if (entity.RoleGroupIDS < 4) throw new Exception("没有审核权限");

                var valid = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", entity.ID, entity.RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));
                if (valid != entity.Valided) throw new Exception("帐号数据异常");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}