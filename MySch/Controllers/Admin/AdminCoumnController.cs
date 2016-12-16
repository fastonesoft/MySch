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
    public class AdminCoumnController : RoleAdminController
    {

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string id)
        {
            var pages = BllPage.GetEntitys<BllPage>(a => true).OrderBy(a => a.IDS);
            ViewBag.Pages = EasyUICombo.ToComboJsons(pages, id);

            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var db = BllColumn.GetEntity<BllColumn>(id);

                var pages = BllPage.GetEntitys<BllPage>(a => true).OrderBy(a => a.IDS);
                ViewBag.Pages = EasyUICombo.ToComboJsons(pages, db.PageIDS);

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
                var db = BllColumn.GetEntity<BllColumn>(id);

                var pages = BllPage.GetEntitys<BllPage>(a => true).OrderBy(a => a.IDS);
                ViewBag.Pages = EasyUICombo.ToComboJsons(pages, db.PageIDS);

                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllColumn entity)
        {
            try
            {
                entity.ID = Guid.NewGuid().ToString("N");
                //添加数据
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
        public ActionResult EditToken(BllColumn entity)
        {
            try
            {
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
        public ActionResult DelToken(BllColumn entity)
        {
            try
            {
                //当前判断
                if (BllPage.Count(a => a.ThemeIDS == entity.IDS) > 0) throw new Exception("表示层：模板已设置页面数据，不能删除！");
                //删除数据
                entity.ToDelete(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


        [HttpPost]
        public ActionResult MenuTree(string id = null)
        {
            if (id == null)
            {
                //模板
                var entitys = BllTheme.GetEntitys<BllTheme>(a => true);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Theme");
                return Json(res);
            }
            else
            {
                //页面
                var entitys = BllPage.GetEntitys<BllPage>(a => a.ThemeIDS == id);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "open", "Page");
                return Json(res);
            }
        }


        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var res = id == null ?
                    VColumn.GetDataGridPages(a => true, page, rows) :
                    VColumn.GetDataGridPages(a => a.PageIDS == id, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}