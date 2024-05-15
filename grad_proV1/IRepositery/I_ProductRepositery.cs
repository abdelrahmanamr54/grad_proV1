using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface I_ProductRepositery
    {
        List<Product> GetAllProducts();
       

        Product GetById(int id);
      



        void AddNewProduct(Product product);
       

        void Delete(int id);
       


        Product Updateproduct(Models.Product product);
    }
}
