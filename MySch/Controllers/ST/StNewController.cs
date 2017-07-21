using MySch.Bll;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Controllers.ST
{
    public class StNewController : RoleController
    {
        // GET: StNew
        public ActionResult Index()
        {
            return View();
        }

        //编号查询
        [HttpPost]
        public ActionResult Search(string id)
        {
            try
            {
                Regex regx = new Regex(@"^%(\d+)$|^(\d*[xX]?)$");
                Match match = regx.Match(id);
                if (match.Success)
                {
                    string left = match.Groups[1].ToString();
                    string all = match.Groups[2].ToString();

                    var entitys = left.Length > 0 ? DataCRUD<Student>.Entitys(a => a.RegNo.StartsWith(left)).OrderBy(a => a.RegNo) :
                        all.Length > 0 ? DataCRUD<Student>.Entitys(a => a.RegNo.Contains(all) || a.IDS.Contains(all)).OrderBy(a => a.RegNo) : null;

                    var res = new { total = entitys.Count(), rows = entitys };

                    return Json(res);
                }
                else
                {
                    return Json(new BllError { error = true, message = "查询格式：前缀%*，包含*，后缀*%" });
                }
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }


        [HttpPost]
        public ActionResult Print(IEnumerable<Student> rows)
        {
            ViewBag.StudNo = DataCRUD<TPrint>.Entity(a => a.Name == "No");
            ViewBag.StudName = DataCRUD<TPrint>.Entity(a => a.Name == "Name");
            ViewBag.School = DataCRUD<TPrint>.Entity(a => a.Name == "School");

            return View(rows);
        }

        [HttpPost]
        public ActionResult PrintPos(IEnumerable<TPrint> pos)
        {
            try
            {
                foreach (var d in pos)
                {
                    DataCRUD<TPrint>.Update(d);
                }
                return Json(new BllError { error = false, message = "打印位置已修改，关闭窗口重来！" });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

    }
}