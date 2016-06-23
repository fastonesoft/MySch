using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserController : RoleController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}