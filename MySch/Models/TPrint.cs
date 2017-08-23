namespace MySch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TPrint")]
    public partial class TPrint
    {
        [Key]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string X { get; set; }

        [Required]
        [StringLength(10)]
        public string Y { get; set; }
    }
}
