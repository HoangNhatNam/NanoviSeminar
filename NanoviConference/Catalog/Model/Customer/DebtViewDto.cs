namespace NanoviConference.Catalog.Model.Customer
{
    public class DebtViewDto
    {
        public int DebtId { get; set; }
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
    }
}
