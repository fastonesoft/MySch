using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TGradeStudLogMap : EntityTypeConfiguration<TGradeStudLog>
    {
        public TGradeStudLogMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID, t.IDS, t.GradeIDS });

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.GradeIDS)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TGradeStudLog");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.GradeIDS).HasColumnName("GradeIDS");
        }
    }
}
