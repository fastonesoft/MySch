namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WxUploadFile")]
    public partial class WxUploadFile
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(10)]
        public string FileType { get; set; }

        [Required]
        [StringLength(10)]
        public string UploadType { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
