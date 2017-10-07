using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.ViewModel
{
    public class VmStudEx
    {
        [Required]
        public string ID { get; set; }
        public string Name { get; set; }
        [Required]
        public string Name1 { get; set; }
        [Required]
        public string Home { get; set; }
        [Required]
        public string Birth { get; set; }

        public static object NotFixed()
        {
            try
            {
                var entity = DataCRUD<Stud>.First(a => a.Examed && !string.IsNullOrEmpty(a.ExamUIDe) && !a.Fixed);
                if (entity == null) throw new Exception("房产数据已整理完毕，辛苦了");

                //不让再次搜到
                entity.Fixed = true;
                DataCRUD<Stud>.Update(entity);

                //构造输出数据
                return new VmStudEx
                {
                    ID = entity.ID,
                    Name = entity.Name,
                    Name1 = entity.Name1,
                    Home = entity.Home,
                    Birth = entity.Birth,
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object UrlByID(string id)
        {
            try
            {
                var entity = DataCRUD<Stud>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("无法识别的扫码信息");

                var urls = DataCRUD<WxUploadFile>.Entitys(a => a.IDS == entity.IDS && a.UploadType == "house");
                var res = from url in urls
                          select string.Format("http://a.jysycz.cn/image/uploaded/{0}", url.ID);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object UrlByRegNo(string regno)
        {
            try
            {
                var entity = DataCRUD<Stud>.Entity(a => a.RegNo == regno && a.StepIDS == "3212840201201701");
                if (entity == null) throw new Exception("未找到当前录取编号对应的学生信息");

                var urls = DataCRUD<WxUploadFile>.Entitys(a => a.IDS == entity.IDS && (a.UploadType == "house" || a.UploadType == "card"));
                var res = from url in urls
                          select string.Format("http://a.jysycz.cn/image/uploaded/{0}", url.ID);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int FixStud(VmStudEx stud)
        {
            try
            {
                var entity = DataCRUD<Stud>.Entity(a => a.ID == stud.ID);
                if (entity == null) throw new Exception("错误的提交数据");

                entity.Name1 = stud.Name1;
                entity.Home = stud.Home;
                entity.Birth = stud.Birth;

                DataCRUD<Stud>.Update(entity);

                return DataCRUD<Stud>.Count(a => a.Examed && !string.IsNullOrEmpty(a.ExamUIDe) && !a.Fixed);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int NotFixedCount()
        {
            try
            {
                return DataCRUD<Stud>.Count(a => a.Examed && !string.IsNullOrEmpty(a.ExamUIDe) && !a.Fixed);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /////////////////////////////////////////////
        ////分班




    }
}