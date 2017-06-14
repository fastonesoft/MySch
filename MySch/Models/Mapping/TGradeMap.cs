using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TGradeMap : EntityTypeConfiguration<TGrade>
    {
        public TGradeMap()
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

            this.Property(t => t.StepIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.YearIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.EduIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TGrade");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.StepIDS).HasColumnName("StepIDS");
            this.Property(t => t.YearIDS).HasColumnName("YearIDS");
            this.Property(t => t.EduIDS).HasColumnName("EduIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
        }
    }
}
