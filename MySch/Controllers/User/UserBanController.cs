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
    public class UserBanController : RoleController
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
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = BllAcc.GetEntitys<BllAcc>(a => a.Parent == login.ID).OrderBy(a => a.IDS);

                ViewBag.Grades = EasyCombo.ToComboJsons(grades, null);
                ViewBag.Groups = EasyCombo.ToComboJsons<BllAcc>(accs, null);
                ViewBag.Masters = EasyCombo.ToComboJsons<BllAcc>(accs, null);

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
                var entity = BllBan.GetEntity<BllBan>(id);

                var login = BllLogin.GetLogin(Session);
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = BllAcc.GetEntitys<BllAcc>(a => a.Parent == login.ID).OrderBy(a => a.IDS);

                ViewBag.Grades = EasyCombo.ToComboJsons(grades, entity.GradeIDS);
                ViewBag.Groups = EasyCombo.ToComboJsons<BllAcc>(accs, entity.GroupIDS);
                ViewBag.Masters = EasyCombo.ToComboJsons<BllAcc>(accs, entity.MasterIDS);

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
                var entity = BllBan.GetEntity<BllBan>(id);

                var login = BllLogin.GetLogin(Session);
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = BllAcc.GetEntitys<BllAcc>(a => a.Parent == login.ID).OrderBy(a => a.IDS);

                ViewBag.Grades = EasyCombo.ToComboJsons(grades, entity.GradeIDS);
                ViewBag.Groups = EasyCombo.ToComboJsons<BllAcc>(accs, entity.GroupIDS);
                ViewBag.Masters = EasyCombo.ToComboJsons<BllAcc>(accs, entity.MasterIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllBan entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.GradeIDS + entity.Num.ToString("D2");

                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var qentity = VBan.GetEntity(a=>a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllBan entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //更新
                entity.ToUpdate(ModelState);
                //查询 视图数据
                var qentity = BllBan.GetEntity<BllBan>(entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllBan entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //查询 视图数据 保存
                var qentity = VBan.GetEntity(a=>a.ID== entity.ID);
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
                var res = VBan.GetDataGridPages(a => a.AccIDS == login.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetCombos(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var bans = VBan.GetEntitys(a => a.GradeIDS == id);
                return Json(EasyCombo.ToCombo<VBan>(bans, "IDS", "TreeName", null));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}