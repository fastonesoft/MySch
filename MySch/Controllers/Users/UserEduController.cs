using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Users
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
        public ActionResult AddTokey(BllEdu entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                entity.AccIDS = login.IDS;
                entity.ID = Guid.NewGuid().ToString("N");

                //添加
                entity.ToAdd(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllEdu entity)
        {
            try
            {
                entity.ToUpdate(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllEdu entity)
        {
            try
            {
                entity.ToDelete(ModelState);
                return Json(entity);
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

                var res = BllEdu.GetDataGridPages<BllEdu, string >(a => a.AccIDS == login.IDS,  a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


    }
}