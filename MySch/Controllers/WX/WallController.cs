using MySch.Bll.WX.Model;
using MySch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.WX
{
    public class WallController : Controller
    {
        // GET: Wall
        public ActionResult Index()
        {
            try
            {
                //首页
                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        //请求页
        public ActionResult Req()
        {
            try
            {
                var url = WX_Url.MenuView(WX_Const.goneAppID, "http://a.jysycz.cn/wall/login", Guid.NewGuid().ToString("N"));
                return Redirect(url);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        //回调接口
        public ActionResult Login(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();

                //显示网页
                return RedirectToAction("Index", "Wall");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

    }
}