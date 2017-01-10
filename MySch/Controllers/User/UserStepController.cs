using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.View;
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
        public ActionResult Add(string id)
        {
            var parts = BllPart.GetEntitys<BllPart>(a => a.IDS == id).OrderBy(a => a.IDS);
            ViewBag.Parts = EasyUICombo.ToComboJsons(parts, "IDS", "Name", id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var db = BllStep.GetEntity<BllStep>(id);

                var parts = BllPart.GetEntitys<BllPart>(a => a.IDS == db.PartIDS).OrderBy(a => a.IDS);
                ViewBag.Parts = EasyUICombo.ToComboJsons(parts, "IDS", "Name", db.PartIDS);

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

                var parts = BllPart.GetEntitys<BllPart>(a => a.IDS == db.PartIDS).OrderBy(a => a.IDS);
                ViewBag.Parts = EasyUICombo.ToComboJsons(parts, "IDS", "Name", db.PartIDS);

                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllStep entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.PartIDS + entity.Value;

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
        public ActionResult EditToken(BllStep entity)
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
        public ActionResult DelToken(BllStep entity)
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
        public ActionResult MenuTree(string id = null)
        {
            var entitys = BllPart.GetEntitys<BllPart>(a => a.Fixed == false).OrderBy(a => a.IDS);
            var res = EasyUITree.ToTree(entitys, "IDS", "Name", "open", "Part");
            return Json(res);
        }

        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {

                var login = BllLogin.GetLogin(Session);

                var res = VStep.GetDataGridPages(a => a.AccIDS == login.IDS && a.PartIDS == id, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}