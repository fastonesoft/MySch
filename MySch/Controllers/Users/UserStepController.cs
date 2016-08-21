using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class UserStepController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStep()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditStep(string id)
        {
            try
            {
                var db = BllStep.GetEntity<BllStep>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelStep(string id)
        {
            try
            {
                var db = BllStep.GetEntity<BllStep>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllStep step)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                step.AccIDS = login.IDS;
                step.ID = Guid.NewGuid().ToString("N");

                //添加
                step.ToAdd(ModelState);
                return Json(step);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllStep step)
        {
            try
            {
                step.ToUpdate(ModelState);
                return Json(step);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllStep step)
        {
            try
            {
                step.ToDelete(ModelState);
                return Json(step);
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

                var res = BllStep.GetDataGridPages<BllStep, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}