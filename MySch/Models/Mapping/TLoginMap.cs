using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class TLoginMap : EntityTypeConfiguration<TLogin>
    {
        public TLoginMap()
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

            this.Property(t => t.IP)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Pwd)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Brower)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.LoginMsg)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("TLogin");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IDS).HasColumnName("IDS");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Pwd).HasColumnName("Pwd");
            this.Property(t => t.Brower).HasColumnName("Brower");
            this.Property(t => t.LoginMsg).HasColumnName("LoginMsg");
            this.Property(t => t.LoginTime).HasColumnName("LoginTime");
        }
    }
}
