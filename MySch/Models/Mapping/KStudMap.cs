using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class KStudMap : EntityTypeConfiguration<KStud>
    {
        public KStudMap()
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

            this.Property(t => t.KaoIDS)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.StudIDS)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Room)
                .HasMaxLength(10);

            this.Property(t => t.Seat)
                .HasMaxLength(10);

            this.Property(t => t.Kao)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("KStud");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.KaoIDS).HasColumnName("KaoIDS");
            this.Property(t => t.StudIDS).HasColumnName("StudIDS");
            this.Property(t => t.Room).HasColumnName("Room");
            this.Property(t => t.Seat).HasColumnName("Seat");
            this.Property(t => t.Kao).HasColumnName("Kao");
        }
    }
}
