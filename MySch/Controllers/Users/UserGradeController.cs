using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class UserGradeController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGrade()
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = Combo.ToComboJsons<BllPart>(parts, null);
                ViewBag.Steps = Combo.ToComboJsons<BllStep>(steps, null);

                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult EditGrade(string id)
        {
            try
            {
                var entity = BllGrade.GetEntity<BllGrade>(id);

                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = Combo.ToComboJsons<BllPart>(parts, entity.PartIDS);
                ViewBag.Steps = Combo.ToComboJsons<BllStep>(steps, entity.StepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelGrade(string id)
        {
            try
            {
                var entity = BllGrade.GetEntity<BllGrade>(id);

                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = Combo.ToComboJsons<BllPart>(parts, entity.PartIDS);
                ViewBag.Steps = Combo.ToComboJsons<BllStep>(steps, entity.StepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllGrade partstep)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                partstep.AccIDS = login.IDS;
                partstep.ID = Guid.NewGuid().ToString("N");
                //添加
                partstep.ToAdd(ModelState);
                //查询 视图数据
                var qpartstep = QllGrade.GetEntity<QllGrade>(partstep.ID);
                return Json(qpartstep);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllGrade partstep)
        {
            try
            {
                //更新
                partstep.ToUpdate(ModelState);
                //查询 视图数据
                var qpartstep = QllGrade.GetEntity<QllGrade>(partstep.ID);
                return Json(qpartstep);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllGrade partstep)
        {
            try
            {
                //查询 视图数据 保存
                var qpartstep = QllGrade.GetEntity<QllGrade>(partstep.ID);
                //删除
                partstep.ToDelete(ModelState);
                return Json(qpartstep);
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
                var res = QllGrade.GetDataGridQPages(a => a.AccIDS == login.IDS,  page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}