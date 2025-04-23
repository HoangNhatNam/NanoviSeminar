namespace NanoviConference.Catalog.Model.Customer
{
    public class CustomerUpdateDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int GroupId { get; set; }
        public bool IsLeader { get; set; }
        public string? Address { get; set; }
        public int SeatRow { get; set; }
        public int SeatLine { get; set; }
    }
}
