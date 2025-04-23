using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        // Booking Info (gộp từ RoomBooking)
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public string SessionTime { get; set; } // morning, afternoon
        public string Status { get; set; } = "reserved"; // reserved, confirmed, cancelled

        // Gắn với người thuyết giảng & đoàn khách
        public Guid SpeakerId { get; set; }
        public int GroupId { get; set; }

        public int TotalCombosSold { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;

        // Điều hướng
        public ConferenceRoom Room { get; set; }
        public AppUser Speaker { get; set; }
        public Group Group { get; set; }
    }
}
