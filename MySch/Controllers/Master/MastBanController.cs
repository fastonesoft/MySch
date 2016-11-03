using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Master
{
    public class MastBanController : RoleController
    {
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(VGradeStud entity)
        {
            try
            {
                var db = BllStudent.GetEntity<BllStudent>(a => a.IDS == entity.StudIDS);
                if (db.Fixed)
                {
                    return Json(new BllError { error = true, message = "前端：已确认，无法再修改！" });
                }
                else
                {
                  return   View("~/MastStud/Edit", db);
                    return View(db);
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllStudent entity)
        {
            try
            {
                entity.Checked = true;

                entity.ToUpdate(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Fix(VGradeStud entity)
        {
            try
            {
                var db = BllStudent.GetEntity<BllStudent>(a => a.IDS == entity.StudIDS);
                if (db.Checked)
                {
                    if (db.Fixed)
                    {
                        return Json(new BllError { error = true, message = "前端：已确认，无须重复提交！" });
                    }
                    else
                    {
                        return View(db);
                    }
                }
                else
                {
                    return Json(new BllError { error = true, message = "前端：资料未更新，无法确认！" });
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FixToken(BllStudent entity)
        {
            try
            {
                var db = BllStudent.GetEntity<BllStudent>(a => a.ID == entity.ID && a.IDS == entity.IDS);

                db.Fixed = true;
                db.ToUpdate();
                return Json(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid(string text = null, int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                //获取当前帐号所对应的班级
                var bansID = VBan.GetEntitys(a => a.MasterIDS == login.IDS && a.IsCurrent, "IDS");
                var bantext = string.Join("-", bansID);

                var res = string.IsNullOrEmpty(text) ?
                    VGradeStud.GetDataGridPages(a => bantext.Contains(a.BanIDS) && a.InSch, page, rows) :
                    VGradeStud.GetDataGridPages(a => bantext.Contains(a.BanIDS) && (a.CID.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid2(string id = null, string memo = null, string text = null)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var bansID = VBan.GetEntitys(a => a.MasterIDS == login.IDS && a.IsCurrent, "IDS");
                var bantext = string.Join("-", bansID);

                var res = string.IsNullOrEmpty(text) ?
                    VStudOut.GetDataGrids(id, memo) :
                    VStudOut.GetDataGrids(id, memo, text);

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}