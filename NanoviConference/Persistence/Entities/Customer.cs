using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; } // Ví dụ: "NA001"
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public bool IsLeader { get; set; } = false;
        public Guid CreatedByUserId { get; set; }
        public int PurchaseCount { get; set; } = 0; // Số lần đã mua hàng
        public string? Comment { get; set; } // Ghi chú đặc biệt
        public ICollection<Order> Orders { get; set; } // Danh sách đơn hàng của khách
        public ICollection<Debt> Debts { get; set; } = new List<Debt>();
        public AppUser CreatedBy { get; set; } // Liên kết với nhân viên

        // Navigation property cho mối quan hệ nhiều-nhiều với Group
        public ICollection<CustomerGroup> CustomerGroups { get; set; }
        public ICollection<Group> Groups { get; set; }

    }
}
