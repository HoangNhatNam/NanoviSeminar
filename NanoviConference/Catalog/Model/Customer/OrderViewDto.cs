namespace NanoviConference.Catalog.Model.Customer
{
    public class OrderViewDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int ComboId { get; set; }
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount => Quantity * ComboPrice;
        public decimal ComboPrice { get; set; } // Giả sử lấy từ Combo
    }
}
