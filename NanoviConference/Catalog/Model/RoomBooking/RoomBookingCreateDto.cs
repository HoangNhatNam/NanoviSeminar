using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Catalog.Model.RoomBooking
{
    public class RoomBookingCreateDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression("morning|afternoon", ErrorMessage = "SessionTime must be 'morning' or 'afternoon'")]
        public string SessionTime { get; set; }

        [RegularExpression("reserved|confirmed|cancelled", ErrorMessage = "Status must be 'reserved', 'confirmed', or 'cancelled'")]
        public string Status { get; set; } = "reserved";
    }
}
