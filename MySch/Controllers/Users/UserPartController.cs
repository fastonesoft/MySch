using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class UserPartController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditPart(string id)
        {
            try
            {
                var db = BllPart.GetEntity<BllPart>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelPart(string id)
        {
            try
            {
                var db = BllPart.GetEntity<BllPart>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllPart part)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                part.AccIDS = login.IDS;
                part.ID = Guid.NewGuid().ToString("N");

                //添加
                part.ToAdd(ModelState);
                return Json(part);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllPart part)
        {
            try
            {
                part.ToUpdate(ModelState);
                return Json(part);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllPart part)
        {
            try
            {
                part.ToDelete(ModelState);
                return Json(part);
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
                var login = BllLogin.GetLogin(Session);

                var res = BllPart.GetPagesToDataGrid<BllPart, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}