using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KSubBanMap : EntityTypeConfiguration<KSubBan>
    {
        public KSubBanMap()
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

            this.Property(t => t.SubGradeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("KSubBan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.BanIDS).HasColumnName("BanIDS");
            this.Property(t => t.SubGradeIDS).HasColumnName("SubGradeIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.IsMaster).HasColumnName("IsMaster");
        }
    }
}
