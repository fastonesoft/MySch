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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WxAcc()
        {
            WxAccPrizes = new HashSet<WxAccPrize>();
            WxAccSends = new HashSet<WxAccSend>();
        }

        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WxAccPrize> WxAccPrizes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WxAccSend> WxAccSends { get; set; }
    }
}
