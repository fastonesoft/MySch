namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminDatum")]
    public partial class AdminDatum
    {
        [Required]
        [StringLength(32)]
        public string ID { get; set; }

        [Key]
        [StringLength(20)]
        public string IDS { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public string Command { get; set; }
    }
}
