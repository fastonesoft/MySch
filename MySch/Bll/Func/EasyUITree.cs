using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.Func
{
    public class EasyUITree
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string memo { get; set; }

        public static IEnumerable<EasyUITree> ToTree(IEnumerable<object> entitys, string ids, string name, string state, string memo)
        {
            var trees = new List<EasyUITree>();
            foreach (var entity in entitys)
            {
                //反射
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();

                var entity_ids = entity_props.FirstOrDefault(a => a.Name == ids);
                var entity_ids_value = entity_ids.GetValue(entity);

                var entity_name = entity_props.FirstOrDefault(a => a.Name == name);
                var entity_name_value = entity_name.GetValue(entity);

                //转换
                var tree = new EasyUITree
                {
                    id = entity_ids_value.ToString(),
                    text = entity_name_value.ToString(),
                    state = state,
                    memo = memo,
                };
                //
                trees.Add(tree);
            }
            //以id排序
            return trees.OrderBy(a => a.id);
        }

        public static string ToTreeJsons<Entity>(IEnumerable<Entity> entitys, string ids, string name, string state, string memo) where Entity : class
        {
            var trees = ToTree(entitys, ids, name, state, memo);
            return Jsons.ToJsons(trees);
        }
    }
}