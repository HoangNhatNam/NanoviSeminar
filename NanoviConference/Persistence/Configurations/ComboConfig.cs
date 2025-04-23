using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class ComboConfig : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.ToTable("Combos");

            builder.HasKey(x => x.ComboId);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(10,2)").IsRequired();
        }
    }
}
