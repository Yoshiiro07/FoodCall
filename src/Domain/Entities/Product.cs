namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }   
    }
}