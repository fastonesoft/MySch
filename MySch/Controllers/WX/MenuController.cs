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
                var regurl = HttpUtility.UrlEncode("http://a.jysycz.cn/regs/");
                var examurl = HttpUtility.UrlEncode("http://a.jysycz.cn/regs/examine");

                var regform = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, regurl, state);
                var regexam = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, examurl, state);

                //检测页面、用户
                var token = WX_AccessToken.GetAccessToken();

                var menus = new WX_Menu();
                var student = new WX_Menu_Sub { name = "学生" };
                student.Add(new WX_Menu_View { name = "报名注册", type = "view", url = regform });
                student.Add(new WX_Menu_View { name = "材料审核", type = "view", url = regexam });
                menus.Add(student);
                var family = new WX_Menu_Sub { name = "家长" };
                menus.Add(family);
                var teach = new WX_Menu_Sub { name = "教师" };
                menus.Add(teach);



                //创建
                WX_Url.MenuCreate(token, Jsons.ToJsons(menus));
                return Content(token + "：" + Jsons.ToJsons(menus));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
    }
}