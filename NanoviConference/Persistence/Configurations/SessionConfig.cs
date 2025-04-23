using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class SessionConfig : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(x => x.SessionId);
            builder.Property(x => x.TotalCombosSold).HasDefaultValue(0);
            builder.Property(x => x.TotalRevenue).HasColumnType("decimal(10,2)").HasDefaultValue(0);

            builder.HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId);

            builder.HasOne(x => x.Speaker)
                .WithMany()
                .HasForeignKey(x => x.SpeakerId);

            builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId);

            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.SessionTime).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(20);
        }
    }
}
