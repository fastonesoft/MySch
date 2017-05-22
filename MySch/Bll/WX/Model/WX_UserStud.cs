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
        public static string OpenID(string idc)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity != null)
                {
                    return entity.OpenID;
                }
                throw new Exception("身份证号未注册");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}