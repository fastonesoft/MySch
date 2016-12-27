using MySch.Bll.Func;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VData
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public dynamic Value1 { get; set; }
        public dynamic Value2 { get; set; }
        public dynamic Value3 { get; set; }
        public dynamic Value4 { get; set; }
        public dynamic Value5 { get; set; }
        public dynamic Value6 { get; set; }
        public dynamic Value7 { get; set; }
        public dynamic Value8 { get; set; }
        public dynamic Value9 { get; set; }

        public static IEnumerable<VData> GetEntitys(string id)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    //var userSuppliedAuthor = new SqlParameter("@author", "Adi");
                    var entitys = db.Database.SqlQuery<VData>("SELECT [ID],[IDS],[Name] as Value1,Value2 = [Bootup],Value3 = [Fixed],Value4 = [ParentID] FROM dbo.TPage").ToList();

                    return entitys;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}