namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudGradeType")]
    public partial class StudGradeType
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
        [StringLength(32)]
        public string AccIDS { get; set; }
    }
}
