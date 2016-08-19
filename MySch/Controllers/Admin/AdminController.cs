using MySch.Bll;
using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminController : RoleAdminController
    {
        //管理员：首页
        [HttpPost]
        public ActionResult Index()
        {
            ViewBag.UserName = MyLogin.GetLogin(Session).Name;
            return View();
        }
    }
}