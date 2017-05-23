using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_UserStud
    {
        public string idc { get; set; }
        public string name { get; set; }
        public string openid { get; set; }
        public static WX_UserStud OpenID(string idc)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity != null)
                {
                    return new WX_UserStud { idc = idc, name = entity.Name, openid = entity.OpenID };
                }
                throw new Exception("无法识别的扫码信息");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}