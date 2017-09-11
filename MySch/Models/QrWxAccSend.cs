namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QrWxAccSend")]
    public partial class QrWxAccSend
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
        public string WxAccIDS { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string WxActionIDS { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreateTime { get; set; }

        [Key]
        [Column(Order = 5)]
        public string SendMsg { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(20)]
        public string MsgType { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool Showed { get; set; }

        [StringLength(10)]
        public string AccName { get; set; }

        [StringLength(200)]
        public string AccImage { get; set; }

        [StringLength(20)]
        public string ActionName { get; set; }
    }
}
