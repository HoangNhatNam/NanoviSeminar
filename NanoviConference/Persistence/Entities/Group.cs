using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        // Navigation property cho mối quan hệ nhiều-nhiều với Customer
        public ICollection<CustomerGroup> CustomerGroups { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
