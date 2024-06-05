using grad_proV1.Models;

namespace grad_proV1.Dtos
{
    public class OrderitemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
