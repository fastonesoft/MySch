using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserBanstController : Controller
    {
        // GET: UserBanst
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 班级调整
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Change(BllBanChange entity)
        {
            try
            {
                var bans = VBan.GetEntitys(a => a.GradeIDS == entity.GradeIDS);
                ViewBag.Bans = EasyUICombo.ToComboJsons<VBan>(bans, "IDS", "TreeName", entity.BanIDS);

                return View(entity);
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
        public ActionResult ChangeToken(BllBanChange entity)
        {
            try
            {
                entity.ToUpdate(ModelState);

                var entitys = VGradeStud.GetEntitys(a => a.ID == entity.ID);
                return Json(EasyUI<VGradeStud>.DataGrids(entitys, entitys.Count()));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult SameBan(IEnumerable<VGradeStud> entitys)
        {
            try
            {
                var name = string.Empty;
                var id = Guid.NewGuid().ToString("N");
                foreach (var entity in entitys)
                {
                    //记录
                    name += (entity.StudName + "，");
                    //写入
                    var studgrade = new VmStudGradeGroupID
                    {
                        ID = entity.ID,
                        IDS = entity.IDS,
                        GroupID = id,
                    };
                    studgrade.ToUpdate();
                }
                return Json(new BllError { error = false, message = name + "已做了同班标志！" });

            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult NotSame(IEnumerable<VGradeStud> entitys)
        {
            try
            {
                var name = string.Empty;
                foreach (var entity in entitys)
                {
                    //记录
                    name += (entity.StudName + "，");
                    //写入
                    var studgrade = new VmStudGradeGroupID
                    {
                        ID = entity.ID,
                        IDS = entity.IDS,
                        GroupID = null,
                    };
                    studgrade.ToUpdate();
                }
                return Json(new BllError { error = false, message = name + "已取消同班标志！" });

            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult FixStud(IEnumerable<VGradeStud> entitys)
        {
            try
            {
                var entity = entitys.First();
                //记录
                var name = (entity.StudName + "，");
                //写入
                var studgrade = new VmStudGradeFixed
                {
                    ID = entity.ID,
                    IDS = entity.IDS,
                    Fixed = true,
                };
                studgrade.ToUpdate();

                return Json(new BllError { error = false, message = name + "已设置固定班级标志！" });

            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult NotFix(IEnumerable<VGradeStud> entitys)
        {
            try
            {
                var entity = entitys.First();
                //记录
                var name = (entity.StudName + "，");
                //写入
                var studgrade = new VmStudGradeFixed
                {
                    ID = entity.ID,
                    IDS = entity.IDS,
                    Fixed = false,
                };
                studgrade.ToUpdate();

                return Json(new BllError { error = false, message = name + "已取消固定班级标志！" });

            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }






        /////////////////////////////////


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
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}