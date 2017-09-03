using MySch.Bll;
using MySch.Bll.Custom;
using MySch.Bll.WX.Model;
using MySch.Core;
using MySch.Mvvm.Web.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Filter
{
    public class RoleRecFilterAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public string RoleTypeIDS { get; set; }
        public bool IsMenu { get; set; }
        public bool AutoNameExt { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                //登录过滤
                //登录是权限的前提
                //没有登录就不要提权限设置
                if (WX_OAuserInfor.HasNoSession())
                {
                    filterContext.Result = new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new ErrorMessage { error = true, message = "动作：没有登录，不能进行相关操作！" },
                    };
                }

                var namex = string.Empty;
                var name = Setting.ActionName(filterContext);
                if (AutoNameExt)
                {
                    //包含，则添加尾巴
                    var dicts = new Dictionary<string, string>();
                    dicts.Add("index", "界面");
                    dicts.Add("add", "添加");
                    dicts.Add("edit", "修改");
                    dicts.Add("del", "删除");
                    dicts.Add("addtoken", "提交添加");
                    dicts.Add("edittoken", "提交修改");
                    dicts.Add("deltoken", "提交删除");
                    dicts.Add("datagrid", "数据列表");
                    namex = dicts.ContainsKey(name) ? dicts[name] : namex;
                    //清除
                    dicts.Clear();
                }

                //添加ARoleAction
                var url = Setting.ActionUrl(filterContext).ToLower();
                var role = new VmRoleAction
                {
                    IDS = url,
                    Name = this.Name,
                    IsMenu = this.IsMenu,
                    RoleTypeIDS = this.RoleTypeIDS,
                };
                role.Add();
            }
            catch (Exception e)
            {
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new ErrorMessage { error = true, message = e.Message },
                };
            }
        }
    }
}