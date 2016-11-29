using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminComController : RoleAdminController
    {
        // GET: AdminCom
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(VGradeStud entity)
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
        public ActionResult AddToken(BllStudent entity)
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
        public ActionResult Del(VGradeStud entity)
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
        public ActionResult DelToken(BllStudent entity)
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
        public ActionResult Data(string id)
        {
            try
            {

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
                    VGradeStud.GetDataGridPages(a => bantext.Contains(a.BanIDS) && a.InSch && (a.CID.Contains(text) || a.StudName.Contains(text)), page, rows);

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}