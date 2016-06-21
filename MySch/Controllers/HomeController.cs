using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public class HomeController :BaseController
    {
        //用户：界面
        //不能使用POST方式
        public ActionResult Index()
        {
            ViewBag.UserName = MyLogin.GetLogin(Session).Name;
            return View();
        }
    }
}