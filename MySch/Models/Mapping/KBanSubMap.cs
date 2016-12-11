using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KBanSubMap : EntityTypeConfiguration<KBanSub>
    {
        public KBanSubMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.BanIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SubIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("KBanSub");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.BanIDS).HasColumnName("BanIDS");
            this.Property(t => t.SubIDS).HasColumnName("SubIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
        }
    }
}
