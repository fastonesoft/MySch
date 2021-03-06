namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TLog")]
    public partial class TLog
    {
        [Key]
        [StringLength(32)]
        public string GD { get; set; }

        [Required]
        [StringLength(2000)]
        public string Value { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
