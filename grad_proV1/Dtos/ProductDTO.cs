namespace grad_proV1.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int SubCategoryId { get; set; }

        public int VendorId { get; set; }
    }
}
