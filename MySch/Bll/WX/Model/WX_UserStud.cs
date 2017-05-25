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
        public string id { get; set; }
        public string idc { get; set; }
        public string name { get; set; }
        public string reguid { get; set; }
        public static WX_UserStud RegUID(string idc)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity != null)
                {
                    if (entity.Examed)
                    {
                        throw new Exception(string.Format("【{0}】的资料已通过审核", entity.Name));
                    }
                    else
                    {
                        return new WX_UserStud { id = entity.ID, idc = entity.IDC, name = entity.Name, reguid = entity.RegUID };
                    }
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