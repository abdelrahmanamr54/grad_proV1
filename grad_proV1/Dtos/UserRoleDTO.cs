using System.ComponentModel.DataAnnotations;

namespace grad_proV1.Dtos

{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
