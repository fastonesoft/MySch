namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSubGrade")]
    public partial class KSubGrade
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string GradeIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string SubIDS { get; set; }

        public int DefaultValue { get; set; }

        public bool DefaultScoring { get; set; }
    }
}
