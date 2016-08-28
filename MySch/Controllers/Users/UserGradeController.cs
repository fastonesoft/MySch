﻿using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Users
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
                var edus = BllEdu.GetEntitys<BllEdu>(a => a.AccIDS == login.IDS && a.Fixed).OrderBy(a => a.IDS);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var partsteps = QllPartStep.GetEntitys(a => a.AccIDS == login.IDS);

                ViewBag.Edus = Combo.ToComboJsons<BllEdu>(edus, null);
                ViewBag.Years = Combo.ToComboJsons<BllYear>(years, null);
                ViewBag.PartSteps = Combo.ToComboJsons<QllPartStep>(partsteps, null);

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
                var edus = BllEdu.GetEntitys<BllEdu>(a => a.AccIDS == login.IDS && a.Fixed).OrderBy(a => a.IDS);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var partsteps = QllPartStep.GetEntitys(a => a.AccIDS == login.IDS);

                ViewBag.Edus = Combo.ToComboJsons<BllEdu>(edus, entity.EduIDS);
                ViewBag.Years = Combo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.PartSteps = Combo.ToComboJsons<QllPartStep>(partsteps, entity.PartStepIDS);

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
                var edus = BllEdu.GetEntitys<BllEdu>(a => a.AccIDS == login.IDS && a.Fixed).OrderBy(a => a.IDS);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var partsteps = QllPartStep.GetEntitys(a => a.AccIDS == login.IDS);

                ViewBag.Edus = Combo.ToComboJsons<BllEdu>(edus, entity.EduIDS);
                ViewBag.Years = Combo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.PartSteps = Combo.ToComboJsons<QllPartStep>(partsteps, entity.PartStepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllGrade entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;
                entity.ID = Guid.NewGuid().ToString("N");
                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var qentity = QllGrade.GetEntity(a => a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllGrade entity)
        {
            try
            {
                //更新
                entity.ToUpdate(ModelState);
                //查询 视图数据
                var qentity = QllGrade.GetEntity(a => a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllGrade entity)
        {
            try
            {
                //查询 视图数据 保存
                var qentity = QllGrade.GetEntity(a => a.ID == entity.ID);
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
                var res = QllGrade.GetDataGridPages(a => a.AccIDS == login.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}