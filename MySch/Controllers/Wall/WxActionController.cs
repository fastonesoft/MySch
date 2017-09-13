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
    public class WxActionController : RoleController
    {
        private WX_OAuserInfor infor = WX_OAuserInfor.GetFromSession();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                return View();
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(VmWxAction entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                if (entity.IsCurrent) entity.ClearCurrent();
                entity.ID = Guid.NewGuid().ToString("N");
                entity.ToAdd(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        public ActionResult Edit(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = VmWxAction.GetEntity<VmWxAction>(id);
                return View(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(VmWxAction entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                if (entity.IsCurrent) entity.ClearCurrent();
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

                var res = VmWxAction.GetEntity<VmWxAction>(id);
                return View(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(VmWxAction entity)
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
                var res = string.IsNullOrEmpty(text) ?
                    VqWxAction.GetDataGridPagesAsc<VqWxAction, string>(a => true, a => a.Name, page, rows) :
                    VqWxAction.GetDataGridPagesAsc<VqWxAction, string>(a => a.Name.Contains(text), a => a.Name, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

    }
}