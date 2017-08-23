using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.Model;
using MySch.Bll.View;
using MySch.Mvvm.Web.User;
using MySch.Mvvm.Web.User.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserBanController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = BllAcc.GetEntitys<BllAcc>(a => a.ParentID == login.unionid).OrderBy(a => a.IDS);

                ViewBag.Grades = EasyUICombo.ToComboJsons(grades, id);
                ViewBag.Masters = EasyUICombo.ToComboJsons<BllAcc>(accs, null);

                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Adds(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = BllAcc.GetEntitys<BllAcc>(a => a.ParentID == login.unionid).OrderBy(a => a.IDS);

                ViewBag.Grades = EasyUICombo.ToComboJsons(grades, id);
                ViewBag.Masters = EasyUICombo.ToComboJsons<BllAcc>(accs, null);

                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            try
            {
                var entity = BllBan.GetEntity<BllBan>(id);

                var login = BllLogin.GetLogin(Session);
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = ActAcc.AccBanMaster(login.IDS, entity.MasterIDS, login.unionid);

                ViewBag.Grades = EasyUICombo.ToComboJsons(grades, entity.GradeIDS);
                ViewBag.Masters = EasyUICombo.ToComboJsons<VqAccBan>(accs, "ID", "Name", entity.MasterIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Del(string id)
        {
            try
            {
                var entity = BllBan.GetEntity<BllBan>(id);

                var login = BllLogin.GetLogin(Session);
                var grades = VGrade.GetEntitys(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var accs = BllAcc.GetEntitys<BllAcc>(a => a.ParentID == login.unionid).OrderBy(a => a.IDS);

                ViewBag.Grades = EasyUICombo.ToComboJsons(grades, entity.GradeIDS);
                ViewBag.Masters = EasyUICombo.ToComboJsons<BllAcc>(accs, entity.MasterIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllBan entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.GradeIDS + entity.Num;

                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var qentity = VBan.GetEntity(a => a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddsToken(BanAdds entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);

                var banids = string.Empty;
                for (int i = 1; i <= entity.Num; i++)
                {
                    //生成每一个记录
                    var ban = new BllBan
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        IDS = entity.GradeIDS + i.ToString("D2"),
                        Num = i.ToString("D2"),
                        NotFeng = false,
                        OnlyFixed = true,
                        AccIDS = login.IDS,
                        GradeIDS = entity.GradeIDS,
                        ChangeNum = 10,
                        Differ = 5,
                        IsAbs = false,
                    };
                    //不存在，添加
                    if (BllBan.Count(a => a.IDS == ban.IDS) == 0)
                    {
                        ban.ToAdd(ModelState);
                        banids += (ban.IDS + ",");
                    }
                }

                //返回添加的数据集
                var res = VBan.GetDataGridPages(a => banids.Contains(a.IDS), 1, 100);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllBan entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //更新
                entity.ToUpdate(ModelState);
                //查询 视图数据
                var qentity = BllBan.GetEntity<BllBan>(entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllBan entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //查询 视图数据 保存
                var qentity = VBan.GetEntity(a => a.ID == entity.ID);
                //删除
                entity.ToDelete(ModelState);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult MenuTree(string id = null)
        {
            var login = BllLogin.GetLogin(Session);
            //校区：所有
            if (id == null)
            {
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS && a.Fixed == false).OrderBy(a => a.IDS);
                var res = EasyUITree.ToTree(entitys, "IDS", "Name", "closed", "Part");
                return Json(res);
            }
            else
            {
                var entitys = VGrade.GetEntitys(a => a.AccIDS == login.IDS && a.PartIDS == id && a.IsCurrent).OrderBy(a => a.IDS);
                var res = EasyUITree.ToTree(entitys, "IDS", "TreeName", "open", "Grade");
                return Json(res);
            }
        }

        [HttpPost]
        public ActionResult DataGrid(string id = null, int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var res = id == null ?
                    VBan.GetDataGridPages(a => a.AccIDS == login.IDS, page, rows) :
                    VBan.GetDataGridPages(a => a.AccIDS == login.IDS && a.GradeIDS == id, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetCombos(string id)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var bans = VBan.GetEntitys(a => a.GradeIDS == id);
                return Json(EasyUICombo.ToCombo<VBan>(bans, "IDS", "TreeName", null));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}