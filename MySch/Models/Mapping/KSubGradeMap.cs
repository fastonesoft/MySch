using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KSubGradeMap : EntityTypeConfiguration<KSubGrade>
    {
        public KSubGradeMap()
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

            this.Property(t => t.SubIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("KSubGrade");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
            this.Property(t => t.SubIDS).HasColumnName("SubIDS");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");
            this.Property(t => t.DefaultScoring).HasColumnName("DefaultScoring");
        }
    }
}
