using MySch.Bll.Custom;
using MySch.Bll.Entity;
using MySch.Bll.WX.Model;
using MySch.Mvvm.User;
using MySch.Mvvm.Wall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Wall
{
    public class WxAccController : RoleController
    {
        private WX_OAuserInfor infor = WX_OAuserInfor.GetFromSession();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = VmWxAcc.GetEntity<VmWxAcc>(a => a.ID == id);
                return View(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(VmWxAcc entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                entity.ToUpdate(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        public ActionResult Del(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = VmWxAcc.GetEntity<VmWxAcc>(a => a.ID == id);
                return View(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(VmWxAcc entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                entity.ToDelete(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        [HttpPost]
        public ActionResult DataGrid(string text, int page = 1, int rows = 100)
        {
            try
            {
                //可以根据姓名、电话、昵称，查询相关信息
                var res = string.IsNullOrEmpty(text) ?
                    VqWxAcc.GetDataGridPages<VqWxAcc, string>(a => true, a => a.Name, page, rows) :
                    VqWxAcc.GetDataGridPages<VqWxAcc, string>(a => a.Name.Contains(text) || a.Mobil.Contains(text) || a.Mobils.Contains(text) || a.nickname.Contains(text), a => a.Name, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

    }
}