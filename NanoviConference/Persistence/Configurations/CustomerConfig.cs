using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).HasMaxLength(10);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(15).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(255);
            builder.Property(x => x.IsLeader)
                   .HasDefaultValue(false);

            builder.Property(x => x.PurchaseCount)
                   .HasDefaultValue(0);

            builder.Property(x => x.Comment)
                   .HasMaxLength(500);

            builder.HasMany(x => x.Groups)
            .WithMany(g => g.Customers)
            .UsingEntity<CustomerGroup>(
                j => j
                    .HasOne(pt => pt.Group)
                    .WithMany(t => t.CustomerGroups)
                    .HasForeignKey(pt => pt.GroupId),
                j => j
                    .HasOne(pt => pt.Customer)
                    .WithMany(p => p.CustomerGroups)
                    .HasForeignKey(pt => pt.CustomerId),
                j =>
                {
                    j.ToTable("CustomerGroups"); // Đặt tên bảng trung gian
                    j.HasKey(t => t.CustomerGroupId); // Khóa chính của bảng trung gian
                    // Thêm các cấu hình khác cho bảng trung gian nếu cần
                });

            // Mối quan hệ với Orders và Debts (nếu cần cấu hình thêm)
            builder.HasMany(x => x.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa Customer thì xóa luôn Orders

            builder.HasMany(x => x.Debts)
                   .WithOne(d => d.Customer)
                   .HasForeignKey(d => d.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa Customer thì xóa luôn Debts

            // Liên kết với AppUser (nhân viên)
            builder.HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict); // Không xóa nhân viên nếu có khách hàng liên kết
        }
    }
}
