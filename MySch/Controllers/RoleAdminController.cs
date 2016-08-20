﻿using MySch.Bll;
using MySch.Bll.Entity;
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
            string actid = Setting.ActionUrl(filterContext);

            //二、后台进入拦截
            if (BllLogin.GetLogin(Session) == null)
            {
                filterContext.Result = Json(new BllError
                {
                    error = true,
                    message = "动作：没有登录，不能进行相关操作！"
                }, JsonRequestBehavior.AllowGet);

                return;
            };
            if (BllLogin.GetLogin(Session).IDS != "admin")
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