using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.PaymentMethod).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Status).HasMaxLength(20).HasDefaultValue("pending");
            builder.Property(x => x.Note).HasMaxLength(255);
            builder.Property(x => x.PaymentProof).HasMaxLength(255);

            // Thiết lập quan hệ
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Cashier)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CashierId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa đơn hàng nếu xóa nhân viên
        }
    }
}
