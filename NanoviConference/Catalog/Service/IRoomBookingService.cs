using NanoviConference.Catalog.Model.RoomBooking;
using NanoviConference.Catalog.Model.Session;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Catalog.Service
{
    public interface IRoomBookingService
    {
        Task<IEnumerable<RoomBookingViewDto>> GetAllBookingsAsync();
        Task<RoomBookingViewDto> GetBookingByIdAsync(int bookingId);
        Task<RoomBookingViewDto> GetBookingByDateAndSessionAsync(DateTime date, string sessionTime, int roomId);
        Task<List<ConferenceRoom>> GetBookedRoomsWithDateAndSessionAsync(DateTime date, string sessionTime);
        Task<IEnumerable<RoomBookingViewDto>> GetBookingsForMonthAsync(int year, int month);
        Task UpdateBookingAsync(int bookingId, UpdateBookingRequestDto request);
        Task CancelBookingAsync(int bookingId);
        Task<IEnumerable<ConferenceRoomViewDto>> GetAvailableRoomsAsync(DateTime date, string sessionTime);
        Task<IEnumerable<ConferenceRoomViewDto>> GetAllRoomsAsync();
        Task<BookingSessionViewDto> CreateBookingAndSessionAsync(CreateBookingSessionRequestDto request); // Gộp lại
    }
}
