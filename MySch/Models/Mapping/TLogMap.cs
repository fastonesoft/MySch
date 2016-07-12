using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TLogMap : EntityTypeConfiguration<TLog>
    {
        public TLogMap()
        {
            // Primary Key
            this.HasKey(t => t.GD);

            // Properties
            this.Property(t => t.GD)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("TLog");
            this.Property(t => t.GD).HasColumnName("GD");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
