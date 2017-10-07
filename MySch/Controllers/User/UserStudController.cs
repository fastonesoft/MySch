using MySch.Bll.Custom;
using MySch.Bll.Entity;
using MySch.Bll.View;
using MySch.Core;
using MySch.Mvvm.School.Student;
using MySch.Mvvm.School.Student.Action;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    ViewBag.Grades = EasyUICombo.ToComboJsons<VGrade>(grades, "IDS", "TreeName", null);
                    ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, null);
                    ViewBag.Comes = EasyUICombo.ToComboJsons<BllCome>(comes, null);
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
                        ViewBag.Grades = EasyUICombo.ToComboJsons<VGrade>(grades, "IDS", "TreeName", id);
                        ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", null);
                        ViewBag.Comes = EasyUICombo.ToComboJsons<BllCome>(comes, null);
                        ViewBag.GradeReadonly = "true";
                        ViewBag.BanReadonly = "false";
                    }
                    else
                    {
                        var ban = VBan.GetEntity(a => a.IDS == id);

                        var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == ban.PartIDS && a.IsCurrent);
                        var comes = BllCome.GetEntitys<BllCome>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                        var bans = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.IDS == id);

                        ViewBag.Grades = EasyUICombo.ToComboJsons<VGrade>(grades, "IDS", "TreeName", ban.GradeIDS);
                        ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", id);
                        ViewBag.Comes = EasyUICombo.ToComboJsons<BllCome>(comes, null);
                        ViewBag.GradeReadonly = "true";
                        ViewBag.BanReadonly = "true";
                    }
                }

                return View();
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllStudentIn entity)
        {
            try
            {
                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var res = VGradeStud.GetDataGridPages(a => a.IDC == entity.IDC, 1, 1);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(VGradeStud entity)
        {
            try
            {
                var stud = VmStudentEdit.GetEntity<VmStudentEdit>(a=>a.IDS == entity.StudIDS, "没有找到你要的学生！");
                return View(stud);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(VmStudentEdit entity)
        {
            try
            {
                entity.ToUpdate(ModelState);
                IDC.Check(entity.IDC);

                var res = VGradeStud.GetEntity(a => a.StudIDS == entity.IDS);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        /// <summary>
        /// 休学办理
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Drop(BllGradeDrop entity)
        {
            try
            {
                var bans = VBan.GetEntitys(a => a.GradeIDS == entity.GradeIDS);
                var steps = VStep.GetEntitys(a => a.PartIDS == entity.PartIDS && a.Graduated == false).ToList();
                //年级筛选：比当前年级小一级的才显示
                bool founded = false;
                int length = steps.Count();
                for (int i = length - 1; i >= 0; i--)
                {
                    //倒序检查当前年级，发现以后，全部过滤
                    if (steps[i].IDS == entity.StepIDS) founded = true;
                    if (founded) steps.Remove(steps[i]);
                }
                ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", entity.BanIDS);
                ViewBag.Steps = EasyUICombo.ToComboJsons<VStep>(steps, "IDS", "Name", entity.StepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        /// <summary>
        /// 休学办理：提交
        /// </summary>
        /// <param name="drop"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DropToken(BllGradeDrop entity)
        {
            try
            {
                //查询“休学”编号
                var login = BllLogin.GetLogin(Session);
                var outs = BllOut.GetEntity<BllOut>(a => a.Name == "休学" && a.AccIDS == login.IDS);
                //一、变更数据 -> 设置不在校、离校状态
                entity.InSch = false;
                entity.OutIDS = outs.IDS;
                entity.OutTime = DateTime.Now;
                entity.ToUpdate(ModelState);
                //二、学生库中降级
                var student = BllStudentDrop.GetEntity<BllStudentDrop>(a => a.IDS == entity.StudIDS);
                student.StepIDS = entity.StepIDS;
                student.ToUpdate();
                //显示
                var entitys = VStudOut.GetEntitys(a => a.ID == entity.ID);
                return Json(EasyUI<VStudOut>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Out(BllGradeOut entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var bans = VBan.GetEntitys(a => a.GradeIDS == entity.GradeIDS).OrderBy(a => a.Num);
                var outs = BllOut.GetEntitys<BllOut>(a => a.AccIDS == login.IDS && a.CanReturn).OrderBy(a => a.IDS);
                var steps = VStep.GetEntitys(a => a.PartIDS == entity.PartIDS && a.Graduated == false);


                ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", entity.BanIDS);
                ViewBag.Outs = EasyUICombo.ToComboJsons<BllOut>(outs, "IDS", "Name", null);
                ViewBag.Steps = EasyUICombo.ToComboJsons<VStep>(steps, "IDS", "Name", entity.StepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OutToken(BllGradeOut entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                //1.1 离校
                entity.InSch = false;
                entity.OutTime = DateTime.Parse(entity.OutTimeIn);
                entity.ToUpdate(ModelState);

                //1.2 显示
                var entitys = VStudOut.GetEntitys(a => a.ID == entity.ID);
                return Json(EasyUI<VStudOut>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Back(BllGradeBack entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var bans = VBan.GetEntitys(a => a.GradeIDS == entity.GradeIDS).OrderBy(a => a.Num);
                var comes = BllCome.GetEntitys<BllCome>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);

                ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", null);
                ViewBag.Comes = EasyUICombo.ToComboJsons<BllCome>(comes, "IDS", "Name", null);

                //可返回
                if (entity.CanReturn) return View(entity);
                //不可返回
                return Json(new ErrorMessage { error = true, message = "错误：此类离校学生无法回校！" });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BackToken(BllGradeBack entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                //1.1 离校
                entity.InSch = true;
                entity.ToUpdate(ModelState);

                //1.2 显示
                var entitys = VStudOut.GetEntitys(a => a.ID == entity.ID);
                return Json(EasyUI<VStudOut>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Backs(IEnumerable<VStudOut> entitys)
        {
            try
            {
                var infor = BllGradeBacks.Backs(entitys);
                return Json(new ErrorMessage { error = false, message = infor });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
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
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS && a.Fixed == false).OrderBy(a => a.IDS);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Part");
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
                    var entitys = VBan.GetEntitys(a => a.AccIDS == login.IDS && a.GradeIDS == id).OrderBy(a => a.Num);
                    var res = EasyUITree.ToTree(entitys, "IDS", "TreeName", "open", "Class");
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
                        VGradeStud.GetDataGridPages(a => a.PartIDS == id && (a.IDC.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                    return Json(res);
                }
                else
                {
                    if (memo == "Grade")
                    {
                        var res = string.IsNullOrEmpty(text) ?
                            VGradeStud.GetDataGridPages(a => a.GradeIDS == id && a.InSch, page, rows) :
                            VGradeStud.GetDataGridPages(a => a.GradeIDS == id && (a.IDC.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                        return Json(res);
                    }
                    else
                    {
                        var res = string.IsNullOrEmpty(text) ?
                            VGradeStud.GetDataGridPages(a => a.BanIDS == id && a.InSch, page, rows) :
                            VGradeStud.GetDataGridPages(a => a.BanIDS == id && (a.IDC.Contains(text) || a.StudName.Contains(text)) && a.InSch, page, rows);

                        return Json(res);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetStudInfor(IEnumerable<VGradeStud> rows)
        {
            try
            {
                var cookies = ActionStudent.AutoLogin("http://xjgl.jse.edu.cn/uids/index.jsp",
                  "http://xjgl.jse.edu.cn/uids/genImageCode?rnd=" + DateTime.Now.Ticks.ToString(),
                  "http://xjgl.jse.edu.cn/uids/login.jsp", "c32128441402", "==QTuhWMaVlWoN2MSFXYR1TP");
                
                int count = 0;
                foreach (var stud in rows)
                {
                    var student = ActionStudent.GetStudentHtml(stud.StudName, stud.IDC, cookies);

                    var xues = Jsons.JsonEntity<IEnumerable<VmStudDetail>>(student);
                    if (xues.Count() != 0)
                    {
                        VmStudDetail xue = xues.First();

                        BllStudentFill ins = BllStudentFill.GetEntity<BllStudentFill>(a => a.IDC == stud.IDC);
                        ins.Name1 = xue.first_guardian_name;
                        ins.Name2 = xue.second_guardian_name;
                        ins.Mobil2 = xue.second_guardian_phone;
                        ins.Birth = xue.birth_place;
                        ins.Home = xue.home_address;

                        ins.ToUpdate();

                        count++;
                    }
                }

                return Json(new ErrorMessage { error = true, message = string.Format("转换成功{0}个学生资料！", count) });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }
    }
}