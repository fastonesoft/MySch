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
                var entity = DataCRUD<Student>.First(a => a.Examed && !string.IsNullOrEmpty(a.ExamUIDe) && !a.Fixed);
                if (entity == null) throw new Exception("房产数据已整理完毕，辛苦了");

                //不让再次搜到
                entity.Fixed = true;
                DataCRUD<Student>.Update(entity);

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
                var entity = DataCRUD<Student>.Entity(a => a.ID == id);
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

        public static int FixStud(VmStudEx stud)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.ID == stud.ID);
                if (entity == null) throw new Exception("错误的提交数据");

                entity.Name1 = stud.Name1;
                entity.Home = stud.Home;
                entity.Birth = stud.Birth;

                DataCRUD<Student>.Update(entity);

                return DataCRUD<Student>.Count(a => a.Examed && !string.IsNullOrEmpty(a.ExamUIDe) && !a.Fixed);
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
                return DataCRUD<Student>.Count(a => a.Examed && !string.IsNullOrEmpty(a.ExamUIDe) && !a.Fixed);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}