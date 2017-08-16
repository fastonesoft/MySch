using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminController : Controller
    {
        //管理员：首页
        [HttpPost]
        public ActionResult Index()
        {
            ViewBag.UserName = BllLogin.GetLogin(Session).Name;
            return View();
        }
    }
}