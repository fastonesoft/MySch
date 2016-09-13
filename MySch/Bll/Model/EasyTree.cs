using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Model
{
    public class EasyTree
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }

        public static IEnumerable<EasyTree> ToTree<Entity>(IEnumerable<Entity> entitys)
        {
            var trees = new List<EasyTree>();
            foreach (var entity in entitys)
            {
                //反射
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();

                var entity_ids = entity_props.FirstOrDefault(a => a.Name == "IDS");
                var entity_ids_value = entity_ids.GetValue(entity);

                var entity_name = entity_props.FirstOrDefault(a => a.Name == "TreeName");
                var entity_name_value = entity_name.GetValue(entity);

                //转换
                var tree = new EasyTree
                {
                    id = entity_ids_value.ToString(),
                    text = entity_name_value.ToString(),
                    state = "closed",
                };
                //
                trees.Add(tree);
            }
            //以id排序
            trees.OrderBy(a => a.id);
            return trees;
        }

        public static string ToTreeJsons<Entity>(IEnumerable<Entity> entitys)
        {
            var trees = ToTree<Entity>(entitys);
            return Jsons.ToJsons(trees);
        }

    }
}