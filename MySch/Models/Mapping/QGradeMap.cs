using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QGradeMap : EntityTypeConfiguration<QGrade>
    {
        public QGradeMap()
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

            this.Property(t => t.PartStepIDS)
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
                .HasMaxLength(20);

            this.Property(t => t.PartIDS)
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .HasMaxLength(33);

            this.Property(t => t.GradeName)
                .HasMaxLength(46);

            this.Property(t => t.PartStepName)
                .HasMaxLength(33);

            this.Property(t => t.YearName)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("QGrade");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.PartStepIDS).HasColumnName("PartStepIDS");
            this.Property(t => t.YearIDS).HasColumnName("YearIDS");
            this.Property(t => t.EduIDS).HasColumnName("EduIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.PartIDS).HasColumnName("PartIDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.GradeName).HasColumnName("GradeName");
            this.Property(t => t.PartStepName).HasColumnName("PartStepName");
            this.Property(t => t.YearName).HasColumnName("YearName");
            this.Property(t => t.Graduated).HasColumnName("Graduated");
            this.Property(t => t.IsCurrent).HasColumnName("IsCurrent");
        }
    }
}
