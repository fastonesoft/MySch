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

                var max = DataCRUD<Student>.Max(a => a.StepIDS == "3212840201201701", a => a.RegNo);

                int kao = string.IsNullOrEmpty(max) ? 1 : int.Parse(max.Substring(0, 2));
                int seat = string.IsNullOrEmpty(max) ? 0 : int.Parse(max.Substring(2, 2));

                kao = seat >= 36 ? kao + 1 : kao;
                seat++;
                seat = seat == 37 ? 1 : seat;

                //审核状态提交
                entity.SchChoose = choose;
                entity.Examed = true;
                entity.ExamUID = examuid;
                entity.RegNo = kao.ToString().PadLeft(2, '0') + seat.ToString().PadLeft(2, '0');
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