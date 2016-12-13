using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminThemeController : RoleAdminController
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
                var db = BllTheme.GetEntity<BllTheme>(id);
                return View(db);
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
                var db = BllTheme.GetEntity<BllTheme>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllTheme entity)
        {
            try
            {
                //清除当前
                if (entity.IsCurrent)
                {
                    var edits = BllTheme.GetEntitys<BllTheme>(a => a.IsCurrent);
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
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllTheme entity)
        {
            try
            {
                //清除当前
                if (entity.IsCurrent)
                {
                    var edits = BllTheme.GetEntitys<BllTheme>(a => a.IsCurrent);
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
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllTheme entity)
        {
            try
            {
                //当前判断
                if (entity.IsCurrent) throw new Exception("表示层：当前模板数据，不能删除！");
                if (BllPage.Count(a => a.ThemeIDS == entity.IDS) > 0) throw new Exception("表示层：模板已设置页面数据，不能删除！");

                entity.ToDelete(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult ThemeTree(string id = null)
        {
            //模板
            if (id == null)
            {
                var entitys = BllTheme.GetEntitys<BllTheme>(a => true);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Theme");
                return Json(res);
            }
            else
            {
                var entitys = BllPage.GetEntitys<BllPage>(a => a.ThemeIDS == id);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "open", "Page");
                return Json(res);
            }
        }


        [HttpPost]
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var res = BllTheme.GetDataGridPages<BllTheme, string>(a => true, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}