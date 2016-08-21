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
    public class UserPartStController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPartSt()
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
        public ActionResult EditPartSt(string id)
        {
            try
            {
                var entity = BllPartSt.GetEntity<BllPartSt>(id);

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
        public ActionResult DelPartSt(string id)
        {
            try
            {
                var entity = BllPartSt.GetEntity<BllPartSt>(id);

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
        public ActionResult AddTokey(BllPartSt partst)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                partst.AccIDS = login.IDS;
                partst.ID = Guid.NewGuid().ToString("N");
                //添加
                partst.ToAdd(ModelState);
                //查询 视图数据
                var qpartst = QllPartSt.GetEntity<QllPartSt>(partst.ID);
                return Json(qpartst);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllPartSt partst)
        {
            try
            {
                //更新
                partst.ToUpdate(ModelState);
                //查询 视图数据
                var qpartst = QllPartSt.GetEntity<QllPartSt>(partst.ID);
                return Json(qpartst);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllPartSt partst)
        {
            try
            {
                //查询 视图数据 保存
                var qpartst = QllPartSt.GetEntity<QllPartSt>(partst.ID);
                //删除
                partst.ToDelete(ModelState);
                return Json(qpartst);
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
                var res = QllPartSt.GetDataGridPages<QllPartSt, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}