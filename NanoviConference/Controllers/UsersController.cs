using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoviConference.Catalog.Model.Speaker;
using NanoviConference.Catalog.Model.User;
using NanoviConference.Catalog.Service;
using NanoviConference.Common;
using NanoviConference.Exceptions;

namespace NanoviConference.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISpeakerService _speakerService;

        public UserController(IUserService userService, ISpeakerService speakerService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _speakerService = speakerService;
        }

        /// <summary>
        /// Đăng nhập và trả về JWT token
        /// </summary>
        /// <param name="request">Thông tin đăng nhập</param>
        /// <returns>JWT token nếu thành công</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var authResponse = await _userService.Authencate(request);
                if (string.IsNullOrEmpty(authResponse.Token))
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                return Ok(authResponse);
            }
            catch (NcException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request", details = ex.Message });
            }
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        /// <param name="request">Thông tin đăng ký</param>
        /// <returns>Trạng thái đăng ký</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var (succeeded, errors) = await _userService.Register(request);
                if (!succeeded)
                {
                    return BadRequest(new { message = "Registration failed", errors });
                }
                return Ok(new { message = "User registered successfully" });
            }
            catch (NcException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request", details = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        /// <returns>Danh sách người dùng</returns>
        [HttpGet]
        // [Authorize(Roles = "Admin")] // Chỉ admin được phép
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUser();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving users", details = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin người dùng theo ID
        /// </summary>
        /// <param name="id">ID của người dùng</param>
        /// <returns>Thông tin người dùng</returns>
        [HttpGet("{id}")]
        // [Authorize] // Yêu cầu đăng nhập
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetById(id);
                return Ok(user);
            }
            catch (NcException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user", details = ex.Message });
            }
        }

        /// <summary>
        /// Xóa người dùng theo ID
        /// </summary>
        /// <param name="id">ID của người dùng</param>
        /// <returns>Trạng thái xóa</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Chỉ admin được phép
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.Delete(id);
                if (!result)
                {
                    return BadRequest(new { message = "Failed to delete user" });
                }

                return NoContent();
            }
            catch (NcException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user", details = ex.Message });
            }
        }
        [HttpGet("speakers")]
        public async Task<ActionResult<IEnumerable<SpeakerViewDto>>> GetSpeakers()
        {
            try
            {
                var speakers = await _speakerService.GetSpeakersAsync();
                return Ok(speakers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}