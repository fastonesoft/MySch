using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MySch.ModelsEx
{
    public class MyType<Entity> where Entity : class
    {
        public static PropertyInfo GetPropertyInfor(Entity entity, string propertyName)
        {
            Type type = entity.GetType();
            PropertyInfo[] ps = type.GetProperties();
            return ps.FirstOrDefault(a => a.Name == propertyName);
        }

        public static object GetPropertyValue(Entity entity, string propertyName)
        {
            var p = GetPropertyInfor(entity, propertyName);
            return p == null ? null : p.GetValue(entity, null);
        }

        public static void SetPropertyValue(Entity entity, string propertyName, object propertyValue)
        {
            var p = GetPropertyInfor(entity, propertyName);
            p.SetValue(entity, propertyValue, null);
        }
    }
}



