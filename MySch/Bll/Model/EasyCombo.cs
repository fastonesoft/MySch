using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Model
{
    public class EasyCombo
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }

        public static IEnumerable<EasyCombo> ToCombo<Entity>(IEnumerable<Entity> entitys, string selectedValue)
        {
            var combos = new List<EasyCombo>();
            foreach (var entity in entitys)
            {
                //反射
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();

                var entity_ids = entity_props.FirstOrDefault(a => a.Name == "IDS");
                var entity_ids_value = entity_ids.GetValue(entity);

                var entity_name = entity_props.FirstOrDefault(a => a.Name == "Name");
                var entity_name_value = entity_name.GetValue(entity);

                //转换
                var combo = new EasyCombo
                {
                    id = entity_ids_value.ToString(),
                    text = entity_name_value.ToString(),
                    selected = entity_ids_value.ToString() == selectedValue
                };
                //
                combos.Add(combo);
            }
            //以id排序
            combos.OrderBy(a => a.id);
            return combos;
        }

        public static string ToComboJsons<Entity>(IEnumerable<Entity> entitys, string selectedValue)
        {
            var combos = ToCombo<Entity>(entitys, selectedValue);
            return Jsons.ToJsons(combos);
        }
    }
}