using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserPartStepController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add()
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = EasyCombo.ToComboJsons<BllPart>(parts, null);
                ViewBag.Steps = EasyCombo.ToComboJsons<BllStep>(steps, null);

                return View();
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
                var entity = BllStep.GetEntity<BllStep>(id);

                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = EasyCombo.ToComboJsons<BllPart>(parts, entity.PartIDS);
                ViewBag.Steps = EasyCombo.ToComboJsons<BllStep>(steps, entity.IDS);

                return View(entity);
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
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.PartIDS + entity.Value;

                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var qentity = VStep.GetEntity(a => a.ID == entity.ID);
                return Json(qentity);
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
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //查询 视图数据 保存
                var qentity = VStep.GetEntity(a => a.ID == entity.ID);
                //删除
                entity.ToDelete(ModelState);
                return Json(qentity);
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
                var res = VStep.GetDataGridPages(a => a.AccIDS == login.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GradeTree(string id = null, string memo = null)
        {
            var login = BllLogin.GetLogin(Session);
            //校区：所有
            if (id == null)
            {
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS);
                var res = EasyTree.ToTree<BllPart>(entitys, "IDS", "Name", "closed", "Part");
                return Json(res);
            }
            else
            {
                //年级：所有
                if (memo == "Part")
                {
                    var entitys =VStep.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id);
                    var res = EasyTree.ToTree(entitys, "IDS", "StepName", "closed", "Step");
                    return Json(res);
                }
                else
                {
                    if(memo == "Step")
                    {
                        var entitys = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.StepIDS == id);
                        var res = EasyTree.ToTree(entitys, "IDS", "TreeName", "closed", "Grade");
                        return Json(res);
                    }
                    else
                    {
                        //班级：所有
                        var entitys = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.GradeIDS == id);
                        var res = EasyTree.ToTree(entitys, "IDS", "TreeName", "open", "Class");
                        return Json(res);
                    }
                }
            }
        }
    }
}