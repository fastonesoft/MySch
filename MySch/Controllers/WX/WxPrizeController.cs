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

namespace MySch.Controllers.WX
{
    public class WxPrizeController : RoleController
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
        public ActionResult AddToken(VmWxPrize entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

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

                var res = VmWxPrize.GetEntity<VmWxPrize>(id);
                return View(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(VmWxPrize entity)
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

                var res = VmWxPrize.GetEntity<VmWxPrize>(id);
                return View(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(VmWxPrize entity)
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
                    VqWxPrize.GetDataGridPagesAsc<VqWxPrize, string>(a => true, a => a.IDS, page, rows) :
                    VqWxPrize.GetDataGridPagesAsc<VqWxPrize, string>(a => a.Name.Contains(text), a => a.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

    }
}