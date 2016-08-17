using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public abstract class RoleAdminController :BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限基类要做的事：
            base.OnActionExecuting(filterContext);

            //一、获取动作名称
            string actid = MySetting.ActionUrl(filterContext);

            //二、后台进入拦截
            if (MyLogin.GetLogin(Session) == null)
            {
                filterContext.Result = Json(new ErrorModel
                {
                    error = true,
                    message = "动作：没有登录，不能进行相关操作！"
                }, JsonRequestBehavior.AllowGet);

                return;
            };
            if (MyLogin.GetLogin(Session).IDS != "admin")
            {
                filterContext.Result = Json(new ErrorModel
                {
                    error = true,
                    message = "未授权，无法进入后台！"
                }, JsonRequestBehavior.AllowGet);

                return;
            };
        }
    }
}