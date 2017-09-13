using MySch.Bll.Custom;
using MySch.Bll.Entity;
using MySch.Bll.WX.Model;
using MySch.Core;
using MySch.Mvvm.User;
using MySch.Mvvm.Wall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Wall
{
    public class WxAccSendController : RoleController
    {
        private WX_OAuserInfor infor = WX_OAuserInfor.GetFromSession();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DataGrid(string text, int page = 1, int rows = 100)
        {
            try
            {
                var res = string.IsNullOrEmpty(text) ?
                    VqWxAccSend.GetDataGridPagesDesc<VqWxAccSend, DateTime>(a => true, a => a.CreateTime, page, rows) :
                    VqWxAccSend.GetDataGridPagesDesc<VqWxAccSend, DateTime>(a => a.AccName.Contains(text), a => a.CreateTime, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

    }
}