using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class ConferenceRoomConfig : IEntityTypeConfiguration<ConferenceRoom>
    {
        public void Configure(EntityTypeBuilder<ConferenceRoom> builder)
        {
            builder.ToTable("ConferenceRooms");

            builder.HasKey(x => x.RoomId);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Status).HasMaxLength(20).HasDefaultValue("available");
        }
    }
}
