using MySch.Bll;
using MySch.Bll.Custom;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        //用户列表：界面
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Del(string id)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }
    }
}