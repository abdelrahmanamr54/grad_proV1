namespace grad_proV1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        //  public int Stock { get; set; }
        //  public Category category { get; set; }

        public SubCategory subcategory { get; set; }
       // public int CategoryId { get; set; }
        public int subCategoryId { get; set; }
  
       public Vendor  vendor{ get; set; }

        public int vendorId { get; set; }

    }
}
