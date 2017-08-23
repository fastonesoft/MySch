using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Web.User.Act
{
    public class ActAcc
    {
        public static IEnumerable<VqAccBan> AccBanMaster(string ids, string unionid)
        {
            try
            {
                var bans = DataCRUD<QrBanCurrent>.Entitys(a => a.AccIDS == ids && a.IsCurrent.HasValue && a.IsCurrent.Value);
                var banstr = string.Empty;
                foreach (var ban in bans)
                {
                    banstr += (string.IsNullOrEmpty(ban.MasterIDS) ? "" : ban.MasterIDS + ",");
                }
                return VqAccBan.GetEntitys<VqAccBan>(a => a.ParentID == unionid && a.Passed && !a.Fixed && !banstr.Contains(a.ID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
