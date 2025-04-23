using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class ConferenceRoom
    {
        [Key]
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } // "available" hoặc "occupied"

        public ICollection<Session> Sessions { get; set; }
    }
}
