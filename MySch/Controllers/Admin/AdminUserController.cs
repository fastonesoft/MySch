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
    public class AdminUserController : RoleController
    {
        [HttpPost]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(TAcc acc)
        {
            acc.GD = MySetting.GetGD("AdminUser", acc.ID);
            acc.RegTime = DateTime.Now;
            acc.Parent = MyLogin.GetLogin(Session).GD;
            acc.Pwd = MyLogin.Password(acc.ID, acc.GD, MySetting.GetMD5(acc.Pwd));
            //添加记录
            DataADU<TAcc>.Add(ModelState, acc);
            return Json(acc);
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            var db = DataQuery<TAcc>.Entity(a => a.ID == id);
            if (db == null)
            {
                return Json(new ErrorModel { error = true, message = "查询数据出错" });
            }
            return View(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTokey(TAcc acc)
        {
            var db = DataQuery<TAcc>.Entity(a => a.GD == acc.GD && a.ID == acc.ID);
            if (db == null)
            {
                return Json(new ErrorModel { error = true, message = "提交数据有误" });
            }
            else
            {
                //密码如果改变，则重新加密
                acc.Pwd = acc.Pwd == db.Pwd ? acc.Pwd : MyLogin.Password(acc.ID, acc.GD, MySetting.GetMD5(acc.Pwd));
                //管理员admin帐号不能冻结
                acc.Fixed = acc.ID == "admin" ? false : acc.Fixed;
                //别的属性直接从数据库拿出来
                acc.RegTime = db.RegTime;
                acc.Parent = db.Parent;
                DataADU<TAcc>.Update(ModelState, acc);
                return Json(acc);
            }
        }

        [HttpPost]
        public ActionResult Del(string id)
        {
            var db = DataQuery<TAcc>.Entity(a => a.ID == id);
            if (db == null)
            {
                return Json(new ErrorModel { error = true, message = "查询数据出错" });
            }
            return View(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(TAcc acc)
        {
            //管理员帐号保护
            if (acc.ID == "admin")
            {
                return Json(new ErrorModel { error = true, message = "系统管理员，无法删除！" });
            }
            //其它正常操作
            var db = DataQuery<TAcc>.Entity(a => a.GD == acc.GD && a.ID == acc.ID);
            if (db == null)
            {
                return Json(new ErrorModel { error = true, message = "提交数据有误" });
            }
            else
            {
                if (DataQuery<TAcc>.Expression(a => a.Parent == db.GD).Count() != 0)
                {
                    return Json(new ErrorModel { error = true, message = "有下级用户，无法删除" });
                }
                else
                {
                    DataADU<TAcc>.Delete(db);
                    return Json(db);
                }
            }
        }

        [HttpPost]
        public ActionResult Search(string text)
        {
            //管理员ParentGD为null，可以查询全部
            //非管理员，只可以查询自己和自己的下属
            string myself = MyLogin.GetLogin(Session).GD;
            string parent = MyLogin.GetLogin(Session).Parent;
            var db = parent == null ?
                DataQuery<TAcc>.Expression(a => a.Name.Contains(text) || a.ID.Contains(text)) :
                DataQuery<TAcc>.Expression(a => (a.Name.Contains(text) || a.ID.Contains(text)) && (a.Parent == myself || a.GD == myself));

            var res = new
            {
                total = db.Count(),
                rows = db
            };
            return Json(res);
        }
    }
}