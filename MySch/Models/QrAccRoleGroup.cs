namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QrAccRoleGroup")]
    public partial class QrAccRoleGroup
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(32)]
        public string IDS { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleGroupIDS { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool Passed { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Fixed { get; set; }

        [StringLength(20)]
        public string RoleGroupName { get; set; }

        [StringLength(32)]
        public string ParentID { get; set; }
    }
}
