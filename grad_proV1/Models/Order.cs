using System.ComponentModel.DataAnnotations;

namespace grad_proV1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? UserAddress { get; set; }
        public string? OptionalNum { get; set; }
        public List<Product> products { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }

    }
}
