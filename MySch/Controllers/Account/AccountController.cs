using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.WX.Model;
using MySch.Dal;
using MySch.Filter;
using MySch.Helper;
using MySch.Models;
using MySch.Mvvm.Web.Role;
using MySch.Mvvm.Web.User;
using System;
using System.Text;
using System.Web.Mvc;

namespace MySch.Controllers.Account
{
    public class AccountController : RoleController
    {
        //用户检测
        public ActionResult Check()
        {
            var infor = WX_OAuserInfor.GetFromSession();
            infor.CheckUser();
            if (infor.isteach || infor.istudent)
            {
                return Json(new BllError { error = true, message = "已登记，无须重复设置" });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Update(string tname)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();

                //提交审核
                infor.AddTeach(tname);
                return Json(new BllError { error = false, message = tname });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult DataGrid(string text, int page = 1, int rows = 100)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();
                var res = VmAcc.DataGrid(text, infor.unionid, page, rows);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //[RoleRecFilter(AutoNameExt = false, IsMenu = true, Name = "用户管理", Order = 20, RoleTypeIDS = "04")]
        public ActionResult Exam()
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();
                if (infor.unionid != "o47ZhvxoQA9QOOgDSZ5hGaea4xdI" && infor.unionid != "o47ZhvzWPWSNS26vG_45Fuz5JMZk") throw new Exception("不是管理员，不好操作");

                return View();
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult PassExam(string id)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(WX_OAuserInfor.PassExam(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult UnExam(string id)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(WX_OAuserInfor.UnExam(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Fixed(string id)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(WX_OAuserInfor.Fixed(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult UnFixed(string id)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(WX_OAuserInfor.UnFixed(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult RoleGroup(VmAccRoleGroup entity)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();
                var roleids = VmRole.GetRoleGroupIDS(infor.unionid);
                var roles = VmRoleGroup.GetEntitys<VmRoleGroup>(a => a.IDS < roleids);
                ViewBag.RoleGroups = EasyUICombo.ToComboJsons<VmRoleGroup>(roles, entity.RoleGroupIDS.ToString());

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleGroupToken(VmAccRoleGroup entity)
        {
            try
            {
                var infor = WX_OAuserInfor.GetFromSession();
                var roleids = VmRole.GetRoleGroupIDS(infor.unionid);
                var roles = VmRoleGroup.GetEntitys<VmRoleGroup>(a => a.IDS < roleids);
                ViewBag.RoleGroups = EasyUICombo.ToComboJsons<VmRoleGroup>(roles, entity.RoleGroupIDS.ToString());

                entity.ToUpdate(ModelState);
                return Json(new BllError { error = false });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Del(VmAccDel entity)
        {
            try
            {
                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelToken(VmAccDel entity)
        {
            try
            {
                entity.ToDelete(ModelState);
                return Json(new BllError { error = false });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}