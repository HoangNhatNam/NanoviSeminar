using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class DebtConfig : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.ToTable("Debts");

            builder.HasKey(d => d.DebtId);

            builder.Property(d => d.Address)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(d => d.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Cấu hình quan hệ với Order (Không tự động xóa Debt khi Order bị xóa)
            builder.HasOne(d => d.Order)
                   .WithMany(o => o.Debts)
                   .HasForeignKey(d => d.OrderId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình quan hệ với Customer (Xóa Debt nếu Customer bị xóa)
            builder.HasOne(d => d.Customer)
                   .WithMany(c => c.Debts)
                   .HasForeignKey(d => d.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
