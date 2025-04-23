namespace NanoviConference.Catalog.Model.Session
{
    public class SessionViewDto
    {
        public int SessionId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public Guid SpeakerId { get; set; }
        public string SpeakerName { get; set; } // Thêm nếu AppUser có Name
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupLocation { get; set; }
        public DateTime Date { get; set; }
    }
}
