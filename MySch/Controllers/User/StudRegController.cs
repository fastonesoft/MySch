using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class StudRegController : RoleController
    {
        //学生注册首页
        public ActionResult Index()
        {
            return View();
        }
    }
}