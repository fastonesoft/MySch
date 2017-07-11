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
        public static object Examine(string id, bool choose, string examuid)
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

                //返回数据
                return ExamedStuds(examuid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //通过查询，未通过统计
        public static object ExamedStuds(string examuid)
        {
            try
            {
                //未审核的人数
                var count = DataCRUD<Student>.Count(a => a.Examed == false);

                //获取初审人员列表
                var entitys = DataCRUD<Student>.Entitys(a => a.Examed && a.ExamUID == examuid && string.IsNullOrEmpty(a.ExamUIDe));
                var keys = from entity in entitys
                           select new WX_Key
                           {
                               key = entity.Name,
                               value = entity.IDC
                           };
                return new WX_Key { key = count, value = keys };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int RexamedStuds(string rexamuid)
        {
            try
            {
                return DataCRUD<Student>.Count(a => a.Examed && a.ExamUIDe == rexamuid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object Rexamine(string id, string examuide)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("未查询到当前学生信息");
                if (!entity.Examed) throw new Exception("未通过初审，不能进行复核");
                if (!string.IsNullOrEmpty(entity.ExamUIDe)) throw new Exception("已经通过复核，无须重复操作");
                if (entity.ExamUID == examuide) throw new Exception("初审、复核不能同一人进行操作");

                var max = DataCRUD<Student>.Max(a => a.StepIDS == "3212840201201701", a => a.RegNo);

                int kao = string.IsNullOrEmpty(max) ? 1 : int.Parse(max.Substring(0, 2));
                int seat = string.IsNullOrEmpty(max) ? 0 : int.Parse(max.Substring(2, 2));

                int num = 36;

                kao = seat >= num ? kao + 1 : kao;
                seat++;
                seat = seat == num + 1 ? 1 : seat;

                //审核状态提交
                entity.ExamUIDe = examuide;
                entity.RegNo = entity.RegNo == null ? kao.ToString().PadLeft(2, '0') + seat.ToString().PadLeft(2, '0') : entity.RegNo;
                //
                DataCRUD<Student>.Update(entity);

                return new WX_Key { key = entity.Name, value = entity.RegNo };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Roll(string id)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("未查询到当前学生信息");
                if (!entity.Examed) throw new Exception("未通过初审，不需要退回重审");

                entity.ExamUIDe = null;
                entity.Examed = false;
                //
                DataCRUD<Student>.Update(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}