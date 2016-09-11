using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserStepController : RoleController
    {
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
                var db = BllStep.GetEntity<BllStep>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Del(string id)
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
        public ActionResult AddTokey(BllStep entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.AccIDS + entity.Value;

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
        public ActionResult EditTokey(BllStep entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;
                
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
        public ActionResult DelTokey(BllStep entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

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