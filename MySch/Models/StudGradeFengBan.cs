namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudGradeFengBan")]
    public partial class StudGradeFengBan
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(32)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string BanIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string OwnerAccIDS { get; set; }
    }
}
