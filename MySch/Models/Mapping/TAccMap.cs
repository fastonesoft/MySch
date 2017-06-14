using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TAccMap : EntityTypeConfiguration<TAcc>
    {
        public TAccMap()
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

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Valided)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.ParentID)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TAcc");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.AccTypeIDS).HasColumnName("AccTypeIDS");
            this.Property(t => t.RegTime).HasColumnName("RegTime");
            this.Property(t => t.Passed).HasColumnName("Passed");
            this.Property(t => t.Fixed).HasColumnName("Fixed");
            this.Property(t => t.Valided).HasColumnName("Valided");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
        }
    }
}
