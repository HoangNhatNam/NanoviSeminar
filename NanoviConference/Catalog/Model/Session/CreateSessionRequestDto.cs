namespace NanoviConference.Catalog.Model.Session
{
    public class CreateSessionRequestDto
    {
        public int RoomId { get; set; }
        public Guid SpeakerId { get; set; }
        public string GroupName { get; set; }
        public string GroupLocation { get; set; }
        public DateTime Date { get; set; }
        public string SessionTime { get; set; } // "morning" hoặc "afternoon"
    }
}
