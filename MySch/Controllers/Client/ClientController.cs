using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Client
{
    public class ClientController : RoleController
    {
        // GET: Client
        public ActionResult Index()
        {
            var login = BllLogin.GetLogin(Session);
            ViewBag.UserName = login.Name;
            //ViewBag.Contents = ";alsdflka;kjlsdfkjlasjl;kdf;lasdkljfa;klsdfl;kaslkdkjlaslkjasdfl;ksdal;sdfal;kj";
            return View();
        }
    }
}