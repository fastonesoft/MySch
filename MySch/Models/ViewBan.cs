namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewBan")]
    public partial class ViewBan
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string IDS { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string GradeIDS { get; set; }

        [StringLength(32)]
        public string MasterIDS { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool NotFeng { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool OnlyFixed { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChangeNum { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Differ { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsAbs { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool SameSex { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(32)]
        public string StepIDS { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(32)]
        public string YearIDS { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(32)]
        public string EduIDS { get; set; }

        [StringLength(20)]
        public string MasterName { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(20)]
        public string AccName { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(10)]
        public string PartName { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(20)]
        public string StepName { get; set; }
    }
}
