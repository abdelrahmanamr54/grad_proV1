using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;

namespace grad_proV1.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ApplicationDbContext context;
        public SubCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(SubCategory subCategory)
        {
            context.subCategories.Add(subCategory);
            context.SaveChanges();
        }

        public void Delete(SubCategory subCategory)
        {
            context.subCategories.Remove(subCategory);
            context.SaveChanges();
        }

        public SubCategory FindById(int id)
        {
            return context.subCategories.Find(id);
        }

        public List<SubCategory> FindByCatId(int catId)
        {
            return context.subCategories.Where(e => e.CategoryId == catId).ToList();

        }

        public List<Product> GetProductsById(int id)
        {
            var ListOfProducts = context.products.Where(e => e.subCategoryId == id).ToList();

            return ListOfProducts;
        }

        public List<SubCategory> ReadAllWithCategory(int id)
        {
            return context.subCategories.Where(e => e.CategoryId == id).ToList();
        }
        public List<SubCategory> ReadAll()
        {
            return context.subCategories.ToList();
        }

        public void Update(SubCategory subCategory)
        {
            var oldSub = context.subCategories.Find(subCategory.Id);
            if (oldSub != null)
            {
                oldSub.Name = subCategory.Name;
                oldSub.ImageUrl = subCategory.ImageUrl;
                oldSub.CategoryId = subCategory.CategoryId;
           
                context.SaveChanges();

            }
        }
    }
}
