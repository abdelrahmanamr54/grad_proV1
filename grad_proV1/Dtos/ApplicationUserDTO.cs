using System.ComponentModel.DataAnnotations;
namespace grad_proV1.Models
{
    public class ApplicationUserDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
     
        public string ConfirmPassword { get; set; }
        public int? Provider { get; set; }
        //  [RegularExpression("Cairo|Alex|Mansoura")]
        public string Address { get; set; }
     //   public bool RememberMe { get; set; }
    }
}
