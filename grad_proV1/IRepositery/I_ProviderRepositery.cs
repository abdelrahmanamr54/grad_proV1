using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface I_ProviderRepositery
    {
        List<Provider> GetAllproviders();
       


        Provider GetById(int id);
       


        void AddNewprovider(Provider Newprovider);
      


        void Delete(int id);
        

        Provider UpdateProvidere(Models.Provider provider);
    }
}
