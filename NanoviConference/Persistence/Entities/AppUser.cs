using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace NanoviConference.Persistence.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }

        // Quan hệ với các bảng khác
        public ICollection<Order> Orders { get; set; } // Nhân viên thu tiền
        public ICollection<Session> Sessions { get; set; } // Người thuyết giảng
    }
}
