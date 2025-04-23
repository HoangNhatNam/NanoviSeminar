using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Catalog.Model.RoomBooking
{
    public class UpdateBookingRequestDto
    {
        public int BookingId { get; set; }
        public string Status { get; set; } // "reserved", "confirmed", "cancelled"
    }
}
