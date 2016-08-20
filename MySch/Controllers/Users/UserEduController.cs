using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class UserEduController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEdu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditEdu(string id)
        {
            try
            {
                var db = BllEdu.GetEntity<BllEdu>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelEdu(string id)
        {
            try
            {
                var db = BllEdu.GetEntity<BllEdu>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllEdu edu)
        {
            try
            {
                edu.ID = Guid.NewGuid().ToString("N");
                //添加
                edu.ToAdd(ModelState);
                return Json(edu);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllEdu edu)
        {
            try
            {
                edu.ToUpdate(ModelState);
                return Json(edu);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllEdu edu)
        {
            try
            {
                edu.ToDelete(ModelState);
                return Json(edu);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var res = BllEdu.GetPagesToDataGrid<BllEdu, int>(a => true, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


    }
}