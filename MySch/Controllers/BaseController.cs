using MySch.Bll;
using MySch.ModelsEx;
using MySch.Mvvm.Web.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            try
            {
                this.View(actionName).ExecuteResult(this.ControllerContext);
            }
            catch
            {
                this.View("~/Views/Account/Wrong1.cshtml").ExecuteResult(this.ControllerContext);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //添加ARoleAction
            var url = Setting.ActionUrl(filterContext).ToLower();
            var name = Setting.ActionName(filterContext).ToLower();

            //包含，则添加尾巴
            var dicts = new Dictionary<string, VmRoleName>();
            dicts.Add("add", new VmRoleName { IDS = "01", Name = "添加" });
            dicts.Add("edit", new VmRoleName { IDS = "02", Name = "修改" });
            dicts.Add("del", new VmRoleName { IDS = "03", Name = "删除" });
            dicts.Add("addtoken", new VmRoleName { IDS = "04", Name = "提交添加" });
            dicts.Add("edittoken", new VmRoleName { IDS = "05", Name = "提交修改" });
            dicts.Add("deltoken", new VmRoleName { IDS = "06", Name = "提交删除" });
            dicts.Add("datagrid", new VmRoleName { IDS = "07", Name = "数据列表" });
            var namex = dicts.ContainsKey(name) ? dicts[name] : null;
            //清除
            dicts.Clear();

            var role = (VmRoleAction)Session[Setting.SESSION_ROLE_ACTION];
            if (role != null)
            {
                if (namex != null)
                {
                    role.Add(namex.IDS, url, name + namex.Name);
                }
                else
                {
                    role.Add(name, url, name);
                }
            }


            ////权限检测
            //if (!MyRole.HasRole(Session, actid))
            //{
            //    filterContext.Result = Json(new MoError
            //    {
            //        error = true,
            //        message = "动作：权限不足，请联系管理员！"
            //    }, JsonRequestBehavior.AllowGet);

            //    return;
            //}
        }
    }

}