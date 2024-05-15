using System.ComponentModel.DataAnnotations;

namespace grad_proV1.Dtos
{
    public class SubCategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

    }
}
