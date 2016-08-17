using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MySch.Bll
{
    public class ApiJson<Entity>
    {
        /// <summary>
        /// JSON方式：将 对象 -> 实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Entity JsonEntity(object obj)
        {
            try
            {
                //将对象转成json
                var javas = new JavaScriptSerializer();
                string jsons = javas.Serialize(obj);

                //再序列化成所要的对象
                return javas.Deserialize<Entity>(jsons);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}