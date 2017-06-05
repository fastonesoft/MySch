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
    public class MenuController : RoleController
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
                var rexamurl = "http://a.jysycz.cn/regs/rexamine";
                var manaurl = "http://a.jysycz.cn/regs/manadd";

                var reg = WX_Url.MenuView(appid, regurl, state);
                var exam = WX_Url.MenuView(appid, examurl, state);
                var rexam = WX_Url.MenuView(appid, rexamurl, state);
                var manadd = WX_Url.MenuView(appid, manaurl, state);

                //检测页面、用户
                var token = WX_AccessToken.GetAccessToken();

                var menus = new WX_Menu();
                var student = new WX_Menu_Sub { name = "学生相关" };
                student.Add(new WX_Menu_View { name = "报名注册", type = "view", url = reg });
                menus.Add(student);
                var teach = new WX_Menu_Sub { name = "教师中心" };
                teach.Add(new WX_Menu_View { name = "材料审核", type = "view", url = exam });
                teach.Add(new WX_Menu_View { name = "审核退回", type = "view", url = rexam });
                teach.Add(new WX_Menu_View { name = "手动添加", type = "view", url = manadd });
                menus.Add(teach);
                var score = new WX_Menu_Sub { name = "成绩查询" };
                score.Add(new WX_Menu_View { name = "分班测试", type = "view", url = "http://a.jysycz.cn/" });
                score.Add(new WX_Menu_View { name = "平时成绩", type = "view", url = "http://a.jysycz.cn/" });
                menus.Add(score);

                var res = WX_Url.MenuCreate(token, Jsons.ToConvert(menus));

                //创建
                return Content(res);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
    }
}