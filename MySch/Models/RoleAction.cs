namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoleAction")]
    public partial class RoleAction
    {
        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(100)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public bool IsMenu { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleTypeIDS { get; set; }

        public virtual RoleType RoleType { get; set; }
    }
}
