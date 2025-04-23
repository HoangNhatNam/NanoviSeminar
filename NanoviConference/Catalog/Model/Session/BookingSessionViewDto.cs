namespace NanoviConference.Catalog.Model.Session
{
    public class BookingSessionViewDto
    {
        public int BookingId { get; set; }
        public int SessionId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public Guid SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupLocation { get; set; }
        public DateTime Date { get; set; }
        public string SessionTime { get; set; }
        public string Status { get; set; }
    }
}
