using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Func
{
    public class EasyUIpGrid
    {
        public string name { get; set; }
        public object value { get; set; }
        public string group { get; set; }
        public string editor { get; set; }

        public static EasyUIpGrid PItem<Entity>(Entity entity, string fieldName, string fieldTitle, string groupName, string editorName) where Entity : class
        {
            if (entity == null)
            {
                return null;
            }
            else
            {
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();
                var entity_field = entity_props.FirstOrDefault(a => a.Name == fieldName);
                var entity_value = entity_field.GetValue(entity);

                var res = new EasyUIpGrid
                {
                    name = fieldTitle,
                    value = entity_value,
                    group = groupName,
                    editor = editorName,
                };

                return res;
            }
        }

        public static object PGrid<Entity>(IEnumerable<Entity> entitys) where Entity : class
        {
            return new { total = entitys.Count(), rows = entitys };
        }
    }
}