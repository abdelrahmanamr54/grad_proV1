using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;

namespace grad_proV1.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Category category)
        {
            context.categories.Add(category);
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            context.categories.Remove(category);
            context.SaveChanges();
        }

        public Category FindById(int id)
        {
            return context.categories.Find(id);
        }
        public List<SubCategory> GetSubCategoryById(int VId)
        {
            var subcategory = context.subCategories.Where(e => e.CategoryId == VId).ToList();

            return subcategory;
        }

        public List<Category> ReadAll()
        {
            return context.categories.ToList();
        }

        public Category Update(Category category)
        {
            var oldCat = context.categories.Find(category.Id);
            if (oldCat != null)
            {
                oldCat.Name = category.Name;
                oldCat.ImageUrl = category.ImageUrl;
                context.SaveChanges();

            }
            return oldCat;
        }

        }
}
