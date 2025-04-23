using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using NanoviConference.Catalog.Model.Customer;
using NanoviConference.Catalog.Service;
using NanoviConference.Persistence.Entities;
using System;
using NanoviConference.Persistence.EF;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MilkConference.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NcDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerService(NcDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private Guid GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("HttpContext is null.");
            }

            var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new Exception("Authorization header is missing or invalid.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    throw new Exception("User ID claim not found in JWT token.");
                }

                if (Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return userId;
                }
                else
                {
                    throw new Exception("Invalid user ID format in JWT token.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading JWT token: " + ex.Message);
            }
        }

        public async Task<IEnumerable<CustomerViewDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.CreatedBy)
                .Include(c => c.CustomerGroups) // Include bảng trung gian
                    .ThenInclude(cg => cg.Group) // Load thông tin Group qua bảng trung gian
                .OrderBy(c => c.CreatedByUserId) // Sắp xếp theo nhân viên
                .ToListAsync();

            // Map sang DTO. Mapper có thể cần điều chỉnh để xử lý mối quan hệ mới.
            return _mapper.Map<IEnumerable<CustomerViewDto>>(customers);
        }

        public async Task<CustomerViewDto> GetCustomerByIdAsync(string customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.CreatedBy)
                .Include(c => c.CustomerGroups)
                    .ThenInclude(cg => cg.Group)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            return _mapper.Map<CustomerViewDto>(customer);
        }

        public async Task<IEnumerable<CustomerViewDto>> SearchCustomersAsync(string searchTerm)
        {
            var customers = await _context.Customers
                .Include(c => c.CreatedBy)
                .Include(c => c.CustomerGroups)
                    .ThenInclude(cg => cg.Group)
                .Where(c => c.Name.Contains(searchTerm) ||
                            c.Phone.Contains(searchTerm) ||
                            (c.Address != null && c.Address.Contains(searchTerm)))
                .OrderBy(c => c.CreatedByUserId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerViewDto>>(customers);
        }

        public async Task<IEnumerable<CustomerViewDto>> GetCustomersByGroupAsync(int groupId)
        {
            var customers = await _context.Customers
                .Include(c => c.CreatedBy)
                .Include(c => c.CustomerGroups)
                    .ThenInclude(cg => cg.Group)
                .Where(c => c.CustomerGroups.Any(cg => cg.GroupId == groupId))
                .OrderBy(c => c.CreatedByUserId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerViewDto>>(customers);
        }

        public async Task<Customer> UpdateCustomerAsync(string customerId, CustomerUpdateDto customerDto)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            // Map các thuộc tính từ CustomerUpdateDto sang Customer một cách thủ công
            customer.Name = customerDto.Name;
            customer.Phone = customerDto.Phone;
            customer.IsLeader = customerDto.IsLeader;
            customer.Address = customer.Address;

            await _context.SaveChangesAsync();

            var updatedCustomer = await _context.Customers.FindAsync(customerId);
            return updatedCustomer;
        }

        public async Task DeleteCustomerAsync(string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderViewDto>> GetOrderHistoryAsync(string customerId)
        {
            if (!await CustomerExistsAsync(customerId))
                throw new KeyNotFoundException("Customer not found");

            var orders = await _context.Orders
                .Include(o => o.Combo)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderViewDto>>(orders);
        }

        public async Task<IEnumerable<DebtViewDto>> GetDebtsByCustomerAsync(string customerId)
        {
            if (!await CustomerExistsAsync(customerId))
                throw new KeyNotFoundException("Customer not found");

            var debts = await _context.Debts
                .Where(d => d.CustomerId == customerId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DebtViewDto>>(debts);
        }

        public async Task<bool> CustomerExistsAsync(string customerId)
        {
            return await _context.Customers.AnyAsync(c => c.CustomerId == customerId);
        }

        public async Task IncrementPurchaseCountAsync(string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            customer.PurchaseCount++;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(string customerId, string address)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            customer.Address = address;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerViewDto>> GetCustomersByUserAsync(Guid userId)
        {
            var customers = await _context.Customers
                .Include(c => c.CreatedBy)
                .Include(c => c.CustomerGroups)
                    .ThenInclude(cg => cg.Group)
                .Where(c => c.CreatedByUserId == userId)
                .OrderBy(c => c.CustomerId) // Sắp xếp theo CustomerId trong cùng nhân viên
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerViewDto>>(customers);
        }

        public async Task<IEnumerable<CustomerViewDto>> GetCustomersByDateAndSessionAsync(DateTime date, string sessionTime)
        {
            var sessions = await _context.Sessions
                .Where(s => s.Date.Date == date.Date && s.SessionTime == sessionTime)
                .Include(s => s.Group)
                .ThenInclude(g => g.Customers)
                .ThenInclude(c => c.CreatedBy)
                .ToListAsync();

            if (!sessions.Any())
            {
                return new List<CustomerViewDto>();
            }

            var customers = sessions
                .SelectMany(s => s.Group.Customers)
                .DistinctBy(c => c.CustomerId)
                .ToList();

            return _mapper.Map<IEnumerable<CustomerViewDto>>(customers);
        }

        public async Task<CustomerViewDto> CreateCustomerByDateAndSessionAsync(DateTime date, string sessionTime, CustomerCreateDto customerDto)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Date.Date == date.Date && s.SessionTime == sessionTime);

            if (session == null)
            {
                throw new Exception($"Không tìm thấy Session cho ngày {date:yyyy-MM-dd} và buổi {sessionTime}");
            }

            // Kiểm tra xem khách hàng đã tồn tại trong bảng Customers chưa
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Name == customerDto.Name && c.Phone == customerDto.Phone);

            Customer customer;
            if (existingCustomer == null)
            {
                // Nếu khách hàng chưa tồn tại, tạo mới
                customer = _mapper.Map<Customer>(customerDto);
                customer.CustomerId = await _context.GenerateCustomerIdAsync();
                customer.CreatedByUserId = GetCurrentUserId();
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                customer = existingCustomer;
            }

            // Thêm liên kết khách hàng với group của phiên hiện tại vào bảng CustomerGroup
            // Giả sử bạn muốn liên kết với GroupId của Session hiện tại
            _context.CustomerGroups.Add(new CustomerGroup { CustomerId = customer.CustomerId, GroupId = session.GroupId });
            await _context.SaveChangesAsync();

            var createdOrExistingCustomer = await _context.Customers
                .Include(c => c.CreatedBy)
                .Include(c => c.CustomerGroups)
                    .ThenInclude(cg => cg.Group)
                .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);

            if (createdOrExistingCustomer == null)
            {
                throw new Exception("Không thể tìm thấy khách hàng sau khi tạo hoặc kiểm tra.");
            }

            return _mapper.Map<CustomerViewDto>(createdOrExistingCustomer);
        }
    }
}