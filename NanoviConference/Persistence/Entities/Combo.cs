using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class Combo
    {
        [Key]
        public int ComboId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Type { get; set; } // Ví dụ: Main, Extra, Exchange
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>(); // Quan hệ nhiều-nhiều với Product
    }
}
