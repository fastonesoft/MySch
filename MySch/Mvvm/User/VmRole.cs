using MySch.Bll;
using MySch.Dal;
using MySch.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Mvvm.User
{
    public class VmRole
    {
        public static int GetRoleGroupIDS(string unionid)
        {
            try
            {
                var entity = DataCRUD<TAcc>.Entity(a => a.ID == unionid);
                if (entity == null) throw new Exception("表示层：用户帐号未注册！");

                return entity.RoleGroupIDS;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class VmRoleGroup : BllEntity<ARoleGroup>
    {
        public string ID { get; set; }
        public int IDS { get; set; }
        public string Name { get; set; }
    }
}