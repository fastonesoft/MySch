using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class UserYearController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddYear()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult EditYear(string id)
        {
            try
            {
                var entity = BllYear.GetEntity<BllYear>(id);
                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelYear(string id)
        {
            try
            {
                var entity = BllYear.GetEntity<BllYear>(id);
                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllYear year)
        {
            try
            {
                if(year.IsCurrent)
                {
                    //清除当前
                    BllYear.UnSelectCurrent();
                }
                //设置用户
                var login = BllLogin.GetLogin(Session);
                year.AccIDS = login.IDS;
                year.ID = Guid.NewGuid().ToString("N");
                //添加
                year.ToAdd(ModelState);
                //查询 视图数据
                return Json(year);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllYear year)
        {
            try
            {
                if(year.IsCurrent)
                {
                    //清除当前
                    BllYear.UnSelectCurrent();
                }
                //更新
                year.ToUpdate(ModelState);
                //查询 视图数据
                return Json(year);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllYear year)
        {
            try
            {
                //查询 视图数据 保存
                //删除
                year.ToDelete(ModelState);
                return Json(year);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var res = BllYear.GetDataGridPages<BllYear, string>(a => a.AccIDS == login.IDS, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}