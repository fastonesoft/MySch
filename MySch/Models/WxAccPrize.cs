namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WxAccPrize")]
    public partial class WxAccPrize
    {
        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(32)]
        public string WxAccIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string WxActionIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string WxPrizeIDS { get; set; }

        public virtual WxAcc WxAcc { get; set; }

        public virtual WxAction WxAction { get; set; }

        public virtual WxPrize WxPrize { get; set; }
    }
}
