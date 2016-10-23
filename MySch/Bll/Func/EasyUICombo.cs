using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Func
{
    public class EasyUICombo
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }

        public static IEnumerable<EasyUICombo> ToCombo<Entity>(IEnumerable<Entity> entitys, string idName, string textName, string selectedValue)
        {
            var combos = new List<EasyUICombo>();
            foreach (var entity in entitys)
            {
                //反射
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();

                var entity_ids = entity_props.FirstOrDefault(a => a.Name == idName);
                var entity_ids_value = entity_ids.GetValue(entity);

                var entity_name = entity_props.FirstOrDefault(a => a.Name == textName);
                var entity_name_value = entity_name.GetValue(entity);

                //转换
                var combo = new EasyUICombo
                {
                    id = entity_ids_value.ToString(),
                    text = entity_name_value.ToString(),
                    selected = entity_ids_value.ToString() == selectedValue
                };
                //
                combos.Add(combo);
            }
            //以id排序
            return combos.OrderBy(a => a.id);
        }

        public static IEnumerable<EasyUICombo> ToCombo<Entity>(IEnumerable<Entity> entitys, string selectedValue)
        {
            return ToCombo<Entity>(entitys, "IDS", "Name", selectedValue);
        }

        public static string ToComboJsons<Entity>(IEnumerable<Entity> entitys, string idName, string textName, string selectedValue)
        {
            var combos = ToCombo<Entity>(entitys,idName, textName, selectedValue);
            return Jsons.ToJsons(combos);
        }

        public static string ToComboJsons<Entity>(IEnumerable<Entity> entitys, string selectedValue)
        {
            return ToComboJsons<Entity>(entitys, "IDS", "Name", selectedValue);
        }

    }
}