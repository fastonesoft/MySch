using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Master
{
    public class MastSelfController : RoleController
    {
        // GET: MastSelf
        public ActionResult Index()
        {
            return View();
        }
    }
}