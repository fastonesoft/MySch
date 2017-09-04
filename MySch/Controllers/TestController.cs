using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySch.Dal;

namespace MySch.Controllers
{
    public class TestController : RoleController
    {
        private BaseContext context = new BaseContext();
        private BaseContext db = new BaseContext();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}