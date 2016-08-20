using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public abstract class RoleController :BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限基类要做的事：
            base.OnActionExecuting(filterContext);

            //一、获取动作名称
            string actid = Setting.ActionUrl(filterContext);
            //二、检测是否进行权限过滤，需要，则检查当前用户是否具备当前动作的权限，没有，则转到：出错页
            //登录检测
            if (BllLogin.GetLogin(Session) == null)
            {
                filterContext.Result = Json(new BllError
                {
                    error = true,
                    message = "动作：没有登录，不能进行相关操作！"
                }, JsonRequestBehavior.AllowGet);

                return;
            }
            ////权限检测
            //if (!MyRole.HasRole(Session, actid))
            //{
            //    filterContext.Result = Json(new ErrorModel
            //    {
            //        error = true,
            //        message = "动作：权限不足，请联系管理员！"
            //    }, JsonRequestBehavior.AllowGet);

            //    return;
            //}
        }
    }
}