using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string CustomerId { get; set; } // NA001, NA002...
        public int ComboId { get; set; }
        public int Quantity { get; set; }

        public string PaymentMethod { get; set; } // cash, transfer, debt
        public Guid CashierId { get; set; } // AppUser (Người thu tiền)
        public string? Note { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now; // Thời gian mua
        public string? PaymentProof { get; set; } // Đường dẫn hình ảnh thanh toán (nếu chuyển khoản)

        [Required]
        public string Status { get; set; } = "pending"; // pending, completed

        // Navigation Properties
        public Customer Customer { get; set; }
        public Combo Combo { get; set; }
        public AppUser Cashier { get; set; }
        public ICollection<Debt> Debts { get; set; } = new List<Debt>();
    }
}
