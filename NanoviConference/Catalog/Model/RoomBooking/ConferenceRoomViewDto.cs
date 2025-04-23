namespace NanoviConference.Catalog.Model.RoomBooking
{
    public class ConferenceRoomViewDto
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } // available, occupied
    }
}
