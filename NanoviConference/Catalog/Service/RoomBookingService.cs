// Services/RoomBookingService.cs
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NanoviConference.Catalog.Model.Customer;
using NanoviConference.Catalog.Model.RoomBooking;
using NanoviConference.Catalog.Model.Session;
using NanoviConference.Catalog.Service;
using NanoviConference.Persistence.EF;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Services
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly NcDbContext _context;
        private readonly IMapper _mapper;

        public RoomBookingService(NcDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomBookingViewDto>> GetAllBookingsAsync()
        {
            var sessions = await _context.Sessions
                .Include(s => s.Room)
                .Include(s => s.Group)
                    .ThenInclude(g => g.Customers)
                .ToListAsync();

            return sessions.Select(s => new RoomBookingViewDto
            {
                BookingId = s.SessionId,
                RoomId = s.RoomId,
                RoomName = s.Room.Name,
                Date = s.Date,
                SessionTime = s.SessionTime,
                Status = s.Status,
                GroupName = s.Group?.Name,
                GroupLocation = s.Group?.Location,
                Customers = s.Group?.Customers.Select(c => new CustomerViewDto
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Phone = c.Phone,
                    IsLeader = c.IsLeader,
                }).ToList() ?? new()
            });
        }

        public async Task<RoomBookingViewDto> GetBookingByIdAsync(int bookingId)
        {
            var session = await _context.Sessions
                .Include(s => s.Room)
                .Include(s => s.Group)
                    .ThenInclude(g => g.Customers)
                    .ThenInclude(c => c.CreatedBy)
                .Include(s => s.Group)
                    .ThenInclude(g => g.Customers)
                    .ThenInclude(c => c.Groups)
                .FirstOrDefaultAsync(s => s.SessionId == bookingId);

            if (session == null)
                throw new KeyNotFoundException($"Booking with ID {bookingId} not found");

            return _mapper.Map<RoomBookingViewDto>(session);
        }

        public async Task<RoomBookingViewDto> GetBookingByDateAndSessionAsync(DateTime date, string sessionTime, int roomId)
        {
            var session = await _context.Sessions
            .Include(s => s.Room)
            .Include(s => s.Group)
                .ThenInclude(g => g.Customers)
                .ThenInclude(c => c.Groups)
            .Include(s => s.Group)
                .ThenInclude(g => g.Customers)
                .ThenInclude(c => c.CreatedBy)
            .FirstOrDefaultAsync(s =>
                s.Date.Date == date.Date &&
                s.SessionTime == sessionTime &&
                s.RoomId == roomId);

            if (session == null)
            {
                throw new KeyNotFoundException($"Booking not found for date: {date.ToShortDateString()}, session: {sessionTime}, room: {roomId}");
            }

            var groupIds = session.Group.Customers.SelectMany(c => c.Groups.Select(g => g.GroupId)).Distinct().ToList();
            var groupSessions = await _context.Sessions
            .Where(s => groupIds.Contains(s.GroupId))
            .Select(s => new
            {
                s.GroupId,
                s.Date,
                s.SessionTime
            })
        .ToListAsync();
            return _mapper.Map<RoomBookingViewDto>(session);
        }

        public async Task<List<ConferenceRoom>> GetBookedRoomsWithDateAndSessionAsync(DateTime date, string sessionTime)
        {
            // Lấy danh sách các RoomId đã được đặt cho ngày và khung giờ cụ thể
            var bookedRoomIds = await _context.Sessions
                .Where(s => s.Date.Date == date.Date && s.SessionTime == sessionTime)
                .Select(s => s.RoomId)
                .ToListAsync();

            // Lấy thông tin chi tiết của các phòng đã đặt
            var bookedRooms = await _context.ConferenceRooms
                .Where(room => bookedRoomIds.Contains(room.RoomId))
                .ToListAsync();

            return bookedRooms;
        }

        public async Task<IEnumerable<RoomBookingViewDto>> GetBookingsForMonthAsync(int year, int month)
        {
            var sessions = await _context.Sessions
                .Include(s => s.Room)
                .Include(s => s.Group)
                    .ThenInclude(g => g.Customers)
                .Where(s => s.Date.Year == year && s.Date.Month == month)
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoomBookingViewDto>>(sessions);
        }

        public async Task UpdateBookingAsync(int bookingId, UpdateBookingRequestDto request)
        {
            var session = await _context.Sessions.FindAsync(bookingId);
            if (session == null)
                throw new Exception("Booking not found.");

            session.Status = request.Status;
            await _context.SaveChangesAsync();
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            var session = await _context.Sessions.FindAsync(bookingId);
            if (session == null)
                throw new Exception("Booking not found.");

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConferenceRoomViewDto>> GetAvailableRoomsAsync(DateTime date, string sessionTime)
        {
            var bookedRoomIds = await _context.Sessions
                .Where(s => s.Date.Date == date.Date && s.SessionTime == sessionTime)
                .Select(s => s.RoomId)
                .ToListAsync();

            var availableRooms = await _context.ConferenceRooms
                .Where(r => !bookedRoomIds.Contains(r.RoomId))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConferenceRoomViewDto>>(availableRooms);
        }

        public async Task<IEnumerable<ConferenceRoomViewDto>> GetAllRoomsAsync()
        {
            var rooms = await _context.ConferenceRooms.ToListAsync();
            return _mapper.Map<IEnumerable<ConferenceRoomViewDto>>(rooms);
        }

        public async Task<BookingSessionViewDto> CreateBookingAndSessionAsync(CreateBookingSessionRequestDto request)
        {
            int sessionId;
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var group = new Group
                {
                    Name = request.GroupName,
                    Location = request.GroupLocation
                };
                _context.Groups.Add(group);
                await _context.SaveChangesAsync();

                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var localDate = TimeZoneInfo.ConvertTimeFromUtc(request.Date.ToUniversalTime(), vietnamTimeZone);

                var session = new Session
                {
                    RoomId = request.RoomId,
                    SpeakerId = request.SpeakerId,
                    GroupId = group.GroupId,
                    Date = localDate.Date,
                    SessionTime = request.SessionTime,
                    Status = "reserved",
                    TotalCombosSold = 0,
                    TotalRevenue = 0
                };
                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                sessionId = session.SessionId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error creating session: " + ex.Message);
            }

            var result = await _context.Sessions
                .Include(s => s.Room)
                .Include(s => s.Speaker)
                .Include(s => s.Group)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId);

            return _mapper.Map<BookingSessionViewDto>(result);
        }
    }
}