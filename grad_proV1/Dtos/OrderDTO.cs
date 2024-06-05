using grad_proV1.Models;

namespace grad_proV1.Dtos
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? UserAddress { get; set; }
        public string? OptionalNum { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
