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
    public class UserTermController : RoleController
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
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var semes = BllSemes.GetEntitys<BllSemes>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Years = EasyCombo.ToComboJsons<BllYear>(years, null);
                ViewBag.Semesters = EasyCombo.ToComboJsons<BllSemes>(semes, null);

                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var entity = BllTerm.GetEntity<BllTerm>(id);

                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var semes = BllSemes.GetEntitys<BllSemes>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Years = EasyCombo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.Semesters = EasyCombo.ToComboJsons<BllSemes>(semes, entity.SemesterIDS);

                return View(entity);
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
                var entity = BllTerm.GetEntity<BllTerm>(id);

                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var semes = BllSemes.GetEntitys<BllSemes>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Years = EasyCombo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.Semesters = EasyCombo.ToComboJsons<BllSemes>(semes, entity.SemesterIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllTerm entity)
        {
            try
            {
                if (entity.IsCurrent)
                {
                    //清除当前
                    BllTerm.UnSelectCurrent();
                }
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.AccIDS + entity.SemesterIDS.Replace(entity.AccIDS, "");
                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllTerm entity)
        {
            try
            {
                if (entity.IsCurrent)
                {
                    //清除当前
                    BllTerm.UnSelectCurrent();
                }
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;
                //更新
                entity.ToUpdate(ModelState);
                //查询 视图数据
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllTerm entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;
                //删除
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
                var res = QllTerm.GetDataGridPages<QllTerm, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}