using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QPartStepMap : EntityTypeConfiguration<QPartStep>
    {
        public QPartStepMap()
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

            this.Property(t => t.PartIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.StepIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PartName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.StepName)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("QPartStep");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.PartIDS).HasColumnName("PartIDS");
            this.Property(t => t.StepIDS).HasColumnName("StepIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.PartName).HasColumnName("PartName");
            this.Property(t => t.StepName).HasColumnName("StepName");
        }
    }
}
