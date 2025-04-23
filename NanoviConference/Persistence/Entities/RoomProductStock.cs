using System.ComponentModel.DataAnnotations;

namespace NanoviConference.Persistence.Entities
{
    public class RoomProductStock
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int StockRemaining { get; set; }

        public ConferenceRoom Room { get; set; }
        public Product Product { get; set; }
    }
}
