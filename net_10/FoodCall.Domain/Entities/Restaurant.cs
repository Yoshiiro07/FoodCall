namespace FoodCall.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set;}
        public string Name {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;

        public string Phone {get; set;} = string.Empty;

        public string ImageUrl {get; set;} = string.Empty;

        public decimal DeliveryFee {get; set;}

        public int DeliveryTimeMinutes {get;set;}

        public decimal MinimumOrderValue {get;set;}

        public bool isActive {get;set;} = true;

        public bool IsOpen {get;set;} = true;

        public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

        //estrangeira
        public int OwnerId {get;set;}

        // Relacoes
        public User Owner {get;set;} = null!;
        public ICollection<MenuItem> MenuItems {get; set;} = new List<MenuItem>();
        public ICollection<Order> Orders {get; set;} = new List<Order>();

        
    }
}