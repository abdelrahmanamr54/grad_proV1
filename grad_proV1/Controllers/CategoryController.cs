using grad_proV1.Dtos;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;

        }

        [HttpGet("categoryGetAll")]
        public IActionResult GetAll()
        {
            var category = categoryRepository.ReadAll();
            //Mapping
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            foreach (var item in category)
            {
                CategoryDTO categoryDTO = new CategoryDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl=item.ImageUrl


                };
                categoryDTOs.Add(categoryDTO);
            }

            return Ok(categoryDTOs);
        }

        [HttpGet("categoryGetById/{id}")]

        public IActionResult GetById(int id)
        {
            var subcategory = categoryRepository.GetSubCategoryById(id);

            return Ok(subcategory);

        }

        [HttpPost("categoryCreate")]
        public IActionResult Create(CategoryDTO categoryDTO)
        {

            if (ModelState.IsValid)
            {
                //Mapping
                Category category = new Category()
                {
                    Name = categoryDTO.Name,
                    ImageUrl=categoryDTO.ImageUrl



                };
                categoryRepository.Create(category);
                return Created($"http://localhost:5093/api/Category/{category.Id}", categoryDTO);
            }
            return BadRequest();
        }

        [HttpPut("categoryUpdate")]
        public IActionResult Update(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var oldEmp = categoryRepository.FindById(categoryDTO.Id);
                if (oldEmp != null)
                {
                    //Mapping
                    Category category = new Category()
                    {
                        Id = categoryDTO.Id,
                        Name = categoryDTO.Name,
                        ImageUrl=categoryDTO.ImageUrl
                    };
                    categoryRepository.Update(category);

                    return Ok(categoryDTO);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }

        [HttpDelete("categoryDelete")]
        public IActionResult Delete(int id)
        {
            var category = categoryRepository.FindById(id);
            if (category != null)
            {
                categoryRepository.Delete(category);
                CategoryDTO categoryDTO = new CategoryDTO()
                {
                    Id = category.Id,
                    Name = category.Name,

                };
                return Ok(categoryDTO);
            }


            return NotFound();


        }
    }
}
