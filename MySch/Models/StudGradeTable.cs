namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudGradeTable")]
    public partial class StudGradeTable
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
        [StringLength(32)]
        public string GradeIDS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string TableName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string TypeIDS { get; set; }

        [StringLength(100)]
        public string Memo { get; set; }
    }
}
