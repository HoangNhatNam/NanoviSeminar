using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Persistence
{
    public static class SeedingData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var roleCashierId = new Guid("f81914a1-fee2-45a7-b0d4-062f051e657c");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            // Seed dữ liệu Roles
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = roleId, Name = "Admin", NormalizedName = "ADMIN", Description = "Quản trị hệ thống" },
                new AppRole { Id = roleCashierId, Name = "Cashier", NormalizedName = "CASHIER", Description = "Nhân viên thu ngân" }
            );

            // Seed dữ liệu Users (Admin)
            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@milkconference.com",
                NormalizedEmail = "ADMIN@MILKCONFERENCE.COM",
                PhoneNumber = "0909999999",
                Address = "Head Office",
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                Name = "Nguyen Van A",
                SecurityStamp = Guid.NewGuid().ToString(), // Đảm bảo SecurityStamp không null
            };

            modelBuilder.Entity<AppUser>().HasData(adminUser);

            // Gán role cho Admin
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { UserId = adminId, RoleId = roleId }
            );

            // Seed dữ liệu Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Sữa Non", InitialStock = 900000 },
                new Product { ProductId = 2, Name = "Sữa Thực Dưỡng", InitialStock = 500000 },
                new Product { ProductId = 3, Name = "Sữa Tiểu Đường", InitialStock = 8000000 },
                new Product { ProductId = 4, Name = "Sữa Hạt Thuần Chay", InitialStock = 500000 },
                new Product { ProductId = 5, Name = "Hộp Thông Đỏ", InitialStock = 900000 }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { GroupId = 1, Name = "Quận 1", Location = "test" },
                new Group { GroupId = 2, Name = "Quận 2" , Location = "test"}
            );

            // Seed dữ liệu Combos
            modelBuilder.Entity<Combo>().HasData(
                new Combo { ComboId = 1, Name = "Combo Chính", Type = "Standard", Price = 1600000 },
                new Combo { ComboId = 2, Name = "Combo Phụ", Type = "Premium", Price = 500000 },
                new Combo { ComboId = 3, Name = "Đổi quà", Type = "Exchange", Price = 200000 }
            );

            // Seed dữ liệu ConferenceRooms
            modelBuilder.Entity<ConferenceRoom>().HasData(
                new ConferenceRoom { RoomId = 1, Name = "Phòng A", Status = "available" },
                new ConferenceRoom { RoomId = 2, Name = "Phòng B", Status = "available" },
                new ConferenceRoom { RoomId = 3, Name = "Phòng C", Status = "available" }
            );

            // Seeding Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = "NA001",
                    Name = "Nguyen Van A",
                    Phone = "0123456789",
                    CreatedByUserId = adminId, // Tham chiếu đến AppUser
                    IsLeader = false,
                    PurchaseCount = 0,
                    Address = "123 Duong ABC",
                },
                new Customer
                {
                    CustomerId = "NA002",
                    Name = "Tran Thi B",
                    Phone = "0987654321",
                    CreatedByUserId = adminId, // Tham chiếu đến AppUser
                    IsLeader = true,
                    PurchaseCount = 0,
                    Address = "456 Duong XYZ"
                }
            );
        }
    }
}
