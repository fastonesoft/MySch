﻿using MySch.Dal;
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

                var res = new WX_KeyValue { key = entity.Name, value = entity.IDC };
                var count = DataCRUD<Student>.Entity(a => a.Examed && a.ExamUID == examuid && !string.IsNullOrEmpty(a.ExamUIDe));
                res.Add("通过过审核人数", count);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object PassStuds(string examuid)
        {
            try
            {
                var entitys = DataCRUD<Student>.Entitys(a => a.Examed && a.ExamUID == examuid);
                var keys = from entity in entitys
                           select new WX_KeyValue
                           {
                               key = entity.Name,
                               value = entity.IDC
                           };
                return keys;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static object NotPassStuds()
        {
            try
            {
                var entitys = DataCRUD<Student>.Entitys(a => a.Examed == false);
                var keys = from entity in entitys
                           select new WX_KeyValue
                           {
                               key = entity.Name,
                               value = entity.RegNo
                           };
                return keys;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Rexamine(string id, string examuide)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("未查询到当前学生信息");

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