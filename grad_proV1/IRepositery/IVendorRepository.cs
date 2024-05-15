using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface IVendorRepository
    {

        void Create(Vendor vendor);
        void Update(Vendor vendor);
        List<Vendor> ReadAll();
        Vendor FindById(int id);
        void Delete(Vendor vendor);
        List<Vendor> ReadAllWithCategory(int id);
      //  List<Vendor> FindByCatId(int catId);
        List<Product> GetProductsById(int id);
    }
}
