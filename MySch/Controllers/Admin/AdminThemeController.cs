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

namespace MySch.Controllers.Admin
{
    public class AdminThemeController : Controller
    {

        [HttpPost]
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
                var db = BllAdminTheme.GetEntity<BllAdminTheme>(a => a.ID == id);
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
                var db = BllAdminTheme.GetEntity<BllAdminTheme>(a => a.ID == id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllAdminTheme entity)
        {
            try
            {
                //清除当前
                if (entity.IsCurrent)
                {
                    var edits = BllAdminTheme.GetEntitys<BllAdminTheme>(a => a.IsCurrent);
                    foreach (var edit in edits)
                    {
                        edit.IsCurrent = false;
                        edit.ToUpdate();
                    }
                }
                //添加数据
                entity.ID = Guid.NewGuid().ToString("N");
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
        public ActionResult EditToken(BllAdminTheme entity)
        {
            try
            {
                //清除当前
                if (entity.IsCurrent)
                {
                    var edits = BllAdminTheme.GetEntitys<BllAdminTheme>(a => a.IsCurrent);
                    foreach (var edit in edits)
                    {
                        edit.IsCurrent = false;
                        edit.ToUpdate();
                    }
                }
                //更新数据
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
        public ActionResult DelToken(BllAdminTheme entity)
        {
            try
            {
                //当前判断
                if (entity.IsCurrent) throw new Exception("表示层：当前模板数据，不能删除！");
                if (BllAdminPage.Count(a => a.ParentID == entity.ID) > 0) throw new Exception("表示层：模板已设置页面数据，不能删除！");

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
                var res = BllAdminTheme.GetDataGridPages<BllAdminTheme, string>(a => true, a => a.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }
    }
}