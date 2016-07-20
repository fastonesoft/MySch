using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TPrintMap : EntityTypeConfiguration<TPrint>
    {
        public TPrintMap()
        {
            // Primary Key
            this.HasKey(t => t.Name);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.X)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Y)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("TPrint");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.X).HasColumnName("X");
            this.Property(t => t.Y).HasColumnName("Y");
        }
    }
}
