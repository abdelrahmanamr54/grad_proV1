using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;

namespace grad_proV1.Repository
{
    public class ProviderRepositery : I_ProviderRepositery

    {
        // ApplicationDbContext context = new ApplicationDbContext();
        private readonly ApplicationDbContext context;
       

        public ProviderRepositery(ApplicationDbContext context)
        {
            this.context = context;

        }
        public List<Provider> GetAllproviders()
        {

            var providers = context.providers.ToList();




            return providers;


        }


        public  Provider GetById(int id)
        {
            //   var providers = context.providers.Include(e => e.service).Where(e => e.ServiceId == id).ToList();
            var provider = context.providers.Find(id);



            return provider;
        }



        public void AddNewprovider(Provider Newprovider)
        {


            var provider = context.providers.Add(new Provider { Name = Newprovider.Name ,ImageUrl= Newprovider .ImageUrl ,Email= Newprovider .Email, 
            Phone=Newprovider.Phone ,Address=Newprovider.Address,Description=Newprovider.Description,ServiceId=Newprovider.ServiceId});

            context.SaveChanges();





        }


        public void Delete(int id)
        {
            var selectedprovider = context.providers.Find(id);
            if (selectedprovider is not null)

            {
                context.providers.Remove(selectedprovider);
                context.SaveChanges();
            }

        }


        public Provider UpdateProvidere(Models.Provider provider)
        {
            var selectedprovider = context.providers.Find(provider.Id);
            if (selectedprovider is not null)

            {
                selectedprovider.Name = provider
                   .Name;
                selectedprovider.ImageUrl = provider
   .ImageUrl;
                selectedprovider.Email = provider
                   .Email;
                selectedprovider.Address = provider
                   .Address;
                selectedprovider.Phone = provider
   .Phone;
                selectedprovider.Description = provider
.Description;

                selectedprovider.ServiceId = provider
   .ServiceId;



                context.SaveChanges();
            }
            return selectedprovider;

        }
    }

}
