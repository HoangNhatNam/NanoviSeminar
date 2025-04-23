using Microsoft.AspNetCore.Identity;

namespace NanoviConference.Persistence.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
