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
        public ActionResult Add(string id, string memo)
        {
            if (memo == "Theme")
            {
                var parents = BllTheme.GetEntitys<BllTheme>(a => a.IDS == id).OrderBy(a => a.IDS);
                ViewBag.Parents = EasyUICombo.ToComboJsons(parents, id);
            }
            else
            {
                var parents = BllPage.GetEntitys<BllPage>(a => a.IDS == id).OrderBy(a => a.IDS);
                ViewBag.Parents = EasyUICombo.ToComboJsons(parents, id);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(BllPage entity)
        {
            try
            {
                var db = BllPage.GetEntity<BllPage>(a => a.ID == entity.ID);
                db.Html = HttpUtility.UrlDecode(db.Html);

                if (BllTheme.Count(a => a.ID == entity.ID) != 0)
                {
                    var parents = BllTheme.GetEntitys<BllTheme>(a => a.IDS == entity.IDS).OrderBy(a => a.IDS);
                    ViewBag.Parents = EasyUICombo.ToComboJsons(parents, entity.IDS);
                }
                else
                {
                    var parents = BllPage.GetEntitys<BllPage>(a => a.IDS == entity.ParentIDS).OrderBy(a => a.IDS);
                    ViewBag.Parents = EasyUICombo.ToComboJsons(parents, entity.ParentIDS);
                }

                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Del(string id, string memo)
        {
            try
            {
                var db = BllPage.GetEntity<BllPage>(id);

                var themes = BllTheme.GetEntitys<BllTheme>(a => true).OrderBy(a => a.IDS);
                ViewBag.Themes = EasyUICombo.ToComboJsons(themes, db.ParentIDS);

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
                    var edits = BllPage.GetEntitys<BllPage>(a => a.Bootup && a.ParentIDS == entity.ParentIDS);
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
                    var edits = BllPage.GetEntitys<BllPage>(a => a.Bootup && a.ParentIDS == entity.ParentIDS);
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
                if (BllPage.Count(a => a.ParentIDS == entity.IDS) > 0) throw new Exception("表示层：存在子页面数据，不能删除！");
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
                var entitys = BllPage.GetEntitys<BllPage>(a => a.ParentIDS == id);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Page");
                //叶子
                foreach (var r in res)
                {
                    r.state = BllPage.Count(a => a.ParentIDS == r.id) > 0 ? "closed" : "open";
                }
                return Json(res);
            }
        }


        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var res = id == null ?
                    BllPage.GetDataGridPages<BllPage, string>(a => true, a => a.IDS, page, rows, OrderType.ASC) :
                    BllPage.GetDataGridPages<BllPage, string>(a => a.ParentIDS == id, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}