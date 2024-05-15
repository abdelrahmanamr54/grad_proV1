using grad_proV1.Dtos;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository subCategoryRepository;
        public SubCategoryController(ISubCategoryRepository subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }
        [HttpGet("SubGetAll")]
        public IActionResult GetAll()
        {
            var subCat = subCategoryRepository.ReadAll();
            //Mapping
            List<SubCategoryDTO> subDTOs = new List<SubCategoryDTO>();
            foreach (var item in subCat)
            {
                SubCategoryDTO subDTO = new SubCategoryDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl=item.ImageUrl,
                    CategoryId = item.CategoryId,
                };
                subDTOs.Add(subDTO);
            }

            return Ok(subDTOs);
        }

        [HttpGet("GetProductsBySubId/{id}")]
        public IActionResult GetProductsById(int id)
        {
            var products = subCategoryRepository.GetProductsById(id);
            return Ok(products);

        }

        [HttpGet("SubGetAllWithCatId")]
        public IActionResult GetAllWithCat(int id)
        {
            var sub = subCategoryRepository.ReadAllWithCategory(id);
            //Mapping
            List<SubCategoryDTO> subDTOs = new List<SubCategoryDTO>();
            foreach (var item in sub)
            {
                SubCategoryDTO subDTO = new SubCategoryDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    CategoryId = item.CategoryId,

                };
                subDTOs.Add(subDTO);
            }

            return Ok(subDTOs);
        }

        [HttpPost("SubCreate")]
        public IActionResult Create(SubCategoryDTO subDTO)
        {

            if (ModelState.IsValid)
            {
                //Mapping
                SubCategory sub = new SubCategory()
                {
                    Name = subDTO.Name,
                    ImageUrl = subDTO.ImageUrl,
                    CategoryId = subDTO.CategoryId,
                };


                subCategoryRepository.Create(sub);
                return Created($"http://localhost:5093/api/Vendor/{sub.Id}", subDTO);
            }
            return BadRequest();
        }


        [HttpPut("SubUpdate")]
        public IActionResult Update(SubCategoryDTO subDTO)
        {
            if (ModelState.IsValid)
            {
                var oldSub = subCategoryRepository.FindById(subDTO.Id);
                if (oldSub != null)
                {
                    //Mapping
                    SubCategory sub = new SubCategory()
                    {
                        Id = oldSub.Id,
                        Name = oldSub.Name,
                        ImageUrl =oldSub.ImageUrl,
                        CategoryId = oldSub.CategoryId
                    };
                    subCategoryRepository.Update(sub);

                    return Ok(subDTO);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }

        [HttpDelete("SubDelete")]
        public IActionResult Delete(int id)
        {
            var sub = subCategoryRepository.FindById(id);
            if (sub != null)
            {
                subCategoryRepository.Delete(sub);
                SubCategoryDTO subDTO = new SubCategoryDTO()
                {
                    Id = sub.Id,
                    Name = sub.Name,
                    CategoryId = sub.CategoryId,

                };
                return Ok(subDTO);
            }


            return NotFound();


        }
    }
}
