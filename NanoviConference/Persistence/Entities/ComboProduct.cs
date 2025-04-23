namespace NanoviConference.Persistence.Entities
{
    public class ComboProduct
    {
        public int ComboId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Combo Combo { get; set; }
        public Product Product { get; set; }
    }
}
