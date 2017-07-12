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
        public static object StudRexamineByScan(string idc, string rexamuid)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity == null) throw new Exception("无法识别的扫码信息");
                if (!entity.Examed) throw new Exception(string.Format("【{0}】还未通过初审，不能进行材料复核", entity.Name));
                if (!string.IsNullOrEmpty(entity.ExamUIDe)) throw new Exception(string.Format("【{0}】已经通过复核，无须重复操作", entity.Name));
                if (entity.ExamUID == rexamuid) throw new Exception("初审、复核不能同一人进行操作");

                var uploads = DataCRUD<WxUploadFile>.Entitys(a => a.IDS == entity.IDS).OrderBy(a => a.CreateTime);
                var urls = from upload in uploads
                           select new WX_Key
                           {
                               key = upload.ID,
                               value = upload.UploadType,
                           };

                return new WX_Key
                {
                    key = new WX_Key { key = entity.Name, value = entity.ID },
                    value = new WX_Key { key = entity.SchChoose, value = urls },
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object StudRexamineByIdc(string idc, string rexamuid)
        {
            try
            {
                var error = IDC.IDS(idc);
                if (error.error) return new BllError { error = true, message = new WX_Key { key = "regs_rexam_idc", value = error.message } };

                return StudRexamineByScan(idc, rexamuid);
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
                //IDC.Check(idc);
                //身份证查询
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity == null) throw new Exception("未知的扫码信息");
                if (!string.IsNullOrEmpty(entity.RegUID)) throw new Exception(string.Format("【{0}】已经被绑定微信号，不能再绑！", entity.Name));

                //绑定检测
                var uid = DataCRUD<Student>.Entity(a => a.RegUID == reguid);
                if (uid != null) throw new Exception(string.Format("你已经绑定【{0}】，不能再绑！", uid.Name));

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
                var entity = DataCRUD<Student>.Entity(a => a.IDC == id);
                if (entity == null) throw new Exception("未知的扫码信息");

                //检测是否审核
                if (entity.Examed) throw new Exception(string.Format("【{0}】已经通过审核，不能解除绑定", entity.Name));

                //删除，完成
                DataCRUD<Student>.Delete(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}