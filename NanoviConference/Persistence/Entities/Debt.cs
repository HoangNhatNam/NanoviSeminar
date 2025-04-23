using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NanoviConference.Persistence.Entities
{
    public class Debt
    {
        [Key]
        public int DebtId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } // Số tiền nợ
        [Required]
        [MaxLength(255)]
        public string Address { get; set; } // Địa chỉ người nợ

        public Order Order { get; set; }
        public Customer Customer { get; set; }

    }
}
