using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TKeyMap : EntityTypeConfiguration<TKey>
    {
        public TKeyMap()
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

            this.Property(t => t.KeyClassID)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TKey");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GD).HasColumnName("GD");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.KeyClassID).HasColumnName("KeyClassID");
        }
    }
}
