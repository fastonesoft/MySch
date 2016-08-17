using MySch.Bll;
using MySch.Dal;
using MySch.ModelsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Admin
{
    public class AdminUserController : RoleController
    {
        //用户列表：界面
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllAcc acc)
        {
            try
            {
                acc.ID = MySetting.GetGD("AdminUser", acc.IDS);
                acc.RegTime = DateTime.Now;
                acc.Parent = MyLogin.GetLogin(Session).ID;
                acc.Pwd = MyLogin.Password(acc.IDS, acc.ID, MySetting.GetMD5(acc.Pwd));

                //添加记录
                acc.ToAdd(ModelState);
                return Json(acc);
            }
            catch (Exception e)
            {
                return Json(new ErrorModel { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult EditUser(string id)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorModel { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(BllAcc acc)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(a => a.ID == acc.ID && a.IDS == acc.IDS);

                //密码如果改变，则重新加密
                acc.Pwd = acc.Pwd == db.Pwd ? acc.Pwd : MyLogin.Password(acc.IDS, acc.ID, MySetting.GetMD5(acc.Pwd));
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
                return Json(new ErrorModel { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DelUser(string id)
        {
            try
            {
                var db = BllAcc.GetEntity<BllAcc>(id);
                return View(db);
            }
            catch (Exception e)
            {
                return Json(new ErrorModel { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllAcc acc)
        {
            try
            {
                //管理员帐号保护
                if (acc.IDS == "admin")
                {
                    return Json(new ErrorModel { error = true, message = "系统管理员，无法删除！" });
                }

                //其它正常操作
                var db = BllAcc.GetEntity<BllAcc>(a => a.ID == acc.ID && a.IDS == acc.IDS);

                if (BllAcc.GetEntitys<List<BllAcc>>(a => a.Parent == db.ID).Count() != 0)
                {
                    return Json(new ErrorModel { error = true, message = "有下级用户，无法删除" });
                }
                else
                {
                    acc.ToDelete(ModelState);
                    return Json(acc);
                }
            }
            catch (Exception e)
            {
                return Json(new ErrorModel { error = true, message = e.Message });
            }
        }

        //[HttpPost]
        //public ActionResult SearchUser(string text)
        //{
        //    //管理员ParentGD为null，可以查询全部
        //    //非管理员，只可以查询自己和自己的下属
        //    string myself = MyLogin.GetLogin(Session).GD;
        //    string parent = MyLogin.GetLogin(Session).Parent;
        //    var db = parent == null ?
        //        DataCRUD<TAcc>.Expression(a => a.Name.Contains(text) || a.ID.Contains(text)) :
        //        DataCRUD<TAcc>.Expression(a => (a.Name.Contains(text) || a.ID.Contains(text)) && (a.Parent == myself || a.GD == myself));

        //    var res = new { total = db.Count(), rows = db };
        //    return Json(res);
        //}
    }
}