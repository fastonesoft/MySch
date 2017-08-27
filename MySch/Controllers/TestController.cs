using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers
{
    public class TestController : RoleController
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}