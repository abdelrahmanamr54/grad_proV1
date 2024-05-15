using grad_proV1.Dtos;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using grad_proV1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly I_ProductRepositery productRepositery;
     //   private readonly ICartRepository cartRepository;
        public ProductController(I_ProductRepositery productRepositery )
        {
            this.productRepositery = productRepositery;
           // this.cartRepository = cartRepository;
        }
        [HttpGet]
        [Route("/api/Products/AllProduct")]
        public IActionResult Read()
        {

            var Services = productRepositery.GetAllProducts();

            //context.Services.ToList();




            return Ok(Services);


        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var Services = productRepositery.GetById(id);

            //context.Services.Find(id);




            return Ok(Services);
        }

        [HttpPost]
        [Route("/api/Products/Add")]
        //[Route("{name}")]
        public IActionResult Add_Product(ProductDTO productDTO)
        {

            Product product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                ImageUrl = productDTO.ImageUrl,
                Price = productDTO.Price,
             subCategoryId = productDTO.SubCategoryId,
             vendorId = productDTO.VendorId,
            };
            productRepositery.AddNewProduct(product);



            return Ok(product);


        }
        [HttpDelete]
        [Route("/api/Productes/Delete")]
        public IActionResult Delete(int id)
        {
            productRepositery.Delete(id);

          
            return Ok();
        }
      
        [HttpPut]
        [Route("/api/Products/Update")]
        public IActionResult Update(ProductDTO productDTO)
        {
            Product product = new Product()
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                ImageUrl = productDTO.ImageUrl,
                Price = productDTO.Price,
               subCategoryId = productDTO.SubCategoryId,
               vendorId = productDTO.VendorId,
            };
            var selectedProduct = productRepositery.Updateproduct(product);


            return Ok(selectedProduct);
        }


    }
}
