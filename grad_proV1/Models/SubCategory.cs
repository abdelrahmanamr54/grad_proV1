namespace grad_proV1.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }

        public List<Product> products { get; set; }
        public List<Vendor> vendors { get; set; }
    }
}
