namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewStud")]
    public partial class ViewStud
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
        public string IDC { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(32)]
        public string StepIDS { get; set; }

        [StringLength(64)]
        public string FromSch { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool SchChoose { get; set; }

        [StringLength(10)]
        public string RegNo { get; set; }

        [StringLength(32)]
        public string RegUID { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool Examed { get; set; }

        [StringLength(32)]
        public string ExamUID { get; set; }

        [StringLength(32)]
        public string ExamUIDe { get; set; }

        [StringLength(20)]
        public string Mobil1 { get; set; }

        [StringLength(20)]
        public string Mobil2 { get; set; }

        [StringLength(20)]
        public string Name1 { get; set; }

        [StringLength(20)]
        public string Name2 { get; set; }

        [StringLength(50)]
        public string Home { get; set; }

        [StringLength(50)]
        public string Birth { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool Fixed { get; set; }

        [StringLength(50)]
        public string Memo { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool Graduated { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string PartName { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(20)]
        public string StepName { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(32)]
        public string AccIDS { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(32)]
        public string PartIDS { get; set; }
    }
}
