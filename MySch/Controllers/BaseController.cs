using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public class BaseController : Controller
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
            //权限检测
            if (!MyRole.HasRole(Session, actid))
            {
                filterContext.Result = Json(new MoError
                {
                    error = true,
                    message = "动作：权限不足，请联系管理员！"
                }, JsonRequestBehavior.AllowGet);

                return;
            }
        }
    }
}