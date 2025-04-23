using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class CustomerGroup
    {
        [Key]
        public int CustomerGroupId { get; set; } // Khóa chính của bảng trung gian

        // Khóa ngoại đến Customer
        [Required]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Khóa ngoại đến Group
        [Required]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
