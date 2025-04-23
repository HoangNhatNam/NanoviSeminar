namespace NanoviConference.Catalog.Model.Customer
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int GroupId { get; set; }
        public bool IsLeader { get; set; } = false;
        public string? Address { get; set; }
    }
}