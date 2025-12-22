using System.Net.Sockets;

namespace FoodCall.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empyty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public String Phone {get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Customer;

        public DateTime CreatedAt { get;set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        //Relações
        public ICollection<Order> Orders {get; set; } = new List<Order>();
        public ICollection<Address> Addresses {get; set; } = new List<Address>();
    }

    public enum UserRole
    {
        Customer = 0,
        RestaurantAdmin = 1,
        SystemAdmin = 2
    }
}