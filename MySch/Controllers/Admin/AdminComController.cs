using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminComController : RoleAdminController
    {
        // GET: AdminCom
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Data()
        {
            var login = BllLogin.GetLogin(Session);


        }
    }
}