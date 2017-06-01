using MySch.Bll.Func;
using MySch.Bll.WX.Model;
using MySch.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.WX
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            try
            {
                var state = "pub";
                var appid = WX_Const.goneAppID;
                var regurl = "http://a.jysycz.cn/regs/";
                var examurl = "http://a.jysycz.cn/regs/examine";

                var regform = WX_Url.MenuView(appid, regurl, state);
                var regexam = WX_Url.MenuView(appid, examurl, state);

                //检测页面、用户
                var token = WX_AccessToken.GetAccessToken();

                var menus = new WX_Menu();
                var student = new WX_Menu_Sub { name = "学生相关" };
                student.Add(new WX_Menu_View { name = "报名注册", type = "view", url = regform });
                student.Add(new WX_Menu_View { name = "材料审核", type = "view", url = regexam });
                menus.Add(student);
                var teach = new WX_Menu_Sub { name = "教师中心" };
                teach.Add(new WX_Menu_View { name = "报名注册", type = "view", url = regform });
                teach.Add(new WX_Menu_View { name = "材料审核", type = "view", url = regexam });
                menus.Add(teach);
                var score = new WX_Menu_Sub { name = "成绩查询" };
                score.Add(new WX_Menu_View { name = "报名注册", type = "view", url = regform });
                score.Add(new WX_Menu_View { name = "材料审核", type = "view", url = regexam });
                menus.Add(score);

                //创建
                return Json(WX_Url.MenuCreate(token, Jsons.ToJsonsConvert(menus)));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
    }
}