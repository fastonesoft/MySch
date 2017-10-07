using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Web.Role
{
    public class VmRoleAction
    {
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsMenu { get; set; }
        public string RoleTypeIDS { get; set; }

        public void Add()
        {
            try
            {
                //检测IDS是否存在
                //不存在，添加
                //  存在，无视
                var entity = DataCRUD<RoleAction>.Entity(a => a.IDS == IDS);
                if (entity == null)
                {
                    var res = new RoleAction
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        IDS = this.IDS,
                        Name = this.Name,
                        IsMenu = this.IsMenu,
                        RoleTypeIDS = this.RoleTypeIDS,
                    };
                    //添加
                    DataCRUD<RoleAction>.Add(res);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}