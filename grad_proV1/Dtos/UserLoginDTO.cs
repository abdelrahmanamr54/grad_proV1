using System.ComponentModel.DataAnnotations;

namespace grad_proV1.Models
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}


