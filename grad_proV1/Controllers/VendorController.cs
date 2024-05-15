using grad_proV1.Dtos;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {

        private readonly IVendorRepository vendorRepository;

        public VendorController(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;

        }

        [HttpGet("VendorGetAll")]
        public IActionResult GetAll()
        {
            var vendor = vendorRepository.ReadAll();
            //Mapping
            List<VendorDTO> vendorDTOs = new List<VendorDTO>();
            foreach (var item in vendor)
            {
                VendorDTO vendorDTO = new VendorDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Description = item.Description,
                    Phone = item.Phone,
                    ImageUrl=item.ImageUrl,
                    subcategoyId=item.subcategoyId
                   // subcategoyname= context.brands.FirstOrDefault(b => b.Id == laptop.BrandId).Name
                };
                vendorDTOs.Add(vendorDTO);
            }

            return Ok(vendorDTOs);
        }

        [HttpGet("vendorGetProductsById/{id}")]

        public IActionResult GetProductsById(int id)
        {
            var products = vendorRepository.GetProductsById(id);
            return Ok(products);

        }



        [HttpGet("VendorGetAllWithCat")]
        public IActionResult GetAllWithCat(int id)
        {
            var vendor = vendorRepository.ReadAllWithCategory(id);
            //Mapping
            List<VendorDTO> vendorDTOs = new List<VendorDTO>();
            foreach (var item in vendor)
            {
                VendorDTO vendorDTO = new VendorDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address,
                    Description = item.Description,
                    Phone = item.Phone,
                  subcategoyId = item.subcategoyId,
                  ImageUrl=item.ImageUrl,

                };
                vendorDTOs.Add(vendorDTO);
            }

            return Ok(vendorDTOs);
        }

        [HttpPost("vendorCreate")]
        public IActionResult Create(VendorDTO vendorDTO)
        {

            if (ModelState.IsValid)
            {
                //Mapping
                Vendor vendor = new Vendor()
                {
                    Name = vendorDTO.Name,
                    Address = vendorDTO.Address,
                    Email = vendorDTO.Email,
                    Description = vendorDTO.Description,
                    Phone = vendorDTO.Phone,
                    ImageUrl=vendorDTO.ImageUrl,
subcategoyId= vendorDTO.subcategoyId,
                };


                vendorRepository.Create(vendor);
                return Created($"http://localhost:5093/api/Vendor/{vendor.Id}", vendorDTO);
            }
            return BadRequest();
        }


        [HttpPut("vendorUpdate")]
        public IActionResult Update(VendorDTO vendorDTO)
        {
            if (ModelState.IsValid)
            {
                var oldVendor = vendorRepository.FindById(vendorDTO.Id);
                if (oldVendor != null)
                {
                    //Mapping
                    Vendor vendor = new Vendor()
                    {
                        Id = vendorDTO.Id,
                        Name = vendorDTO.Name,
                        Address = vendorDTO.Address,
                        Email = vendorDTO.Email,
                        Description = vendorDTO.Description,
                        Phone = vendorDTO.Phone,
                        subcategoyId=vendorDTO.subcategoyId,
                        ImageUrl=vendorDTO.ImageUrl
                    };
                    vendorRepository.Update(vendor);

                    return Ok(vendorDTO);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }
        [HttpDelete("vendorDelete")]
        public IActionResult Delete(int id)
        {
            var vendor = vendorRepository.FindById(id);
            if (vendor != null)
            {
                vendorRepository.Delete(vendor);
                VendorDTO vendorDTO = new VendorDTO()
                {
                    Id = vendor.Id,
                    Name = vendor.Name,
                    Address = vendor.Address,
                    Email = vendor.Email,
                    Description = vendor.Description,
                    Phone = vendor.Phone,
                    ImageUrl=vendor.ImageUrl,
                };
                return Ok(vendorDTO);
            }


            return NotFound();


        }

    }
}
