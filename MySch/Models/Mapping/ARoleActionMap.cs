using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class ARoleActionMap : EntityTypeConfiguration<ARoleAction>
    {
        public ARoleActionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.IDS)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RoleTypeIDS)
                .IsRequired()
                .HasMaxLength(20);


            this.Property(t => t.ActionName)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ARoleAction");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.RoleTypeIDS).HasColumnName("RoleTypeIDS");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
        }
    }
}
