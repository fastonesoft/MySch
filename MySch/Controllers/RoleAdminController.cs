using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public abstract class RoleAdminController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限基类要做的事：
            base.OnActionExecuting(filterContext);

            //一、获取动作名称
            string actid = Setting.ActionUrl(filterContext);

            //二、后台进入拦截
            var login = BllLogin.GetLogin(Session);
            if (login == null)
            {
                filterContext.Result = Json(new BllError
                {
                    error = true,
                    message = "动作：没有登录，不能进行相关操作！"
                }, JsonRequestBehavior.AllowGet);

                return;
            };
            if (login.IDS != "admin" && login.IDS != "32128402")
            {
                filterContext.Result = Json(new BllError
                {
                    error = true,
                    message = "权限：未经授权，无法进入后台！"
                }, JsonRequestBehavior.AllowGet);

                return;
            };
        }
    }
}