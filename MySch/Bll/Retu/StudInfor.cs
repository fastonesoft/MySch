using MySch.Bll.Func;
using MySch.Bll.WX.Model;
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
        public static BllError StudRexamine(string idc)
        {
            try
            {
                var error = IDC.IDS(idc);
                if (error.error) return new BllError { error = true, message = new WX_KeyValue { key = "regs_reform_idc", value = error.message } };

                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity == null) throw new Exception("未查询到当前学生信息");
                if (!entity.Examed) throw new Exception(string.Format("【{0}】还未通过审核，不必退回", entity.Name));

                //可以退回，返回学生信息
                return new BllError { error = false, message = new WX_KeyValue { key = entity.ID, value = entity.Name } };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}