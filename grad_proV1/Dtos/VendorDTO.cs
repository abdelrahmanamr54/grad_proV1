using System.ComponentModel.DataAnnotations.Schema;

namespace grad_proV1.Dtos
{
    public class VendorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int subcategoyId { get; set; }
     //   public string subcategoyname { get; set; }
        public string ImageUrl { get; set; }
       // [NotMapped]
        //public IFormFile ImageFile { get; set; }
    }
}
