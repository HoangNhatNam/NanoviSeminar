using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class ComboProductConfig : IEntityTypeConfiguration<ComboProduct>
    {
        public void Configure(EntityTypeBuilder<ComboProduct> builder)
        {
            builder.ToTable("Combo_Products");

            // Thiết lập khóa chính kép (Composite Key)
            builder.HasKey(cp => new { cp.ComboId, cp.ProductId });

            // Liên kết với bảng Combo
            builder.HasOne(cp => cp.Combo)
                   .WithMany(c => c.ComboProducts)
                   .HasForeignKey(cp => cp.ComboId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Liên kết với bảng Product
            builder.HasOne(cp => cp.Product)
                   .WithMany(p => p.ComboProducts)
                   .HasForeignKey(cp => cp.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
