using grad_proV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signIn;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn, IConfiguration configuration, IHttpContextAccessor _httpContextAccessor)
        {
            this.userManager = userManager;
            this.signIn = signIn;
            this.configuration = configuration;
            this._httpContextAccessor= _httpContextAccessor; ;
        }

        //private async Task<string> GenerateJwtToken(ApplicationUser user)
        //{
        //    var randomNumberGenerator = RandomNumberGenerator.Create();
        //    var keyBytes = new byte[32]; // 32 bytes for 256-bit key
        //    randomNumberGenerator.GetBytes(keyBytes);
        //    var keyString = Convert.ToBase64String(keyBytes);

        //    // 2. Create signing credentials
        //    var signingKeyBytes = Encoding.UTF8.GetBytes(keyString);
        //    var symmetricKey = new SymmetricSecurityKey(signingKeyBytes);
        //    var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        //    //var claimsIdentity = new ClaimsIdentity(new IdentityOptions().ClaimsIdentity);
        //    //claimsIdentity.AddClaim(new Claim(ClaimTypes.Sub, user.Id)); //
        //    // 3. Define token claims
        //              var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(ClaimTypes.Email, user.Email)
        //    };

        //    var roles = await userManager.GetRolesAsync(user);
        //    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        //    // 4. Create security token descriptor
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims
        //        ),
        //        Expires = DateTime.UtcNow.AddMinutes(30), // Adjust expiration time as needed
        //        SigningCredentials = signingCredentials
        //    };

        //    // 5. Create and write the JWT token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(securityToken);

        //    return tokenString;
        //}
        private string GenerateToken(ApplicationUser userVM)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, userVM.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, userVM.Id),
        new Claim(ClaimTypes.Email, userVM.Email),
        new Claim(ClaimTypes.Role, "User")
    };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    //    private string generayrToken(ApplicationUser userVM)
    //    {
    //        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    //        var cred = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
    //        var claims = new[]
    //{
                              
    //                            new Claim(ClaimTypes.Email,  userVM.Email),
                             
    //                            new Claim(ClaimTypes.NameIdentifier,  userVM.Id),
    //                            new Claim(ClaimTypes.Role,"user")
                               
    //                        };
    //        var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred
    //);
    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }




        //      private string generayrToken(ApplicationUser userVM)
        //      {
        //          var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        //          var cred = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256); // Use HmacSha256 algorithm
        //                                                                                         //   var claimsIdentity = new ClaimsIdentity(new IdentityOptions().ClaimsIdentity);
        //                                                                                         //   claimsIdentity.AddClaim(new Claim(ClaimTypes.Sub, userId.ToString())); // Use ClaimTypes.Sub for subject
        //          var claims = new[]
        //{
        //                              new Claim(ClaimTypes.NameIdentifier, userVM.Id.ToString()),
        //                              new Claim(ClaimTypes.Email,  userVM.Email)
        //                              // Add other claims as needed
        //                          };
        //          var tokenDescriptor = new SecurityTokenDescriptor
        //          {
        //              Subject = new ClaimsIdentity(new Claim[]
        //                  {
        //                                  new Claim(ClaimTypes.Email

        //                                      , userVM.Email),
        //                             //     new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        //                                  new Claim(ClaimTypes.NameIdentifier, userVM.Id),
        //                                  new Claim("sub", userVM.Id.ToString()
        //                                       ),
        //                                 //    new Claim(ClaimTypes.Role, userVM.Role), 
        //                                  new Claim("nbf", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
        //                      new Claim("exp", DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds().ToString()),
        //                      new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        //                      // Add other claims as needed
        //                  }),




        //              Expires = DateTime.UtcNow.AddDays(1), // Token expiry
        //              SigningCredentials = cred
        //          };
        //          var tokenHandler = new JwtSecurityTokenHandler();
        //          var token = tokenHandler.CreateToken(tokenDescriptor);
        //          var tokenString = tokenHandler.WriteToken(token);

        //          return tokenString;
        //      }


        [HttpPost]
        [Route("api/Account/Registration")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(ApplicationUserDTO userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser() { UserName = userVM.Name, Email = userVM.Email, Address = userVM.Address, PasswordHash = userVM.Password };

                var result = await userManager.CreateAsync(user, userVM.Password);
               // await userManager.AddClaimAsync(user, new Claim("userId", user.Id));
                if (result.Succeeded)
                {
                    await signIn.SignInAsync(user, false);
                    return Ok();
                }
                return Unauthorized("bad request");
            }

            return Unauthorized("bad request");
        }
      
        [HttpPost]
        [Route("api/Account/Login")]
        public async Task<IActionResult> Login(UserLoginDTO
            userVM)
        {
       
            if (ModelState.IsValid)
            {
                var result = await userManager.FindByEmailAsync(userVM.Email);
                if (result != null)
                {
                    bool check = await userManager.CheckPasswordAsync(result, userVM.Password);

                //    var user = await signIn.PasswordSignInAsync(username, password, false);
                  //  var claimsPrincipal = await userManager.GetUsersForClaimAsync();

                    if (check)
                    {
                        await signIn.SignInAsync(result, userVM.RememberMe);
                  //      var user = await signIn.PasswordSignInAsync(userName, Password, false);
                        var token = GenerateToken(result);
                     
                        var cookieOptions = new CookieOptions
                         {
                            HttpOnly = true,
                            // Add other options as needed (e.g., expiration time)
                        };
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("token", token, cookieOptions);
                        var msg = "succes";
                     
                        return Ok(new
                        {
                            token = token,
                            msg ="succes"
                        });
                      
                    }
                    //  ModelState.AddModelError("invalidpw", "invalidpassword");
                    return Unauthorized("Invalid  password");

                }
                else
                {
                    return Unauthorized("Invalid UserName");
                }

            }
            return Ok(userVM);

        }

    }
}
