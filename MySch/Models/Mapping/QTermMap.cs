using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class QTermMap : EntityTypeConfiguration<QTerm>
    {
        public QTermMap()
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

            this.Property(t => t.YearIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SemesterIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AccIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .HasMaxLength(37);

            // Table & Column Mappings
            this.ToTable("QTerm");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.IsCurrent).HasColumnName("IsCurrent");
            this.Property(t => t.YearIDS).HasColumnName("YearIDS");
            this.Property(t => t.SemesterIDS).HasColumnName("SemesterIDS");
            this.Property(t => t.AccIDS).HasColumnName("AccIDS");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}