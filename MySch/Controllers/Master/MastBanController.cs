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