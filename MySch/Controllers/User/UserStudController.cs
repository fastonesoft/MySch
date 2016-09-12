using MySch.Bll;
using MySch.Bll.Action;
using MySch.Bll.Entity;
using MySch.Bll.Model;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class UserStudController : RoleController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add()
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = EasyCombo.ToEasyComboJsons<BllPart>(parts, null);
                ViewBag.Steps = EasyCombo.ToEasyComboJsons<BllStep>(steps, null);

                return View();
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
                var entity = BllPartStep.GetEntity<BllPartStep>(id);

                var login = BllLogin.GetLogin(Session);
                var parts = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                var steps = BllStep.GetEntitys<BllStep>(a => a.AccIDS == login.IDS).OrderBy(a => a.IDS);
                ViewBag.Parts = EasyCombo.ToEasyComboJsons<BllPart>(parts, entity.PartIDS);
                ViewBag.Steps = EasyCombo.ToEasyComboJsons<BllStep>(steps, entity.StepIDS);

                return View(entity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTokey(BllPartStep entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                entity.ID = Guid.NewGuid().ToString("N");
                entity.IDS = entity.PartIDS + entity.StepIDS.Replace(entity.AccIDS, "");

                //添加
                entity.ToAdd(ModelState);
                //查询 视图数据
                var qentity = QllPartStep.GetEntity<QllPartStep>(a => a.ID == entity.ID);
                return Json(qentity);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelTokey(BllPartStep entity)
        {
            try
            {
                //设置用户
                var login = BllLogin.GetLogin(Session);
                entity.AccIDS = login.IDS;

                //查询 视图数据 保存
                var qentity = QllPartStep.GetEntity<QllPartStep>(a => a.ID == entity.ID);
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
        public ActionResult DataGrid(int page = 1, int rows = 100)
        {
            try
            {
                var login = BllLogin.GetLogin(Session);
                var res = QllGradeStud.GetDataGridPages<QllGradeStud, string>(a => true, a => a.IDS, page, rows, OrderType.ASC);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GradeTree(string id = null)
        {
            var login = BllLogin.GetLogin(Session);
            if (id == null)
            {
                var entitys = BllPart.GetEntitys<BllPart>(a => a.AccIDS == login.IDS && a.Fixed == false);
                var res = EasyTree.ToTreeJsons<BllPart>(entitys);
                return Json(res);
            }
            else
            {
                var entitys = QllGrade.GetEntitys<QllGrade>(a => a.AccIDS == login.IDS && a.PartIDS == id);
                var res = EasyTree.ToTreeJsons<QllGrade>(entitys);
                return Json(res);
            }
        }

        [HttpPost]
        public ActionResult GradeCheck(IEnumerable<QllGradeStud> rows)
        {
            try
            {
                var cookies = AutoXue.Login("http://58.213.155.172/uids/index.jsp",
                         "http://58.213.155.172/uids/genImageCode?rnd=" + DateTime.Now.Ticks.ToString(),
                         "http://58.213.155.172/uids/login!login.action", "c32128441402", "==QTuhWMaVlWoN2MSFXYR1TP");

                int count = 0;
                foreach (var stud in rows)
                {
                    var student = AutoXue.GetStudent(stud.StudName, stud.CID, cookies);

                    XueDetail xue = Jsons<IEnumerable<XueDetail>>.JsonEntity(student).First();

                    BllStudentIn ins = BllStudentIn.GetEntity<BllStudentIn>(a => a.IDS == stud.StudIDS);
                    ins.Name1 = xue.first_guardian_name;
                    ins.Mobil1 = xue.first_guardian_phone;
                    ins.Name2 = xue.second_guardian_name;
                    ins.Mobil2 = xue.second_guardian_phone;
                    ins.Birth = xue.birth_place;
                    ins.Home = xue.home_address;

                    ins.Checked = true;
                    ins.ToUpdate();

                    count++;
                }

                return Json(new BllError { error = true, message = string.Format("转换成功{0}", count) });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }

        }
    }
}