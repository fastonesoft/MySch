namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ARoleAction")]
    public partial class ARoleAction
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public bool IsMenu { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleTypeIDS { get; set; }
    }
}
