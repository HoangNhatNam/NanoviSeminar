using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NanoviConference.Catalog.Model.Speaker;
using NanoviConference.Persistence.Entities;

namespace NanoviConference.Catalog.Service
{
    public class SpeakerService : ISpeakerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public SpeakerService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpeakerViewDto>> GetSpeakersAsync()
        {
            var speakers = await _userManager.GetUsersInRoleAsync("Speaker"); // Lấy tất cả user có role Speaker
            return _mapper.Map<IEnumerable<SpeakerViewDto>>(speakers);
        }
    }
}
