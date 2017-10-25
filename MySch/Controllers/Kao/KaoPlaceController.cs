using MySch.Bll.Custom;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Kao
{
    public class KaoPlaceController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adds()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var db = BllKaoPlaceEdit.GetEntity<BllKaoPlaceEdit>(a => a.ID == id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Del(string id)
        {
            try
            {
                var db = BllKaoPlaceEdit.GetEntity<BllKaoPlaceEdit>(a => a.ID == id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllKaoPlace entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.AccIDS + entity.PlaceNo;

                //添加
                entity.ToAdd(ModelState);
                return Json(entity);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddsToken(KaoPlaceAdds entity)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var placestr = string.Empty;
                for (var i = 1; i <= entity.Num; i++)
                {
                    var place = new BllKaoPlace
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        IDS = login.IDS + i.ToString("D2"),
                        AccIDS = login.IDS,
                        PlaceNo = i.ToString("D2"),
                        Fixed = false,
                    };

                    if (BllKaoPlace.Count(a => a.IDS == place.IDS) == 0)
                    {
                        place.ToAdd();
                        placestr += (place.IDS + ",");
                    }
                }

                //返回添加的数据集
                var res = BllKaoPlace.GetDataGridPages<BllKaoPlace, string>(a => placestr.Contains(a.IDS), a => a.IDS, 1, 100);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllKaoPlaceEdit entity)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllKaoPlaceEdit entity)
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
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                var res = BllKaoPlace.GetDataGridPages<BllKaoPlace, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

    }
}