using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminYearController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}