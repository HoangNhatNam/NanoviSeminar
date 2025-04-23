// Controllers/RoomBookingController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilkConference.Services;
using NanoviConference.Catalog.Model.RoomBooking;
using NanoviConference.Catalog.Model.Session;
using NanoviConference.Catalog.Service;
using System;
using System.Threading.Tasks;

namespace NanoviConference.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingController(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomBookingViewDto>>> GetAllBookings()
        {
            try
            {
                var bookings = await _roomBookingService.GetAllBookingsAsync();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomBookingViewDto>> GetBookingById(int id)
        {
            try
            {
                var booking = await _roomBookingService.GetBookingByIdAsync(id);
                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{date}/{sessionTime}/{roomId}")]
        public async Task<IActionResult> GetCustomersByDateAndSession(DateTime date, string sessionTime, int roomId)
        {
            try
            {
                var booking = await _roomBookingService.GetBookingByDateAndSessionAsync(date, sessionTime, roomId);
                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{date}/{sessionTime}")]
        public async Task<IActionResult> GetBookedRoomsWithDateAndSessionAsync(DateTime date, string sessionTime)
        {
            try
            {
                var rooms = await _roomBookingService.GetBookedRoomsWithDateAndSessionAsync(date,sessionTime);
                return Ok(rooms);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("month")]
        public async Task<ActionResult<IEnumerable<RoomBookingViewDto>>> GetBookingsForMonth(int year, int month)
        {
            try
            {
                var bookings = await _roomBookingService.GetBookingsForMonthAsync(year, month);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] UpdateBookingRequestDto request)
        {
            try
            {
                await _roomBookingService.UpdateBookingAsync(bookingId, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                await _roomBookingService.CancelBookingAsync(bookingId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<ConferenceRoomViewDto>>> GetAvailableRooms([FromQuery] DateTime date, [FromQuery] string sessionTime)
        {
            try
            {
                var rooms = await _roomBookingService.GetAvailableRoomsAsync(date, sessionTime);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable<ConferenceRoomViewDto>>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomBookingService.GetAllRoomsAsync();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<BookingSessionViewDto>> CreateBookingAndSession([FromBody] CreateBookingSessionRequestDto request)
        {
            try
            {
                var result = await _roomBookingService.CreateBookingAndSessionAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}