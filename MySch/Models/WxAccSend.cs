namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WxAccSend")]
    public partial class WxAccSend
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

        public DateTime CreateTime { get; set; }

        [Required]
        public string SendMsg { get; set; }

        [Required]
        [StringLength(20)]
        public string MsgType { get; set; }

        public bool Showed { get; set; }

        public virtual WxAcc WxAcc { get; set; }

        public virtual WxAction WxAction { get; set; }
    }
}
