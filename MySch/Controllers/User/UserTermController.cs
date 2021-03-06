﻿using MySch.Bll;
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
    public class UserTermController : RoleController
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
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var semes = BllTermType.GetEntitys<BllTermType>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Years = EasyUICombo.ToComboJsons<BllYear>(years, id);
                ViewBag.TermTypes = EasyUICombo.ToComboJsons<BllTermType>(semes, null);

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
                var entity = BllTerm.GetEntity<BllTerm>(a => a.ID == id);

                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var semes = BllTermType.GetEntitys<BllTermType>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Years = EasyUICombo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.TermTypes = EasyUICombo.ToComboJsons<BllTermType>(semes, entity.TermTypeIDS);

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
                var entity = BllTerm.GetEntity<BllTerm>(a => a.ID == id);

                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var semes = BllTermType.GetEntitys<BllTermType>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Years = EasyUICombo.ToComboJsons<BllYear>(years, entity.YearIDS);
                ViewBag.TermTypes = EasyUICombo.ToComboJsons<BllTermType>(semes, entity.TermTypeIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllTerm entity)
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
                entity.IDS = "TMT" +  entity.AccIDS + entity.YearIDS.Replace(entity.AccIDS, "") + entity.TermTypeIDS.Replace(entity.AccIDS, "");
                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllTerm entity)
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllTerm entity)
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult MenuTree()
        {
            var login = BllLogin.GetLogin(Session);

            var entitys = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
            var res = EasyUITree.ToTree(entitys, "IDS", "Name", "open", "Part");
            return Json(res);
        }

        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = id == null ?
                    ViSchTerm.GetDataGridPages<ViSchTerm, string>(a => a.AccIDS == login.IDS,a=>a.IDS, page, rows) :
                    ViSchTerm.GetDataGridPages<ViSchTerm,string>(a => a.AccIDS == login.IDS && a.YearIDS == id,a=>a.IDS, page, rows);

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }
    }
}