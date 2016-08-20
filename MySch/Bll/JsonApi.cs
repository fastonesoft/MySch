using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MySch.Bll
{
    public class JsonApi<Entity>
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
                //这里不能用Newtonsoft.Json
                //原因：未知，暂时这样
                var javas = new JavaScriptSerializer();
                var jsons = javas.Serialize(obj);
                return javas.Deserialize<Entity>(jsons);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}