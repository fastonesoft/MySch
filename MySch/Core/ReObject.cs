using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Core
{
    public class ReObject
    {
        public static object Entity(object obj, string fieldName)
        {
            Type obj_type = obj.GetType();
            var obj_props = obj_type.GetProperties();
            var obj_field = obj_props.FirstOrDefault(a => a.Name == fieldName);
            return  obj_field.GetValue(obj);
        }
    }
}