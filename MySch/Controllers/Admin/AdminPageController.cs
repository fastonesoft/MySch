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
    public class AdminPageController : RoleAdminController
    {

        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string id)
        {
            var themes = BllTheme.GetEntitys<BllTheme>(a => true).OrderBy(a => a.IDS);
            ViewBag.Themes = EasyUICombo.ToComboJsons(themes, id);

            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var db = BllPage.GetEntity<BllPage>(id);

                var themes = BllTheme.GetEntitys<BllTheme>(a => true).OrderBy(a => a.IDS);
                ViewBag.Themes = EasyUICombo.ToComboJsons(themes, db.ThemeIDS);

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
                var db = BllPage.GetEntity<BllPage>(id);

                var themes = BllTheme.GetEntitys<BllTheme>(a => true).OrderBy(a => a.IDS);
                ViewBag.Themes = EasyUICombo.ToComboJsons(themes, db.ThemeIDS);

                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllPage entity)
        {
            try
            {
                //清除当前
                if (entity.Bootup)
                {
                    var edits = BllPage.GetEntitys<BllPage>(a => a.Bootup && a.ThemeIDS == entity.ThemeIDS);
                    foreach (var edit in edits)
                    {
                        edit.Bootup = false;
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
        public ActionResult EditToken(BllPage entity)
        {
            try
            {
                //清除当前
                if (entity.Bootup)
                {
                    var edits = BllPage.GetEntitys<BllPage>(a => a.Bootup && a.ThemeIDS == entity.ThemeIDS);
                    foreach (var edit in edits)
                    {
                        edit.Bootup = false;
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
        public ActionResult DelToken(BllPage entity)
        {
            try
            {
                //当前判断
                if (entity.Bootup) throw new Exception("表示层：启动页，不能删除！");
                if (BllColumn.Count(a => a.PageIDS == entity.IDS) > 0) throw new Exception("表示层：页面已设置栏目数据，不能删除！");
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
        public ActionResult MenuTree()
        {
            //模板
            var entitys = BllTheme.GetEntitys<BllTheme>(a => true);
            var res = EasyUITree.ToTree(entitys, "IDS", "Name", "open", "Theme");
            return Json(res);
        }


        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var res = id == null ?
                    BllPage.GetDataGridPages<BllPage, string>(a => true, a => a.IDS, page, rows, OrderType.ASC) :
                    BllPage.GetDataGridPages<BllPage, string>(a => a.ThemeIDS == id, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}