using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAI.Domain.Entities;
using SAI.Domain.ValueObjects;

namespace SAI.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(v => v.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.OwnsOne(u => u.PhoneNumber, p =>
            {
                p.Property(v => v.Value)
                    .HasColumnName("PhoneNumber")
                    .IsRequired()
                    .HasMaxLength(20);
            });

            builder.OwnsOne(u => u.TelegramNickName, t =>
            {
                t.Property(v => v.Value)
                    .HasColumnName("TelegramNickName")
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Navigation(u => u.Addresses).HasField("_addresses");
            builder.Navigation(u => u.Organizations).HasField("_organizations");
            builder.Navigation(u => u.Notes).HasField("_notes");
        }
    }
}
