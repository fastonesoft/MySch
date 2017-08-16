using MySch.Bll;
using MySch.Mvvm.Web.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Filter
{
    public class RoleFilterAttribute : ActionFilterAttribute
    {
        public string Title { get; set; }
        public string Ext { get; set; }
        public bool IsMenu { get; set; }
        public string RoleTypeIDS { get; set; }
        public bool 

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //添加ARoleAction
            var url = Setting.ActionUrl(filterContext).ToLower();
            var role = new VmRoleAction {
                IDS = url,
                Title = this.Title,
                Ext = this.Ext,
                IsMenu = this.IsMenu,
                RoleTypeIDS = this.RoleTypeIDS,
            };
            role.Add();
            filterContext.Result = JsonResult(new MoError
            {
                error = true,
                message = "动作：权限不足，请联系管理员！"
            }, JsonRequestBehavior.AllowGet);

        }
    }
}