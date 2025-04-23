using NanoviConference.Catalog.Model.Customer;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Catalog.Service
{
    public interface ICustomerService
    {
        /// <summary>
        /// Lấy danh sách tất cả khách hàng.
        /// </summary>
        Task<IEnumerable<CustomerViewDto>> GetAllCustomersAsync();

        /// <summary>
        /// Lấy thông tin khách hàng theo ID.
        /// </summary>
        /// <param name="customerId">ID của khách hàng (ví dụ: NA001).</param>
        Task<CustomerViewDto> GetCustomerByIdAsync(string customerId);

        /// <summary>
        /// Tìm kiếm khách hàng theo tên, số điện thoại hoặc địa chỉ.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm (tên, số điện thoại, hoặc địa chỉ).</param>
        Task<IEnumerable<CustomerViewDto>> SearchCustomersAsync(string searchTerm);

        /// <summary>
        /// Lấy danh sách khách hàng theo nhóm (Group).
        /// </summary>
        /// <param name="groupId">ID của nhóm.</param>
        Task<IEnumerable<CustomerViewDto>> GetCustomersByGroupAsync(int groupId);

        /// <summary>
        /// Cập nhật thông tin khách hàng.
        /// </summary>
        /// <param name="customerId">ID của khách hàng cần cập nhật.</param>
        /// <param name="customer">Thông tin khách hàng mới.</param>
        Task<Customer> UpdateCustomerAsync(string customerId, CustomerUpdateDto customer);

        /// <summary>
        /// Xóa khách hàng theo ID.
        /// </summary>
        /// <param name="customerId">ID của khách hàng cần xóa.</param>
        Task DeleteCustomerAsync(string customerId);

        /// <summary>
        /// Lấy lịch sử đơn hàng của khách hàng.
        /// </summary>
        /// <param name="customerId">ID của khách hàng.</param>
        Task<IEnumerable<OrderViewDto>> GetOrderHistoryAsync(string customerId);

        /// <summary>
        /// Lấy danh sách nợ của khách hàng.
        /// </summary>
        /// <param name="customerId">ID của khách hàng.</param>
        Task<IEnumerable<DebtViewDto>> GetDebtsByCustomerAsync(string customerId);

        /// <summary>
        /// Kiểm tra xem khách hàng có tồn tại hay không.
        /// </summary>
        /// <param name="customerId">ID của khách hàng.</param>
        Task<bool> CustomerExistsAsync(string customerId);

        /// <summary>
        /// Tăng số lần mua hàng của khách hàng (PurchaseCount).
        /// </summary>
        /// <param name="customerId">ID của khách hàng.</param>
        Task IncrementPurchaseCountAsync(string customerId);

        /// <summary>
        /// Cập nhật địa chỉ của khách hàng.
        /// </summary>
        /// <param name="customerId">ID của khách hàng.</param>
        /// <param name="address">Địa chỉ mới.</param>
        Task UpdateAddressAsync(string customerId, string address);

        Task<IEnumerable<CustomerViewDto>> GetCustomersByUserAsync(Guid userId); // Lấy theo nhân viên
        /// <summary>
        /// Lấy danh sách khách hàng của tất cả các session trong ngày hôm nay.
        /// </summary>

        /// <summary>
        /// Lấy danh sách khách hàng theo ngày và buổi của session (dựa trên RoomBooking).
        /// </summary>
        /// <param name="date">Ngày của session.</param>
        /// <param name="sessionTime">Buổi của session (morning hoặc afternoon).</param>
        Task<IEnumerable<CustomerViewDto>> GetCustomersByDateAndSessionAsync(DateTime date, string sessionTime);
        /// <summary>
        /// Tạo mới khách hàng theo ngày và buổi của session.
        /// </summary>
        /// <param name="date">Ngày của session.</param>
        /// <param name="sessionTime">Buổi của session (morning hoặc afternoon).</param>
        /// <param name="customer">Thông tin khách hàng cần tạo.</param>
        Task<CustomerViewDto> CreateCustomerByDateAndSessionAsync(DateTime date, string sessionTime, CustomerCreateDto customer);
    }
}
