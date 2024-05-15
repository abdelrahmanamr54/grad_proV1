using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface ISubCategoryRepository
    {
        void Create(SubCategory subCategory);
        void Update(SubCategory subCategory);
        List<SubCategory> ReadAll();
        SubCategory FindById(int id);
        void Delete(SubCategory subCategory);
        List<SubCategory> ReadAllWithCategory(int id);
        List<SubCategory> FindByCatId(int catId);
        List<Product> GetProductsById(int id);
    }
}
