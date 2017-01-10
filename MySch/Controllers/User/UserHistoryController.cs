using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserHistoryController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GradeTree(string id = null, string memo = null)
        {
            var login = BllLogin.GetLogin(Session);
            //校区：所有
            if (id == null)
            {
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Part");
                return Json(res);
            }
            else
            {
                //年级：所有
                if (memo == "Part")
                {
                    var entitys = VStep.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id);
                    var res = EasyUITree.ToTree(entitys, "IDS", "StepName", "closed", "Step");
                    return Json(res);
                }
                else
                {
                    //分级：不是当年的
                    if (memo == "Step")
                    {
                        var entitys = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.StepIDS == id && a.IsCurrent == false);
                        var res = EasyUITree.ToTree(entitys, "IDS", "TreeName", "closed", "Grade");
                        return Json(res);
                    }
                    else
                    {
                        //班级：所有
                        var entitys = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.GradeIDS == id);
                        var res = EasyUITree.ToTree(entitys, "IDS", "TreeName", "open", "Class");
                        return Json(res);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult DataGrid(string id = null, string memo = null, string text = null, int page = 1, int rows = 100)
        {
            var login = BllLogin.GetLogin(Session);
            try
            {
                if (memo == "Part")
                {
                    var res = string.IsNullOrEmpty(text) ?
                        VGradeStud.GetDataGridPages(a => a.PartIDS == id && a.IsCurrent == false, page, rows) :
                        VGradeStud.GetDataGridPages(a => a.PartIDS == id && a.IsCurrent == false && (a.CID.Contains(text) || a.StudName.Contains(text)), page, rows);

                    return Json(res);
                }
                else
                {
                    if (memo == "Step")
                    {
                        var res = string.IsNullOrEmpty(text) ?
                            VGradeStud.GetDataGridPages(a => a.StepIDS == id && a.IsCurrent == false, page, rows) :
                            VGradeStud.GetDataGridPages(a => a.StepIDS == id && a.IsCurrent == false && (a.CID.Contains(text) || a.StudName.Contains(text)), page, rows);

                        return Json(res);
                    }
                    else
                    {
                        if (memo == "Grade")
                        {
                            var res = string.IsNullOrEmpty(text) ?
                                VGradeStud.GetDataGridPages(a => a.GradeIDS == id && a.IsCurrent == false, page, rows) :
                                VGradeStud.GetDataGridPages(a => a.GradeIDS == id && a.IsCurrent == false && (a.CID.Contains(text) || a.StudName.Contains(text)), page, rows);

                            return Json(res);
                        }
                        else
                        {
                            var res = string.IsNullOrEmpty(text) ?
                                VGradeStud.GetDataGridPages(a => a.BanIDS == id && a.IsCurrent == false, page, rows) :
                                VGradeStud.GetDataGridPages(a => a.BanIDS == id && a.IsCurrent == false && (a.CID.Contains(text) || a.StudName.Contains(text)), page, rows);

                            return Json(res);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}