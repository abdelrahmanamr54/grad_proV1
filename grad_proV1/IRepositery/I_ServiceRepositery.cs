using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface I_ServiceRepositery
    {


      List<Service> GetAllServices();
         List<Provider> GetById(int id);
       


        void AddNewService(Service service);
      


      void Delete(int id);



       Service UpdateService(Models.Service service);
    }
}
