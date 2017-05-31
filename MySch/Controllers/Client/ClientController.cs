using MySch.Bll;
using MySch.Bll.Entity;
using MySch.Bll.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.Client
{
    public class ClientController : RoleController
    {
        //返回结果数据格式
        private class HtmlScript
        {
            public string Html { get; set; }
            public string Script { get; set; }
        }

        //页面合并，递归
        private HtmlScript Merge(string pageIDS, string whitespace)
        {
            whitespace += "    ";
            var page = BllPage.GetEntity<BllPage>(a => a.IDS == pageIDS);
            var html = HttpUtility.UrlDecode(page.Html);
            var script = HttpUtility.UrlDecode(page.Script);

            var subs = BllPage.GetEntitys<BllPage>(a => a.ParentID == page.ID && a.Fixed == false).OrderBy(a => a.IDS);
            if (subs.Count() == 0)
            {
                html = string.Format("\n{2}<!--编码开始 {0}-->\n{1}\n{2}<!--编码结束 {0}-->", page.Name, html, whitespace);
                script = string.Format("\n<!--脚本开始 {0}-->\n{1}\n<!--脚本结束 {0}-->", page.Name, script);
                return new HtmlScript { Html = html, Script = script };
            }
            else
            {
                html = string.Format("\n{2}<!--编码开始 {0}-->\n{1}\n{2}<!--编码结束 {0}-->", page.Name, html, whitespace);
                script = string.Format("\n\n<!--脚本开始 {0}-->\n{1}\n<!--脚本结束 {0}-->", page.Name, script);
                List<string> param = new List<string>();
                foreach (var sub in subs)
                {
                    var res = Merge(sub.IDS, whitespace);
                    param.Add(res.Html);
                    script += res.Script;
                }
                //检测是否存在插入点
                int mc = Regex.Matches(html, @"{\d+}").Count;
                //多的，追加
                html = string.Format(html, param.ToArray());
                for (int i = mc; i < param.Count; i++)
                {
                    html += param[i];
                }
                return new HtmlScript { Html = html, Script = script };
            }
        }
        // GET: Client
        public ActionResult Index()
        {
            //var login = BllLogin.GetLogin(Session);
            //ViewBag.UserName = login.Name;
            ////ViewBag.Contents = ";alsdflka;kjlsdfkjlasjl;kdf;lasdkljfa;klsdfl;kaslkdkjlaslkjasdfl;ksdal;sdfal;kj";

            return RedirectToAction("Index", "Account");

            return View();
        }

        public ActionResult Page(string id = null)
        {
            try
            {
                if (id == null)
                {
                    var entitys = BllPage.GetEntitys<BllPage>(a => a.Bootup).SingleOrDefault();
                    if (entitys != null) id = entitys.ID;
                }

                var res = Merge(id, "");
                return Content(res.Html + res.Script);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}