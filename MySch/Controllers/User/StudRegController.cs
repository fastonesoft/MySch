using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.User
{
    public class StudRegController : RoleController
    {
        //学生注册首页
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReg()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddToKen(StudRegValid reg)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        //验证成功，抓取网络数据

        //    }
        //}
    }
}