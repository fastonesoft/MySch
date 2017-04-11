using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MySch.Models.Mapping
{
    public class AccessTokenMap : EntityTypeConfiguration<AccessToken>
    {
        public AccessTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.create_time);

            // Properties
            this.Property(t => t.access_token)
                .IsRequired()
                .HasMaxLength(900);

            // Table & Column Mappings
            this.ToTable("AccessToken");
            this.Property(t => t.create_time).HasColumnName("create_time");
            this.Property(t => t.access_token).HasColumnName("access_token");
            this.Property(t => t.expires_in).HasColumnName("expires_in");
        }
    }
}
