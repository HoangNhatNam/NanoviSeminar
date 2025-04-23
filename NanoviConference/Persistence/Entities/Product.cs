using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int InitialStock { get; set; }
        public ICollection<ComboProduct> ComboProducts { get; set; } = new List<ComboProduct>(); // Quan hệ nhiều-nhiều với Combo
    }
}
