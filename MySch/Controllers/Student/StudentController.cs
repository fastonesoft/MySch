using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Student
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}