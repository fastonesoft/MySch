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
        public static dynamic AccBanMaster(string ids, string unionid)
        {
            try
            {
                var acc = VqAccBan.GetEntitys<VqAccBan>(a => a.ParentID == unionid);
                var bans = DataCRUD<QrBanCurrent>.Entitys(a => a.AccIDS == ids && a.IsCurrent.HasValue && a.IsCurrent.Value);

                var banstr = string.Empty;
                foreach (var ban in bans)
                {
                    banstr += (string.IsNullOrEmpty(ban.MasterIDS) ? "" : ban.MasterIDS + ",");
                }
                return acc.Select(a => !banstr.Contains(a.ID) && a.Passed && !a.Fixed);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
