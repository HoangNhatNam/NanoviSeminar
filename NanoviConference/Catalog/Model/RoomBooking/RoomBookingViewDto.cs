using NanoviConference.Catalog.Model.Customer;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Catalog.Model.RoomBooking
{
    public class RoomBookingViewDto
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; } // Tên phòng
        public DateTime Date { get; set; }
        public string SessionTime { get; set; } // morning, afternoon
        public string Status { get; set; } // reserved, confirmed, cancelled

        // Thông tin bổ sung
        public string GroupName { get; set; }
        public string GroupLocation { get; set; }
        public List<CustomerViewDto> Customers { get; set; } = new();
    }
}
