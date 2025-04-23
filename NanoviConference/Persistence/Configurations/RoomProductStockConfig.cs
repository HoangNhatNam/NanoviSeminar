using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class RoomProductStockConfig : IEntityTypeConfiguration<RoomProductStock>
    {
        public void Configure(EntityTypeBuilder<RoomProductStock> builder)
        {
            builder.ToTable("Room_Product_Stock");

            // Thiết lập khóa chính là RoomId + ProductId
            builder.HasKey(rps => new { rps.RoomId, rps.ProductId });

            // Thiết lập quan hệ với ConferenceRoom
            builder.HasOne(rps => rps.Room)
                   .WithMany()
                   .HasForeignKey(rps => rps.RoomId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập quan hệ với Product
            builder.HasOne(rps => rps.Product)
                   .WithMany()
                   .HasForeignKey(rps => rps.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
