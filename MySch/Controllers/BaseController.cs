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
            var dicts = new Dictionary<string, string>();
            dicts.Add("index", "界面");
            dicts.Add("add", "添加");
            dicts.Add("edit",  "修改" );
            dicts.Add("del", "删除" );
            dicts.Add("addtoken",  "提交添加" );
            dicts.Add("edittoken", "提交修改" );
            dicts.Add("deltoken",  "提交删除" );
            dicts.Add("datagrid",  "数据列表" );
            var namex = dicts.ContainsKey(name) ? dicts[name] : string.Empty;
            //清除
            dicts.Clear();

            var role = (VmRoleAction)Session[Setting.SESSION_ROLE_ACTION];
            if (role != null)
            {
                role.Add(url, name + namex);
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