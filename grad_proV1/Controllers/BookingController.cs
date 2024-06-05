using grad_proV1.Data;
using grad_proV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using grad_proV1.Dtos;

namespace grad_proV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly ApplicationDbContext context;
         private readonly UserManager<ApplicationUser> userManager;

            public BookingController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            {
                this.context = context;
                this.userManager = userManager;

            }
            [HttpPost]
            public async Task<IActionResult> Enroll(int providerId, string code)
            {
                if (string.IsNullOrEmpty(code))
                {
                    //ViewBag.Message = "Enrollment code cannot be empty!";
                    //return RedirectToAction("AddCart", new { id = providerId });
                    return BadRequest("Empty");
                }


                var enrollmentCode = context.EnrollmentCodes
                    .FirstOrDefault(e => e.BookingCode == code);
                if (enrollmentCode != null)
                {

                    var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                    if (token == null)
                    {
                        return Unauthorized("Authorization token is missing.");
                    }


                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    SecurityToken validatedToken;
                    ClaimsPrincipal claimsPrincipal;

                    try
                    {
                        claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                    }
                    catch (Exception ex)
                    {
                        return Unauthorized("Invalid token: " + ex.Message);
                    }

                    // Find the claim containing the user ID
                    var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim == null)
                    {
                        return Unauthorized("User ID claim not found in token.");
                    }

                    string userId = userIdClaim.Value;
                    var provider = await context.providers.FindAsync(providerId);
                    // string userlogedgradeId = user.Id;                   


                    var existingEnrollment = context.BookingItems
                            .FirstOrDefault(e => e.ProviderId == providerId && e.UserId == userId);

                    if (existingEnrollment != null)
                    {
                  
                    //ViewBag.Message = "You are already enrolled in this course!";
                    return Unauthorized("You are already enrolled in this course!");
                }
                    else
                    {


                    var Enroll = context.EnrollmentCodes.Find(enrollmentCode.Id);
                    var proId = context.providers.Find(provider.Id);
                    var enrollment = new BookingItem
                    {
                        UserId = userId,
                        ProviderId = providerId,
                        //EnrollmentCode = enrollmentCode,
                        EnrollmentCode = Enroll.BookingCode,
                        //Provider = provider,


                    };
                    context.BookingItems.Add(enrollment);
                    context.Entry(enrollment).State = EntityState.Added;
                    context.SaveChanges();        
                    context.EnrollmentCodes.Remove(enrollmentCode);
                    context.SaveChanges();
                    var jsonSerializerOptions = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                                       
                      // Other options as needed                                                 
                    };

                    // Serialize the object using JsonSerializerOptions
        var jsonResponse = JsonSerializer.Serialize(enrollment, jsonSerializerOptions);

                    // Return the serialized object
                      return Ok(jsonResponse);


                    // Mark the enrollment code as used (delete or mark it in some way)


                    return Ok(enrollment);


                    //  ViewBag.Message = "Enrolled successfully!";
                }
                }
                else
                {
                    //ViewBag.Message = "Invalid enrollment code or course ID!";
                    return BadRequest("Invalid enrollment code or course ID!");
                }

                //return View("index","Lecture");
                //return RedirectToAction("index", "Home");
                return Ok("ok");
            }

            [HttpGet("AllBooking")]
            public IActionResult GetAllBooking()
            {
                var items = context.BookingItems.ToList();
                return Ok(items);
            }
        [HttpPost("ADDcode")]
        public IActionResult AddNewCode( EnrollmentCode enrollmentCode)
        {
            context.EnrollmentCodes.Add(enrollmentCode);
            return Ok();
        }


        [HttpGet("AllBookingWithuserName")]
        public async Task<List<BookingItemViewModel>> GetBookingItemsAsync()
        {
            var bookingItems = await context.BookingItems
                .Include(b => b.Provider) 
                .Join(context.Users,
                      bookingItem => bookingItem.UserId,
                      user => user.Id,
                      (bookingItem, user) => new BookingItemViewModel
                      {
                          Id = bookingItem.Id,
                          UserId = bookingItem.UserId,            
                          UserName = user.UserName,                  
                          ProviderId = bookingItem.ProviderId,                               
                          Provider = bookingItem.Provider,                    
                          EnrollmentCode = bookingItem.EnrollmentCode                     
                      })
                .ToListAsync();

            return bookingItems;
        }
        [HttpGet("GetBookingofEachprovider")]
        public async Task<IActionResult> GetBookingofEachprovider()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return Unauthorized("Authorization token is missing.");
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM")),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal;

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid token: " + ex.Message);
            }

            // Find the claim containing the user ID
            var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.HomePhone);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found in token.");
            }

            string userId = userIdClaim.Value;
            var bookingItems = await context.BookingItems
                .Include(b => b.Provider)
                .Join(context.Users,
                      bookingItem => bookingItem.UserId,
                      user => user.Id,
                      (bookingItem, user) => new BookingItemViewModel
                      {
                          Id =  bookingItem.Id,
                          UserId =   bookingItem.UserId,
                          UserName = user.UserName,
                          UserEmail=    user.Email,
                         

                          ProviderId = bookingItem.ProviderId,
                          Provider = bookingItem.Provider,
                          EnrollmentCode = bookingItem.EnrollmentCode
                      })
            .Where(e=>e.ProviderId.ToString() == userId)   .ToListAsync();

            return Ok(bookingItems) ;
        }
        [HttpGet]
        [Route("selectCode")]
        public async Task<IActionResult> selectCode ()
        {
            var selectedcode = context.EnrollmentCodes.First();


            return Ok(selectedcode);
                }

            [HttpGet]
        [Route("GetUserbooking")]

        public async Task<IActionResult> GetUserbooking()
        {
            try
            {

                var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    return Unauthorized("Authorization token is missing.");
                }


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal;

                try
                {
                    claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                }
                catch (Exception ex)
                {
                    return Unauthorized("Invalid token: " + ex.Message);
                }

                // Find the claim containing the user ID
                var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized("User ID claim not found in token.");
                }

                string userId = userIdClaim.Value;

                var usercart = context.BookingItems.Where(e => e.UserId == userId).Include(e=>e.Provider).Select(e=>new {e.UserId, e.Provider,e.EnrollmentCode }).ToList();


                var bookingItems = await context.BookingItems
            .Include(b => b.Provider)
            .Join(context.Users,
                  bookingItem => bookingItem.UserId,
                  user => user.Id,
                  (bookingItem, user) => new BookingItemViewModel
                  {
                      Id = bookingItem.Id,
                      UserId = bookingItem.UserId,
                      UserName = user.UserName,
                      UserEmail = user.Email,


                      ProviderId = bookingItem.ProviderId,
                      ProviderName = bookingItem.Provider.Name,
                      EnrollmentCode = bookingItem.EnrollmentCode
                  })
        .Where(e => e.UserId == userId).ToListAsync();
                //var viewModel = usercart.Select(order => new BookingItemDTO
                //{
                //    UserId = order.UserId,
                //    ProviderId = order.ProviderId,
                //    Provider = context.providers.Find(order.ProviderId),
                //    EnrollmentCode=order.EnrollmentCode
                //}).ToList();
               // context.Entry(usercart).State = EntityState.Added;
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,

                    // Other options as needed                                                 
                };

                // Serialize the object using JsonSerializerOptions
                var jsonResponse = JsonSerializer.Serialize(usercart, jsonSerializerOptions);

                // Return the serialized object
                return Ok(usercart);

                return Ok(

                  bookingItems
                //usercart1,
                //totalPrice


                );

                // return Ok(usercart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);

            }
        }


    }
}
