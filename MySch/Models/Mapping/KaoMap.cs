using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KaoMap : EntityTypeConfiguration<Kao>
    {
        public KaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TermIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Kao");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TermIDS).HasColumnName("TermIDS");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
        }
    }
}
