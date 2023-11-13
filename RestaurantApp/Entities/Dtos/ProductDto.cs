namespace Entities.Dtos
{
    public record ProductDto{
        public int ProductId { get; init; }
        public String? ProductName { get; init; }
        public decimal Price { get; init; }
        public bool ShowCase { get; set; }
        public String? Summary { get; init; } = String.Empty;
        public String? ImageUrl { get; set; }
        public int CategoryId { get; init; }
    }
}