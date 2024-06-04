using grad_proV1.Models;

namespace grad_proV1.Dtos
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
