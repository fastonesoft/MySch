using MySch.Bll.WX.Model;
using MySch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.WX
{
    public class WallController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }


    }
}