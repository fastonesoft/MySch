using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Core;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySch.Bll.Custom;

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
                var db = BllStud.GetEntity<BllStud>(a => a.IDS == entity.StudIDS);

                var res = new List<EasyUIpGrid>();
                res.Add(EasyUIpGrid.PItem(db, "IDS", "学生编号", "基本信息", ""));
                res.Add(EasyUIpGrid.PItem(db, "Name", "姓名", "基本信息", ""));
                res.Add(EasyUIpGrid.PItem(db, "IDC", "身份证号", "基本信息", ""));
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
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GradeTree(string id = null, string memo = null)
        {
            try
            {
                //直接获取：我的学生对应的
                //校区、年级、班级
                var login = BllLogin.GetLogin(Session);
                var bans = ViSchBan.GetEntitys<ViSchBan>(a => a.MasterIDS == login.IDS);


                //校区
                if (id == null)
                {
                    var entitys = ViBanTree.PartTree(bans);
                    var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Part");
                    return Json(res);
                }
                else
                {
                    //年级
                    if (memo == "Part")
                    {
                        var entitys = ViBanTree.GradeTree(bans, id);
                        var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Grade");
                        return Json(res);
                    }
                    else
                    {
                        //班级
                        var entitys = ViBanTree.BanTree(bans, id);
                        var res = EasyUITree.ToTree(entitys, "IDS", "Name", "open", "Class");
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
        public ActionResult DataGrid(string id = null, string text = null, int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                //获取当前帐号所对应的班级
                var bansID = ViSchBan.GetEntitys(a => a.MasterIDS == login.IDS, "IDS");
                var bantext = string.Join("-", bansID);

                var res = string.IsNullOrEmpty(text) ?
                    VGradeStud.GetDataGridPages(a => a.BanIDS == id, page, rows) :
                    VGradeStud.GetDataGridPages(a => bantext.Contains(a.BanIDS) && (a.IDC.Contains(text) || a.StudName.Contains(text)), page, rows);

                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }
    }
}