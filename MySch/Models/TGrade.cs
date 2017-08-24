namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TGrade")]
    public partial class TGrade
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string StepIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string YearIDS { get; set; }

        [Required]
        [StringLength(20)]
        public string EduIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }

        public bool CanFeng { get; set; }
    }
}
