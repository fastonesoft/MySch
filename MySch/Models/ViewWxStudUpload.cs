namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViewWxStudUpload")]
    public partial class ViewWxStudUpload
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
        public string FileType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string UploadType { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreateTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string IDC { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(2)]
        public string StudSex { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(32)]
        public string GradeIDS { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(20)]
        public string BanIDS { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool InSch { get; set; }

        public int? Score { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(32)]
        public string StudGradeID { get; set; }
    }
}
