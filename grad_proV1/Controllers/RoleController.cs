using grad_proV1.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRoleDTO userRoleDTO)
        {
            //role.8
            if (ModelState.IsValid)
            {
                //role.9 
                //mapping(casting) aw de el service ely bt3mel mapping w 3shan gadwal el identityRole mtwak3 enena bndef feh bs el name hwa 3amlo ka2no constructor msh lazem n3melo zy object initializer {}
                IdentityRole identityRole = new IdentityRole(userRoleDTO.Name);

                //role.10  badef roles zy admin w subAdmin w user ....
                var result = await roleManager.CreateAsync(identityRole);

                //role.11
                if (result.Succeeded)
                {
                    return Ok(userRoleDTO);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Role");
                }


            }
            return Ok(userRoleDTO);
        }
    }
}
