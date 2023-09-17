namespace Entities.Models
{
    public class CartLine{
        public int CartLineId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
        public DateTime date { get; set; }
        public CartLine()
        {
            date = DateTime.Now;
        }
        
    }
}