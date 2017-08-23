namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KStud")]
    public partial class KStud
    {
        [StringLength(32)]
        public string ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string KaoIDS { get; set; }

        [Required]
        [StringLength(32)]
        public string StudIDS { get; set; }

        [StringLength(10)]
        public string Room { get; set; }

        [StringLength(10)]
        public string Seat { get; set; }

        [StringLength(20)]
        public string Kao { get; set; }
    }
}
