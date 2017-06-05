using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_Examine
    {
        public static void Examine(string id, bool choose, string examuid)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("未查询到当前学生信息");
                //审核状态提交
                entity.SchChoose = choose;
                entity.Examed = true;
                entity.ExamUID = examuid;
                DataCRUD<Student>.Update(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Rexamine(string id)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("未查询到当前学生信息");
                //审核状态变更
                entity.Examed = false;
                DataCRUD<Student>.Update(entity);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}