using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TBanMap : EntityTypeConfiguration<TBan>
    {
        public TBanMap()
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

            this.Property(t => t.GradeIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.MasterIDS)
                .HasMaxLength(20);

            this.Property(t => t.GroupIDS)
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TBan");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.MasterIDS).HasColumnName("MasterIDS");
            this.Property(t => t.GroupIDS).HasColumnName("GroupIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
        }
    }
}
