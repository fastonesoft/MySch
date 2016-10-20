using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Student
{
    public class StudEditController : RoleStudController
    {
        //学生注册首页
        [HttpPost]
        public ActionResult Index()
        {
            var login = BllLogin.GetLogin(Session);
            ViewBag.ID = login.IDS;

            return View();
        }

        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = VStudent.GetDataGridPages(a => a.CID == login.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}