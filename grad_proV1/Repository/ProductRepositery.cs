using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;

namespace grad_proV1.Repository
{
    public class ProductRepositery : I_ProductRepositery
    {
        private readonly ApplicationDbContext context;


        public ProductRepositery(ApplicationDbContext context)
        {
            this.context = context;

        }

        public List<Product> GetAllProducts()
        {

            var Products = context.products.ToList();
         //   var vendorproducts = context.CartItems.Where(venderid == id);



            return Products;


        }


        public Product GetById(int id)
        {
            //   var providers = context.providers.Include(e => e.service).Where(e => e.ServiceId == id).ToList();
            var Product = context.products.Find(id);



            return Product;
        }



        public void AddNewProduct(Product product)
        {


            var newProduct = context.products.Add(new Product
            {
                Name = product.Name,
               Description = product.Description,
                ImageUrl = product.ImageUrl,
             Price = product.Price,
               subCategoryId = product.subCategoryId,
                vendorId = product.vendorId,
            });

            context.SaveChanges();





        }


        public void Delete(int id)
        {
            var selectedproduct = context.products.Find(id);
            if (selectedproduct is not null)

            {
                context.products.Remove(selectedproduct);
                context.SaveChanges();
            }

        }


        public Product Updateproduct(Models.Product product)
        {
            var selectedproduct = context.products.Find(product.Id);
            if (selectedproduct is not null)

            {
                selectedproduct.Name = product
                   .Name;
                selectedproduct.Description = product
 .Description;
                selectedproduct.ImageUrl = product
   .ImageUrl;


                selectedproduct.Price = product.Price
                   ;
                selectedproduct.vendorId = product
                   .vendorId;
                selectedproduct.subCategoryId = product
   .subCategoryId;


           



                context.SaveChanges();
            }
            return selectedproduct;

        }
    }
}
