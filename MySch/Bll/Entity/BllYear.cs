using MySch.Dal;
using MySch.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllYear : BllEntity<TYear>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("年度名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "{0}：为4位数字！")]
        public string Name { get; set; }

        [DisplayName("当前年度")]
        public bool IsCurrent { get; set; }

        public string AccIDS { get; set; }

        public static void UnSelectCurrent()
        {
            try
            {
                var years = DataCRUD<TYear>.Entitys(a => a.CurrentYear);
                foreach (var year in years)
                {
                    year.CurrentYear = false;
                    DataCRUD<TYear>.Update(year);
                }
            }
            catch (Exception)
            {
                new Exception("业务逻辑：清除当前年度出错！");
            }
        }
    }
}