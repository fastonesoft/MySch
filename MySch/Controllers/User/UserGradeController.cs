using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Core;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySch.Bll.Custom;

namespace MySch.Controllers.User
{
    public class UserGradeController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var edus = BllEdu.GetEntitys<BllEdu>(a => a.AccIDS == login.IDS && a.Fixed).OrderBy(a => a.IDS);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = ViSchStep.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Edus = EasyUICombo.ToComboJsons<BllEdu>(edus, null);
                ViewBag.Years = EasyUICombo.ToComboJsons<BllYear>(years, null);
                ViewBag.Steps = EasyUICombo.ToComboJsons<ViSchStep>(steps, id);

                return View();
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var entity = BllGrade.GetEntity<BllGrade>(a => a.ID == id);

                var edus = BllEdu.GetEntitys<BllEdu>(a => a.AccIDS == login.IDS && a.Fixed).OrderBy(a => a.IDS);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = ViSchStep.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Edus = EasyUICombo.ToComboJsons<BllEdu>(edus, entity.EduIDS);
                ViewBag.Years = EasyUICombo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.Steps = EasyUICombo.ToComboJsons<ViSchStep>(steps, entity.StepIDS);


                return View(entity);
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
                var entity = BllGrade.GetEntity<BllGrade>(a => a.ID == id);

                var login = BllLogin.GetLogin(Session);
                var edus = BllEdu.GetEntitys<BllEdu>(a => a.AccIDS == login.IDS && a.Fixed).OrderBy(a => a.IDS);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = ViSchStep.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Edus = EasyUICombo.ToComboJsons<BllEdu>(edus, entity.EduIDS);
                ViewBag.Years = EasyUICombo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.Steps = EasyUICombo.ToComboJsons<ViSchStep>(steps, entity.StepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllGrade entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.StepIDS + entity.EduIDS.Replace(entity.AccIDS, "");
                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var qentity = ViSchGrade.GetEntity(a => a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllGrade entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //修改
                entity.ToUpdate(ModelState);
                //查询 视图数据
                var qentity = ViSchGrade.GetEntity(a => a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllGrade entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //查询 视图数据 保存
                var qentity = ViSchGrade.GetEntity(a => a.ID == entity.ID);
                //删除
                entity.ToDelete(ModelState);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult MenuTree(string id = null)
        {
            var login = BllLogin.GetLogin(Session);
            //校区：所有
            if (id == null)
            {
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS && a.Fixed == false).OrderBy(a => a.IDS);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Part");
                return Json(res);
            }
            else
            {
                var entitys = ViSchStep.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id).OrderBy(a => a.IDS);
                var res = EasyUITree.ToTree(entitys, "IDS", "StepName", "open", "Step");
                return Json(res);
            }
        }

        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var res = id == null ?
                    ViSchGrade.GetDataGridPages(a => a.AccIDS == login.IDS, page, rows) :
                    ViSchGrade.GetDataGridPages(a => a.AccIDS == login.IDS && a.StepIDS == id, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }
    }
}