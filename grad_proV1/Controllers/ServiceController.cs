using grad_proV1.Data;
using grad_proV1.Dtos;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using grad_proV1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ServiceController : ControllerBase
    {
        //   ApplicationDbContext context;
        private readonly I_ServiceRepositery serviceRepositery;

        public ServiceController(I_ServiceRepositery serviceRepositery)
        {
            this.serviceRepositery = serviceRepositery;

        }
        [HttpGet]
        [Route("/api/Services/AllService")]
        public IActionResult Read()
        {

            var Services = serviceRepositery.GetAllServices();
                
                //context.Services.ToList();




            return Ok(Services);


        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var Services = serviceRepositery.GetById(id);
                
                //context.Services.Find(id);




            return Ok(Services);
        }

        [HttpPost]
        [Route("/api/Services/Add")]
        //[Route("{name}")]
        public IActionResult Add_Service(ServiceDTO serviceDTO)
        {



            var service = new Service()
            {
                Name = serviceDTO.Name,
                  Description = serviceDTO.Description
            };

             serviceRepositery.AddNewService(service);
                //context.Services.Add(new Service { Name=name});

           // context.SaveChanges();


            return Ok();


        }
        [HttpDelete]
        [Route("/api/Services/Delete")]
        public IActionResult Delete(int id)
        {
            serviceRepositery.Delete(id);
                
            //    context.Services.Find(id);
            //if (selectedServices is not null)

            //{
            //    context.Services.Remove(selectedServices);
            //    context.SaveChanges();
            //}
            return Ok();
        }
        [HttpPut]
        [Route("/api/Services/Update")]
        public IActionResult Update(ServiceDTO serviceDTO)
        {

            var service = new Service()
            {
                Id=serviceDTO.Id,
                Name = serviceDTO.Name,
                Description=serviceDTO.Description
            };

            var selectedServices = serviceRepositery.UpdateService(service);
                
            //    context.Services.Find(service.Id);
            //if (selectedServices is not null)

            //{
            //    selectedServices.Name = service.Name;

               
            //    context.SaveChanges();
            //}
            return Ok(selectedServices);
        }
    }
}
