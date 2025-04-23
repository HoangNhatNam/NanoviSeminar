using AutoMapper;
using NanoviConference.Catalog.Model.Customer;
using NanoviConference.Catalog.Model.Group;
using NanoviConference.Catalog.Model.RoomBooking;
using NanoviConference.Catalog.Model.Session;
using NanoviConference.Catalog.Model.Speaker;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewDto>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups))
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId)) // Guid
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedBy.Name));

            CreateMap<Group, GroupViewDto>();

            CreateMap<CustomerCreateDto, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.PurchaseCount, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore()); // Sẽ gán từ context

            CreateMap<CustomerUpdateDto, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.PurchaseCount, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore()); // Không cập nhật CreatedByUserId

            CreateMap<Order, OrderViewDto>()
                .ForMember(dest => dest.ComboPrice, opt => opt.MapFrom(src => src.Combo.Price));

            CreateMap<Debt, DebtViewDto>();

            

            // ConferenceRoom -> ConferenceRoomViewDto
            CreateMap<ConferenceRoom, ConferenceRoomViewDto>();

            CreateMap<Session, RoomBookingViewDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.SessionTime, opt => opt.MapFrom(src => src.SessionTime))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.GroupLocation, opt => opt.MapFrom(src => src.Group.Location))
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Group.Customers));
            CreateMap<Session, SessionViewDto>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.SpeakerName, opt => opt.MapFrom(src => src.Speaker.UserName)) // Giả sử AppUser có UserName
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.GroupLocation, opt => opt.MapFrom(src => src.Group.Location));
            CreateMap<AppUser, SpeakerViewDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<Session, BookingSessionViewDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.SessionId)) // Nếu cần BookingId, bạn có thể dùng SessionId làm BookingId
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name)) // Lấy tên phòng từ đối tượng Room
                .ForMember(dest => dest.SpeakerId, opt => opt.MapFrom(src => src.SpeakerId))
                .ForMember(dest => dest.SpeakerName, opt => opt.MapFrom(src => src.Speaker.Name)) // Lấy tên người thuyết giảng từ đối tượng Speaker
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name)) // Lấy tên nhóm từ đối tượng Group
                .ForMember(dest => dest.GroupLocation, opt => opt.MapFrom(src => src.Group.Location)) // Lấy địa điểm nhóm từ đối tượng Group
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date)) // Lấy ngày từ đối tượng Session
                .ForMember(dest => dest.SessionTime, opt => opt.MapFrom(src => src.SessionTime)) // Lấy thời gian từ đối tượng Session
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)); // Lấy trạng thái từ đối tượng Session
        }
    }
}