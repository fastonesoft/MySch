using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TPageMap : EntityTypeConfiguration<TPage>
    {
        public TPageMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

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

            this.Property(t => t.ThemeID)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TPage");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GD).HasColumnName("GD");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ThemeID).HasColumnName("ThemeID");
        }
    }
}
