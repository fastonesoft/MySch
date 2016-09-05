using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll.Entity
{
    public class BllTerm : BllEntity<TTerm>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("当前学期")]
        public bool IsCurrent { get; set; }

        [DisplayName("年度编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "{0}：用12位数字设置；")]
        public string YearIDS { get; set; }

        [DisplayName("学期编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string SemesterIDS { get; set; }

        public string AccIDS { get; set; }

        public static void UnSelectCurrent()
        {
            try
            {
                var years = DataCRUD<TTerm>.Expression(a => a.IsCurrent);
                foreach (var year in years)
                {
                    year.IsCurrent = false;
                    DataCRUD<TTerm>.Update(year);
                }
            }
            catch (Exception)
            {
                new Exception("业务逻辑：清除当前学期出错！");
            }
        }

    }
}