namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBan")]
    public partial class TBan
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(10)]
        public string Num { get; set; }

        [Required]
        [StringLength(32)]
        public string GradeIDS { get; set; }

        [StringLength(32)]
        public string MasterIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string AccIDS { get; set; }

        public bool NotFeng { get; set; }

        public bool OnlyFixed { get; set; }

        public int ChangeNum { get; set; }

        public int Differ { get; set; }

        public bool IsAbs { get; set; }

        public bool SameSex { get; set; }

        public virtual TAcc TAcc { get; set; }
    }
}
