using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface ICategoryRepository
    {
        void Create(Category category);
        Category Update(Category category);
        List<Category> ReadAll();
        Category FindById(int id);
       List<SubCategory> GetSubCategoryById(int VId);
        void Delete(Category category);
    }
}
