using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll
{
    public class EasyUI<Entity> 
    {
        /// <summary>
        /// 将实体数据输出为DataGrid格式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object DataGrid(Entity entity)
        {
            if (entity == null)
            {
                return new { total = 0, rows = new object[] { } };
            }
            else
            {
                List<object> objes = new List<object>();
                objes.Add(entity);
                return new { total = 1, rows = objes };
            }
        }

        /// <summary>
        /// 将数组输出为DataGrid格式
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static object DataGrids(IEnumerable<Entity> entities, int total)
        {
            if (entities == null)
            {
                return new { total = 0, rows = new object[] { } };
            }
            else
            {
                return new { total = total, rows = entities };
            }
        }
    }
}