using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace grad_proV1.Repository

   
{
   
    public class ServiceRepositery : I_ServiceRepositery
    {
        private readonly ApplicationDbContext context;


        public ServiceRepositery(ApplicationDbContext context)
        {
            this.context = context;

        }
        public List<Service> GetAllServices()
        {

            var Services = context.Services.ToList();




            return Services;


        }
       

        public List<Provider> GetById(int id)
        {
            var providers = context.providers.Where(e => e.ServiceId == id).ToList();




            return providers;
        }

       
    
        public void AddNewService(Service service)
        {


            var Service = context.Services.Add(service);

            context.SaveChanges();





        }
      

        public void Delete(int id)
        {
            var selectedServices = context.Services.Find(id);
            if (selectedServices is not null)

            {
                context.Services.Remove(selectedServices);
                context.SaveChanges();
            }
           
        }
    

        public  Service UpdateService(Models.Service service)
        {
            var selectedServices = context.Services.Find(service.Id);
            if (selectedServices is not null)

            {
                selectedServices.Name = service.Name;


                context.SaveChanges();
            }
            return selectedServices;
       
    }
}
}
