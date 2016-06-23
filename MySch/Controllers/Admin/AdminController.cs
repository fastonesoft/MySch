using MySch.Dal;
using MySch.Models;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminController : RoleAController
    {
        //管理员：首页
        [HttpPost]
        public ActionResult Index()
        {
            ViewBag.UserName = MyLogin.GetLogin(Session).Name;
            return View();
        }

        //用户列表：界面
        [HttpPost]
        public ActionResult Users()
        {
            return View();
        }

        //用户列表：分页数据
        [HttpPost]
        public ActionResult UsersPage(int page = 1, int rows = 100)
        {
            int many, total;
            string myself = MyLogin.GetLogin(Session).GD;
            string parent = MyLogin.GetLogin(Session).Parent;
            var db = parent == null ?
                DataTake<TAcc>.TakePage<string>(a => true, a => a.ID, page, rows, out many, out total) :
                DataTake<TAcc>.TakePage<string>(a => a.Parent == myself || a.GD == myself, a => a.ID, page, rows, out many, out total);

            var res = new
            {
                total = total,
                rows = db
            };
            return Json(res);
        }


    }
}