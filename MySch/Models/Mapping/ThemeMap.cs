using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class ThemeMap : EntityTypeConfiguration<Theme>
    {
        public ThemeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.GD, t.Name, t.Defaulted });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.GD)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Theme");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GD).HasColumnName("GD");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Defaulted).HasColumnName("Defaulted");
        }
    }
}
