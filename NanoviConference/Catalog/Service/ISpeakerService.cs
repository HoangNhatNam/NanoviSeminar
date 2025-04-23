using NanoviConference.Catalog.Model.Speaker;

namespace NanoviConference.Catalog.Service
{
    public interface ISpeakerService
    {
        Task<IEnumerable<SpeakerViewDto>> GetSpeakersAsync();
    }
}
