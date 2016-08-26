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
    public class UserTermController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTerm()
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Years = Combo.ToComboJsons<BllYear>(years, null);
                
                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult EditTerm(string id)
        {
            try
            {
                var entity = BllTerm.GetEntity<BllTerm>(id);

                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Years = Combo.ToComboJsons<BllYear>(years, entity.YearIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelTerm(string id)
        {
            try
            {
                var entity = BllTerm.GetEntity<BllTerm>(id);

                var login = BllLogin.GetLogin(Session);
                var years = BllYear.GetEntitys<BllYear>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Years = Combo.ToComboJsons<BllYear>(years, entity.YearIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllTerm term)
        {
            try
            {
                if(term.IsCurrent)
                {
                    //清除当前
                    BllTerm.UnSelectCurrent();
                }
                //设置用户
                var login = BllLogin.GetLogin(Session);
                term.AccIDS = login.IDS;
                term.ID = Guid.NewGuid().ToString("N");
                //添加
                term.ToAdd(ModelState);
                //查询 视图数据
                return Json(term);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllTerm term)
        {
            try
            {
                if(term.IsCurrent)
                {
                    //清除当前
                    BllTerm.UnSelectCurrent();
                }
                //更新
                term.ToUpdate(ModelState);
                //查询 视图数据
                return Json(term);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllTerm term)
        {
            try
            {
                //查询 视图数据 保存
                //删除
                term.ToDelete(ModelState);
                return Json(term);
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
                var res = QllTerm.GetDataGridQPages(a => a.AccIDS == login.IDS, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}