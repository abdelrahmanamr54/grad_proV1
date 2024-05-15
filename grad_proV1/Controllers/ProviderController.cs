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

   
    public class ProviderController : ControllerBase
    {
        private readonly I_ProviderRepositery providerRepositery;
        // I_ServiceRepositery serviceRepositery = new ServiceRepositery();

        public ProviderController(I_ProviderRepositery providerRepositery)
        {
            this.providerRepositery=providerRepositery;

        }
        [HttpGet]
        [Route("/api/Providers/AllProviders")]
      
        public IActionResult Read()
        {

            var providers = providerRepositery.GetAllproviders();
            return Ok(providers);


        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var provider = providerRepositery.GetById(id);
           
            return Ok(provider);
        }
        [HttpPost]
        [Route("/api/Providers/Add")]
     
        public IActionResult Add_provider(ProviderDTO providerDTO)
        {

            var provider = new Provider()
            {
                Name = providerDTO.Name,
                ImageUrl = providerDTO.ImageUrl,
                Email=providerDTO.Email,
                Phone=providerDTO.Phone,
                Address=providerDTO.Address,
                Description = providerDTO.Description,
                ServiceId =providerDTO.ServiceId
                
            };
            providerRepositery.AddNewprovider(provider);
          


            return Ok();


        }
        [HttpDelete]
        [Route("/api/Providers/Delete")]
        public IActionResult Delete(int id)
        {
           providerRepositery.Delete(id);

          
            return Ok();
        }
        [HttpPut]
        [Route("/api/Providers/Update")]
        public IActionResult Update(ProviderDTO providerDTO)
        {




            var provider = new Provider()
            {
                Id=providerDTO.Id,
                Name = providerDTO.Name,
                ImageUrl = providerDTO.ImageUrl,
                Email = providerDTO.Email,
                Phone = providerDTO.Phone,
                Address = providerDTO.Address,
                Description=providerDTO.Description,
                ServiceId = providerDTO.ServiceId

            };
            var selectedprovider = providerRepositery.UpdateProvidere(provider);

        
            return Ok(selectedprovider);
        }



    }
}
