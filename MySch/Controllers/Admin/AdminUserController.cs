using MySch.Bll;
using MySch.Bll.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminUserController : RoleAdminController
    {
        //用户列表：界面
        [HttpPost]
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
        public ActionResult Edit(string id)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(id);
                return View(db);
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
                var db = BllAcc.GetEntity<BllAcc>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToken(BllAcc acc)
        {
            try
            {
                acc.ID = Guid.NewGuid().ToString("N");
                acc.RegTime = DateTime.Now;
                acc.Parent = BllLogin.GetLogin(Session).ID;
                acc.Pwd = BllLogin.Password(acc.IDS, acc.Pwd);

                //添加记录
                acc.ToAdd(ModelState);
                return Json(acc);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditToken(BllAcc acc)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(a => a.ID == acc.ID && a.IDS == acc.IDS);

                //密码如果改变，则重新加密
                acc.Pwd = acc.Pwd == db.Pwd ? acc.Pwd : BllLogin.Password(acc.IDS, acc.Pwd);
                //管理员admin帐号不能冻结
                acc.Fixed = acc.IDS == "admin" ? false : acc.Fixed;
                //别的属性直接从数据库拿出来
                acc.RegTime = db.RegTime;
                acc.Parent = db.Parent;
                //
                acc.ToUpdate(ModelState);

                return Json(acc);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(BllAcc acc)
        {
            try
            {
                //管理员帐号保护
                if (acc.IDS == "admin")
                {
                    throw new Exception("系统管理员，无法删除！");
                }

                //其它正常操作
                var db = BllAcc.GetEntity<BllAcc>(a => a.ID == acc.ID && a.IDS == acc.IDS);

                if (BllAcc.GetEntitys<List<BllAcc>>(a => a.Parent == db.ID).Count() != 0)
                {
                    throw new Exception("有下级用户，无法删除！");
                }
                else
                {
                    acc.ToDelete(ModelState);
                    return Json(acc);
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //用户列表：分页数据
        [HttpPost]
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);

                string myself = login.ID;
                string parent = login.Parent;

                var res = BllAcc.GetDataGridPages<BllAcc, string>(a => a.Parent == myself, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Search(string id)
        {
            try
            {
                //查询自己和自己的下属
                var login = BllLogin.GetLogin(Session);
                string myself = login.ID;
                string parent = login.Parent;
                //查询帐号、名称（只显示自己 及 下属）
                var res = BllAcc.GetDataGridEntitys<BllAcc>(a => (a.Name.Contains(id) || a.IDS.Contains(id)) && (a.Parent == myself || a.ID == myself));
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}