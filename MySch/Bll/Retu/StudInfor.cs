using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Retu
{
    public class StudInfor
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public static StudInfor StudRexamine(string idc)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity == null) throw new Exception("未查询到当前学生信息");
                if (!entity.Examed) throw new Exception(string.Format("【{0}】还未通过审核，不必退回",entity.Name));

                //可以退回，返回学生信息
                return new StudInfor { ID = entity.ID, Name = entity.Name};
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}