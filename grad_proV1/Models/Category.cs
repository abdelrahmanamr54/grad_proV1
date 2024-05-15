namespace grad_proV1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<SubCategory>  subCategories{ get; set; }
     //   public List<Vendor> vendors { get; set; }

    }
}
