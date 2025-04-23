using NanoviConference.Catalog.Model.Group;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Catalog.Model.Customer
{
    public class CustomerViewDto
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<GroupViewDto> Groups { get; set; }
        public bool IsLeader { get; set; }
        public int PurchaseCount { get; set; }
        public string? Address { get; set; }
        public Guid CreatedByUserId { get; set; } // ID của nhân viên tạo
        public string CreatedByUserName { get; set; } // Tên nhân viên tạo
    }
}
