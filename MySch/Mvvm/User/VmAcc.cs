using MySch.Bll;
using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Mvvm.User
{
    public class VmAcc
    {
        public static object DataGrid(string text, string parentid, int page, int rows)
        {
            try
            {
                int skip = (page - 1) * rows;
                var entitys = string.IsNullOrEmpty(text) ?
                    DataCRUD<QrAccRoleGroup>.Entitys(a => a.ParentID == parentid).Skip(skip).Take(rows) :
                    DataCRUD<QrAccRoleGroup>.Entitys(a => a.ParentID == parentid && a.Name.Contains(text));

                return EasyUI<QrAccRoleGroup>.DataGrids(entitys, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class VmAccDel : BllEntity<TAcc>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        [DisplayName("帐号名称")]
        public string Name { get; set; }
    }

    public class VmAccPass : BllEntity<TAcc>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Valided { get; set; }

        [DisplayName("帐号名称")]
        public string Name { get; set; }

        [DisplayName("通过审核")]
        public bool Passed { get; set; }

        public override void ToUpdate(ModelStateDictionary model)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == ID && a.IDS == IDS);
                if (entity == null) throw new Exception("表示层：编号查询有误！");
                //验证变更
                Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", ID, entity.RoleGroupIDS, Passed.ToString(), entity.Fixed.ToString()));
                base.ToUpdate(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class VmAccFix : BllEntity<TAcc>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Valided { get; set; }

        [DisplayName("帐号名称")]
        public string Name { get; set; }

        [DisplayName("帐号冻结")]
        public bool Fixed { get; set; }

        public override void ToUpdate(ModelStateDictionary model)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == ID && a.IDS == IDS);
                if (entity == null) throw new Exception("表示层：编号查询有误！");
                //验证变更
                Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", ID, entity.RoleGroupIDS, entity.Passed.ToString(), Fixed.ToString()));
                base.ToUpdate(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class VmAccRoleGroup : BllEntity<TAcc>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Valided { get; set; }

        [DisplayName("帐号名称")]
        public string Name { get; set; }

        [DisplayName("帐号身份")]
        public int RoleGroupIDS { get; set; }

        public override void ToUpdate(ModelStateDictionary model)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == ID && a.IDS == IDS);
                if (entity == null) throw new Exception("表示层：编号查询有误！");
                //验证变更
                Valided = Setting.GetMD5(string.Format("{0}##yuch88##{1}##{2}##{3}", ID, RoleGroupIDS, entity.Passed.ToString(), entity.Fixed.ToString()));
                base.ToUpdate(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    

}