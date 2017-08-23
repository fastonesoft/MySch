namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAcc")]
    public partial class TAcc
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int RoleGroupIDS { get; set; }

        public DateTime RegTime { get; set; }

        public bool Passed { get; set; }

        public bool Fixed { get; set; }

        [Required]
        [StringLength(32)]
        public string Valided { get; set; }

        [StringLength(32)]
        public string ParentID { get; set; }
    }
}
