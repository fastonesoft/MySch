using MySch.Bll;
using MySch.Bll.Action;
using MySch.Bll.Entity;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserStudController : RoleController
    {
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string id, string memo)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                if (memo == "Part")
                {
                    var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id && a.IsCurrent);
                    var comes = BllCome.GetEntitys<BllCome>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                    var bans = new List<VBan>();
                    ViewBag.Grades = EasyCombo.ToComboJsons<VGrade>(grades, "IDS", "PartStepName", null);
                    ViewBag.Bans = EasyCombo.ToComboJsons<VBan>(bans, null);
                    ViewBag.Comes = EasyCombo.ToComboJsons<BllCome>(comes, null);
                    ViewBag.GradeReadonly = "false";
                    ViewBag.BanReadonly = "false";
                }
                else
                {
                    if (memo == "Grade")
                    {
                        var grade = VGrade.GetEntity(a => a.IDS == id);

                        var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == grade.PartIDS && a.IsCurrent);
                        var comes = BllCome.GetEntitys<BllCome>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                        var bans = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.GradeIDS == id);
                        ViewBag.Grades = EasyCombo.ToComboJsons<VGrade>(grades, "IDS", "PartStepName", id);
                        ViewBag.Bans = EasyCombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", null);
                        ViewBag.Comes = EasyCombo.ToComboJsons<BllCome>(comes, null);
                        ViewBag.GradeReadonly = "true";
                        ViewBag.BanReadonly = "false";
                    }
                    else
                    {
                        var ban = VBan.GetEntity(a => a.IDS == id);

                        var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == ban.PartIDS && a.IsCurrent);
                        var comes = BllCome.GetEntitys<BllCome>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                        var bans = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.IDS == id);

                        ViewBag.Grades = EasyCombo.ToComboJsons<VGrade>(grades, "IDS", "PartStepName", ban.GradeIDS);
                        ViewBag.Bans = EasyCombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", id);
                        ViewBag.Comes = EasyCombo.ToComboJsons<BllCome>(comes, null);
                        ViewBag.GradeReadonly = "true";
                        ViewBag.BanReadonly = "true";
                    }
                }

                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(XueAdd entity)
        {
            try
            {
                //添加
                var id = entity.ToAdd(ModelState);
                //查询 视图数据
                var entitys = VGradeStud.GetEntitys(a => a.ID == id);
                return Json(EasyUI<VGradeStud>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        /// <summary>
        /// 班级调整
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Change(BllBanChange row)
        {
            try
            {
                var bans = VBan.GetEntitys(a => a.GradeIDS == row.GradeIDS);
                ViewBag.Bans = EasyCombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", row.BanIDS);

                return View(row);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        /// <summary>
        /// 班级调整：提交
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeToken(BllBanChange change)
        {
            try
            {
                change.ToUpdate(ModelState);

                var entitys = VGradeStud.GetEntitys(a => a.ID == change.ID);
                return Json(EasyUI<VGradeStud>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        /// <summary>
        /// 休学办理
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Drop(BllGradeDrop row)
        {
            try
            {
                var bans = VBan.GetEntitys(a => a.GradeIDS == row.GradeIDS);
                var partsteps = VPartStep.GetEntitys(a => a.PartIDS == row.PartIDS && a.Graduated == false);
                ViewBag.Bans = EasyCombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", row.BanIDS);
                ViewBag.PartSteps = EasyCombo.ToComboJsons<VPartStep>(partsteps, "IDS", "Name", row.PartStepIDS);

                return View(row);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        /// <summary>
        /// 休学办理：提交
        /// </summary>
        /// <param name="drop"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DropToken(BllGradeDrop drop)
        {
            try
            {
                //查询“休学”编号
                var login = BllLogin.GetLogin(Session);
                var outs = BllOut.GetEntity<BllOut>(a => a.Name == "休学" && a.AccIDS == login.IDS);
                //一、变更数据 -> 设置不在校、离校状态
                drop.InSch = false;
                drop.OutIDS = outs.IDS;
                drop.ToUpdate(ModelState);
                //二、学生库中降级
                var student = BllStudentDrop.GetEntity<BllStudentDrop>(a => a.IDS == drop.StudIDS);
                student.PartStepIDS = drop.PartStepIDS;
                student.ToUpdate();
                //显示
                var entitys = VStudOut.GetEntitys(a => a.ID == drop.ID);
                return Json(EasyUI<VStudOut>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public ActionResult GradeTree(string id = null, string memo = null)
        {
            var login = BllLogin.GetLogin(Session);
            //校区
            if (id == null)
            {
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS && a.Fixed == false);
                var res = EasyTree.ToTree<BllPart>(entitys, "IDS", "Name", "closed", "Part");
                return Json(res);
            }
            else
            {
                //年级
                if (memo == "Part")
                {
                    var entitys = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id && a.IsCurrent);
                    var res = EasyTree.ToTree(entitys, "IDS", "TreeName", "closed", "Grade");
                    return Json(res);
                }
                else
                {
                    //班级
                    var entitys = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.GradeIDS == id);
                    var res = EasyTree.ToTree(entitys, "IDS", "TreeName", "open", "Class");
                    return Json(res);
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
                        VGradeStud.GetDataGridPages(a => a.PartIDS == id && a.InSch, page, rows) :
                        VGradeStud.GetDataGridPages(a => a.PartIDS == id && (a.CID.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                    return Json(res);
                }
                else
                {
                    if (memo == "Grade")
                    {
                        var res = string.IsNullOrEmpty(text) ?
                            VGradeStud.GetDataGridPages(a => a.GradeIDS == id && a.InSch, page, rows) :
                            VGradeStud.GetDataGridPages(a => a.GradeIDS == id && (a.CID.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                        return Json(res);
                    }
                    else
                    {
                        var res = string.IsNullOrEmpty(text) ?
                            VGradeStud.GetDataGridPages(a => a.BanIDS == id && a.InSch, page, rows) :
                            VGradeStud.GetDataGridPages(a => a.BanIDS == id && (a.CID.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                        return Json(res);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid2(string id = null, string memo = null, string text = null)
        {
            var login = BllLogin.GetLogin(Session);
            try
            {
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

        [HttpPost]
        public ActionResult GradeCheck(IEnumerable<VGradeStud> rows)
        {
            try
            {
                var cookies = AutoXue.Login("http://58.213.155.172/uids/index.jsp",
                    "http://58.213.155.172/uids/genImageCode?rnd=" + DateTime.Now.Ticks.ToString(),
                    "http://58.213.155.172/uids/login!login.action", "c32128441402", "==QTuhWMaVlWoN2MSFXYR1TP");

                int count = 0;
                foreach (var stud in rows)
                {
                    var student = AutoXue.GetStudent(stud.StudName, stud.CID, cookies);

                    var xues = Jsons.JsonEntity<IEnumerable<XueDetail>>(student);
                    if (xues.Count() != 0)
                    {
                        XueDetail xue = xues.First();

                        BllStudentIn ins = BllStudentIn.GetEntity<BllStudentIn>(a => a.CID == stud.CID);
                        ins.Name1 = xue.first_guardian_name;
                        ins.Mobil1 = xue.first_guardian_phone;
                        ins.Name2 = xue.second_guardian_name;
                        ins.Mobil2 = xue.second_guardian_phone;
                        ins.Birth = xue.birth_place;
                        ins.Home = xue.home_address;

                        ins.Checked = true;
                        ins.ToUpdate();

                        count++;
                    }
                }

                return Json(new BllError { error = true, message = string.Format("转换成功{0}个学生资料！", count) });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}