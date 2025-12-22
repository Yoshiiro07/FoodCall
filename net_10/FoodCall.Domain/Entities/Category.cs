using System.Runtime.CompilerServices;

namespace FoodCAll.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon {get;set;} = string.Empty;
        public bool IsActive {get;set;} = true;

        // Relações
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}