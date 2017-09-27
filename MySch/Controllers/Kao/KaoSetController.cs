﻿using MySch.Bll.Custom;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Kao
{
    public class KaoSetController : RoleController
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
                var db = BllEdu.GetEntity<BllEdu>(id);
                return View(db);
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
                var db = BllEdu.GetEntity<BllEdu>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllEdu entity)
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllEdu entity)
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllEdu entity)
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = BllEdu.GetDataGridPagesAsc<BllEdu, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

    }
}