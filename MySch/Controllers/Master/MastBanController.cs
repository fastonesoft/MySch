using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.View;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace MySch.Controllers.Master
{
    class dd
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }
        public string Text6 { get; set; }
        public string Text7 { get; set; }
        public string Text8 { get; set; }
        public string Text9 { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }
        public double Value5 { get; set; }
        public double Value6 { get; set; }
        public double Value7 { get; set; }
        public double Value8 { get; set; }
        public double Value9 { get; set; }
        public bool X1 { get; set; }
        public bool X2 { get; set; }
        public bool X3 { get; set; }
        public bool X4 { get; set; }
        public bool X5 { get; set; }
    }


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
                    return View(db);
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
        public ActionResult UnFix(IEnumerable<VGradeStud> entitys)
        {
            try
            {
                foreach (var entity in entitys)
                {
                    var db = BllStudent.GetEntity<BllStudent>(a => a.IDS == entity.StudIDS);

                    db.Fixed = false;
                    db.ToUpdate();

                    entity.Fixed = false;
                }
                return Json(entitys);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //查看离校学生资料
        [HttpPost]
        public ActionResult Detail(VStudOut entity)
        {
            try
            {
                var db = BllStudent.GetEntity<BllStudent>(a => a.IDS == entity.StudIDS);
                return View(db);
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
                    VGradeStud.GetDataGridPages(a => bantext.Contains(a.BanIDS) && a.InSch && (a.IDC.Contains(text) || a.StudName.Contains(text)), page, rows);

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid2(string text = null)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var bansID = VBan.GetEntitys(a => a.MasterIDS == login.IDS && a.IsCurrent, "IDS");
                var bantext = string.Join("-", bansID);

                var res = string.IsNullOrEmpty(text) ?
                    VStudOut.GetDataGrids(a => bantext.Contains(a.BanIDS) && a.InSch == false) :
                    VStudOut.GetDataGrids(a => bantext.Contains(a.BanIDS) && a.InSch == false && (a.IDC.Contains(text) || a.StudName.Contains(text)));

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        public ActionResult Command()
        {

            try
            {
                using (BaseContext db = new BaseContext())
                {

                    var res = db.Database.SqlQuery<dd>("select ID, IDS, Value1 = cast(Value1 as float), Text1 = convert(nvarchar(32) , Value2, 120), X1 = Value3 from dbo.KSubSub").ToList();
                    return Json(res);
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}