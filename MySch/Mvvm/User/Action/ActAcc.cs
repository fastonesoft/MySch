using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.User.Action
{
    public class ActAcc
    {
        public static IEnumerable<VqAccBan> AccBanMaster(string ids, string masterids, string unionid)
        {
            try
            {
                //当前年度的班级
                var banstr = string.Empty;
                using (var db = new BaseContext())
                {
                    var entitys = from b in db.TBans
                                  join g in db.TGrades on b.GradeIDS equals g.IDS
                                  join y in db.TYears on g.YearIDS equals y.IDS
                                  where g.AccIDS == ids && y.IsCurrent && !string.IsNullOrEmpty(b.MasterIDS)
                                  select b.MasterIDS;

                    //拼接
                    if (entitys.Count() > 0) banstr = string.Join(",", entitys);
                    if (masterids != null) banstr = banstr.Replace(masterids, "");
                }
                //未使用的用户
                return VqAccBan.GetEntitys<VqAccBan>(a => a.ParentID == unionid && a.Passed && !a.Fixed && !banstr.Contains(a.ID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
