using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KSubTestMap : EntityTypeConfiguration<KSubTest>
    {
        public KSubTestMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.KaoIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SubIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("KSubTest");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.KaoIDS).HasColumnName("KaoIDS");
            this.Property(t => t.SubIDS).HasColumnName("SubIDS");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Scoring).HasColumnName("Scoring");
        }
    }
}
