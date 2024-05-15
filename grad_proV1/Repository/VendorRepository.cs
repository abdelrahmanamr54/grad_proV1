using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;

namespace grad_proV1.Repository
{
    public class VendorRepository : IVendorRepository
    {

        private readonly ApplicationDbContext context;
        public VendorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void Create(Vendor vendor)
        {
            context.vendors.Add(vendor);
            context.SaveChanges();
        }

        public void Delete(Vendor vendor)
        {
            context.vendors.Remove(vendor);
            context.SaveChanges();
        }

        public Vendor FindById(int id)
        {
            return context.vendors.Find(id);
        }



        public List<Product> GetProductsById(int id)
        {
            var ListOfProducts = context.products.Where(e => e.vendorId == id).ToList();

            return ListOfProducts;

        }

        public List<Vendor> ReadAll()
        {
            return context.vendors.ToList();

        }



        public List<Vendor> ReadAllWithCategory(int id)
        {
            //  return context.vendors.Where(e => e.CategoryId == id).ToList();
            return context.vendors.Where(e => e.subcategoyId == id).ToList();
        }


        public void Update(Vendor vendor)
        {
            var oldVendor = context.vendors.Find(vendor.Id);
            if (oldVendor != null)
            {
                oldVendor.Name = vendor.Name;
                oldVendor.Address = vendor.Address;
                oldVendor.Email = vendor.Email;
                oldVendor.Phone = vendor.Phone;
                oldVendor.Description = vendor.Description;
                oldVendor.subcategoyId = vendor.subcategoyId;
                oldVendor.ImageUrl = vendor.ImageUrl;
                context.SaveChanges();

            }
            
        }


        //List<Vendor> IVendorRepository.FindByCatId(int catId)
        //{
        //    return context.vendors.Where(e => e.CategoryId == catId).ToList();
        //}


    }
}
