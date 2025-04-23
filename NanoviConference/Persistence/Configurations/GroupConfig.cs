using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.Configurations
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasKey(g => g.GroupId);
            builder.Property(g => g.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(g => g.Location)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.HasMany(g => g.Customers)
            .WithMany(c => c.Groups)
            .UsingEntity<CustomerGroup>(
                j => j
                    .HasOne(cg => cg.Customer)
                    .WithMany(c => c.CustomerGroups)
                    .HasForeignKey(cg => cg.CustomerId),
                j => j
                    .HasOne(cg => cg.Group)
                    .WithMany(g => g.CustomerGroups)
                    .HasForeignKey(cg => cg.GroupId),
                j =>
                {
                    j.ToTable("CustomerGroups"); 
                    j.HasKey(cg => cg.CustomerGroupId);
                });
        }
    }
}
