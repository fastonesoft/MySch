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
        public static BllError StudRexamineByScan(string idc)
        {
            try
            {
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

        public static BllError StudRexamineByIdc(string idc)
        {
            try
            {
                var error = IDC.IDS(idc);
                if (error.error) return new BllError { error = true, message = new WX_KeyValue { key = "regs_reform_idc", value = error.message } };

                return StudRexamineByScan(idc);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public static string BindStudByScan(string idc, string reguid)
        {
            try
            {
                IDC.Check(idc);
                //身份证查询
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if(entity == null) throw new Exception("未知的扫码信息");
                if (!string.IsNullOrEmpty(entity.RegUID)) throw new Exception(string.Format("【{0}】已经被绑定微信号，不能再绑！",entity.Name));

                //绑定检测
                var uid = DataCRUD<Student>.Entity(a => a.RegUID == reguid);
                if (uid != null) throw new Exception(string.Format("你已经绑定【{0}】，不能再绑！",uid.Name));

                //提交绑定
                entity.RegUID = reguid;
                DataCRUD<Student>.Update(entity);
                return entity.Name;
            }
            catch (Exception e)
            {                
                throw e;
            }
        }

        public static void UnBindStud(string id)
        {
            try
            {

            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}