using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class CustomerGroupConfig: IEntityTypeConfiguration<CustomerGroup>
    {
        public void Configure(EntityTypeBuilder<CustomerGroup> builder)
        {
            builder.ToTable("CustomerGroups"); // Đảm bảo tên bảng đúng

            builder.HasKey(cg => cg.CustomerGroupId); // Cấu hình khóa chính

            // Cấu hình khóa ngoại đến Customer
            builder.HasOne(cg => cg.Customer)
                .WithMany(c => c.CustomerGroups)
                .HasForeignKey(cg => cg.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Tùy chọn: cấu hình hành vi xóa

            // Cấu hình khóa ngoại đến Group
            builder.HasOne(cg => cg.Group)
                .WithMany(g => g.CustomerGroups)
                .HasForeignKey(cg => cg.GroupId)
                .OnDelete(DeleteBehavior.Cascade); // Tùy chọn: cấu hình hành vi xóa
        }
    }
}
