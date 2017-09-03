using MySch.Bll;
using MySch.Core;
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
        }
    }

}