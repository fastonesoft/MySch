namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WxAcc")]
    public partial class WxAcc
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Mobil { get; set; }

        [StringLength(10)]
        public string Mobils { get; set; }

        [Required]
        [StringLength(32)]
        public string openid { get; set; }

        [Required]
        [StringLength(32)]
        public string nickname { get; set; }

        [Required]
        [StringLength(200)]
        public string headimgurl { get; set; }
    }
}
