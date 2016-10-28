using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Master
{
    public class MastStudController : RoleController
    {
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Property(VGradeStud entity)
        {
            try
            {
                var db = BllStudent.GetEntity<BllStudent>(a => a.IDS == entity.StudIDS);

                var res = new List<EasyUIpGrid>();
                res.Add(EasyUIpGrid.PItem(db, "IDS", "学生编号", "基本信息", ""));
                res.Add(EasyUIpGrid.PItem(db, "Name", "姓名", "基本信息", ""));
                res.Add(EasyUIpGrid.PItem(db, "CID", "身份证号", "基本信息", ""));
                res.Add(EasyUIpGrid.PItem(db, "Name1", "第一监护人", "详细资料", ""));
                res.Add(EasyUIpGrid.PItem(db, "Name2", "第二监护人", "详细资料", ""));
                res.Add(EasyUIpGrid.PItem(db, "Birth", "户籍地址", "详细资料", ""));
                res.Add(EasyUIpGrid.PItem(db, "Home", "家庭地址", "详细资料", ""));
                res.Add(EasyUIpGrid.PItem(db, "Mobil1", "联系电话一", "联系方式", ""));
                res.Add(EasyUIpGrid.PItem(db, "Mobil2", "联系电话二", "联系方式", ""));
                return Json(EasyUIpGrid.PGrid<EasyUIpGrid>(res));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GradeTree(string id = null, string memo = null)
        {
            var login = BllLogin.GetLogin(Session);
            //校区
            if (id == null)
            {
                var bansID = VBan.GetEntitys(a => a.MasterIDS == login.IDS && a.IsCurrent, "IDS");
                var bantext = string.Join("-", bansID);

                
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS && a.Fixed == false);
                var res = EasyUITree.ToTree<BllPart>(entitys, "IDS", "Name", "closed", "Part");
                return Json(res);
            }
            else
            {
                //年级
                if (memo == "Part")
                {
                    var entitys = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id && a.IsCurrent);
                    var res = EasyUITree.ToTree(entitys, "IDS", "TreeName", "closed", "Grade");
                    return Json(res);
                }
                else
                {
                    //班级
                    var entitys = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.GradeIDS == id);
                    var res = EasyUITree.ToTree(entitys, "IDS", "TreeName", "open", "Class");
                    return Json(res);
                }
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