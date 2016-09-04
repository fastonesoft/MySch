using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QPartStepMap : EntityTypeConfiguration<QPartStep>
    {
        public QPartStepMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.PartIDS, t.StepIDS, t.AccIDS, t.Graduated });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PartIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.StepIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .HasMaxLength(25);

            this.Property(t => t.PartName)
                .HasMaxLength(10);

            this.Property(t => t.StepName)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("QPartStep");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.PartIDS).HasColumnName("PartIDS");
            this.Property(t => t.StepIDS).HasColumnName("StepIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PartName).HasColumnName("PartName");
            this.Property(t => t.StepName).HasColumnName("StepName");
            this.Property(t => t.Graduated).HasColumnName("Graduated");
        }
    }
}
