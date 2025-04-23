using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Configurations;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence.EF
{
    public class NcDbContext(DbContextOptions<NcDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<RoomProductStock> RoomProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
            modelBuilder.ApplyConfiguration(new ComboConfig());
            modelBuilder.ApplyConfiguration(new ComboProductConfig());
            modelBuilder.ApplyConfiguration(new ConferenceRoomConfig());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new DebtConfig());
            modelBuilder.ApplyConfiguration(new GroupConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new RoomProductStockConfig());
            modelBuilder.ApplyConfiguration(new SessionConfig());
            modelBuilder.ApplyConfiguration(new CustomerGroupConfig());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);


            modelBuilder.Seed();
        }

        public async Task<string> GenerateCustomerIdAsync()
        {
            string prefix = "MK"; // Hoặc dựa vào khu vực
            int lastNumber = await Customers.CountAsync() + 1;
            return $"{prefix}{lastNumber:D6}"; // NA001, NA002, ...
        }
    }
}
