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
                var scanurl = "http://a.jysycz.cn/regs/scan";
                var examurl = "http://a.jysycz.cn/regs/examine";
                var rexamurl = "http://a.jysycz.cn/regs/rexamine";
                var manaurl = "http://a.jysycz.cn/regs/addmana";
                var outurl = "http://a.jysycz.cn/regs/addout";
                var houseurl = "http://a.jysycz.cn/regs/house";
                var masterurl = "http://a.jysycz.cn/regs/master";
                var goneurl = "http://a.jysycz.cn/regs/gone";

                var reg = WX_Url.MenuView(appid, regurl, state);
                var scan = WX_Url.MenuView(appid, scanurl, state);
                var exam = WX_Url.MenuView(appid, examurl, state);
                var rexam = WX_Url.MenuView(appid, rexamurl, state);
                var addmana = WX_Url.MenuView(appid, manaurl, state);
                var addout = WX_Url.MenuView(appid, outurl, state);
                var house = WX_Url.MenuView(appid, houseurl, state);
                var master = WX_Url.MenuView(appid, masterurl, state);
                var gone = WX_Url.MenuView(appid, goneurl, state);

                //检测页面、用户
                var token = WX_AccessToken.GetAccessToken();

                var menus = new WX_Menu();
                var student = new WX_Menu_Sub { name = "学生相关" };
                student.Add(new WX_Menu_Click { name = "🈸报名须知", type = "click", key = "reg_about_1" });
                student.Add(new WX_Menu_View { name = "🅰报名注册", type = "view", url = reg });
                student.Add(new WX_Menu_View { name = "🅱绑定学生", type = "view", url = scan });
                menus.Add(student);
                var teach = new WX_Menu_Sub { name = "教师中心" };
                teach.Add(new WX_Menu_View { name = "👀材料初审", type = "view", url = exam });
                teach.Add(new WX_Menu_View { name = "🅾材料复核", type = "view", url = rexam });
                teach.Add(new WX_Menu_View { name = "💪手动注册", type = "view", url = addmana });
                teach.Add(new WX_Menu_View { name = "🎅外省添加", type = "view", url = addout });
                teach.Add(new WX_Menu_View { name = "🏠房产核查", type = "view", url = house });
                menus.Add(teach);
                var score = new WX_Menu_Sub { name = "成绩查询" };
                score.Add(new WX_Menu_View { name = "👐班级主管", type = "view", url = master });
                score.Add(new WX_Menu_View { name = "💯公共关系", type = "view", url = gone });
                menus.Add(score);

                var res = WX_Url.MenuCreate(token, Jsons.ToConvert(menus));

                //创建
                return Content(house +"\n" + gone);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
    }
}