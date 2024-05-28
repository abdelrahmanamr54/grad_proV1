using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace grad_proV1.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ICartRepository cartRepository;
        public CartController(ICartRepository cartRepository, ApplicationDbContext context)
        {
            this.context = context;
            this.cartRepository = cartRepository;
        }
        private string GetLoggedInUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;
        }

        private string GetUserIdFromToken(string token)
        {
            try
            {
                // Create a token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // Decode the JWT token
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM")),
                    ValidateIssuer = true, // Set to true if you want to validate the token issuer

                    ValidateAudience = true // Set to true if you want to validate the token audience
                }, out SecurityToken validatedToken);


                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error decoding token: " + ex.Message);
                return null;
            }
        }
        //public IActionResult AddToCart(int productId)
        //{
        [HttpGet]
        [Route("/api/Cart/userid")]
        public async Task<IActionResult> getuserid()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("userid"));
            return Ok(new { user = id });
        }


        [HttpPost]
        [Route("AddToCart")]
        //  [HttpPost("AddToCart")]
        //public async Task<IActionResult> AddToCart(int productId)
        //{
        //    try
        //    {
        //        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        if (userId == null)
        //        {
        //            return Unauthorized("User ID claim not found in token.");
        //        }

        //        var addedToCart = await cartRepository.AddToCartAsync(productId, userId);
        //        if (addedToCart)
        //        {
        //            var response = new { message = "Product added to cart", productId };
        //            return Ok(response);
        //        }

        //        return BadRequest("Failed to add product to cart.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred: " + ex.Message);
        //    }
        //}

        //  [Authorize(Roles = "User")]
        //[Authorize]

        public async Task<IActionResult> AddToCart(int productId)
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
                var addedToCart = await cartRepository.AddToCartAsync(productId, userId);
                if (addedToCart)
                {
                    var response = JsonConvert.SerializeObject(new { message = "Product added to cart", productId = productId });
                    return Ok(response);
                }


                return Ok("Product successfully added to cart.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }



        [HttpGet]
        [Route("userCart")]

        public IActionResult getUserCart()
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


                var usercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();



                var order = new Order();


                order.products = new List<Product>();


                foreach (var cartItem in usercart)
                {

                    order.products.Add(cartItem.Product);
                }


                context.Orders.Add(order);
                context.SaveChanges();

                var ordersv1 = context.Orders.Select(e => e.products);



                //  var usercartv = context.Orders.Include(e => e.cartItems).Where(e => e. == userId).ToList();

                return Ok(usercart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);

            }
        }
        [HttpPost]
        [Route("checkout")]
        public IActionResult checkout()
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


                var usercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();



                var order = new Order();


                order.products = new List<Product>();


                foreach (var cartItem in usercart)
                {

                    order.products.Add(cartItem.Product);
                }


                context.Orders.Add(order);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);

            }


        }
        [HttpGet]
        [Route("getAllOrder")]
        public IActionResult getAllOrder()
        {

            var orders = context.Orders.Include(e => e.products).ToList();
            return Ok(orders);
        }
        [HttpGet]

        [Route("getAllVendorId")]
        // [Authorize]
        public IActionResult getAllVendorId()
        {

            var orderWithProducts = context.Orders
   .Include(o => o.products) // Include related products
       .ThenInclude(p => p.vendor)// Include related vendor for each product
   .FirstOrDefault();
            if (orderWithProducts != null && orderWithProducts.products != null)
            {
                // Iterate through each product in the order
                foreach (var product in orderWithProducts.products)
                {
                    // Access the associated vendor and get its ID
                    int vendorId = product.vendorId; // Assuming Id is the property representing the vendor ID
                    return Ok(vendorId);                   // Now you have the vendor ID associated with the product in the order
                }
            }
            return Ok();
        }

        [HttpGet]
        // [Authorize]
        public IActionResult getAllCarst()
        {

            var items = cartRepository.GetCartItems();
            return Ok(items);
        }
        [HttpDelete]
        public IActionResult DeleteCart(int id)
        {
            cartRepository.Delete(id);

            //var items = context.CartItems.ToList();
           

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


                var Newusercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();

                return Ok(Newusercart);
        }



        [HttpPut("updateQuantity")]
        public IActionResult updateQuantity(int id,int quantity)
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

            var item = context.CartItems.Find(id);
            item.Quantity = quantity;
            //var itemToUpdate = new CartItem
            //{
            //    UserId = item.UserId,
            //    ProductId = item.ProductId,
            //    Quantity = quantity
            //};

            context.CartItems.Update(item);
            context.SaveChanges();


          var Newusercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();

            return Ok(Newusercart);
        }


            [HttpPut("remove")]
        public IActionResult RemoveOneFromCart(int id)
        {

            var cartItem = context.CartItems.Find(id);



            if (cartItem.Quantity > 1)
            {

                var itemToUpdate = new CartItem
                {
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity--
                };

                context.CartItems.Update(itemToUpdate);
                context.SaveChanges();

                return Ok();
            }
            else
            {

                return Ok("No change");
            }
        }
        [HttpPut("AddOne")]
        public IActionResult AddOneFromCart(int id)
        {

            var cartItem = context.CartItems.Find(id);



            if (cartItem.Quantity >= 1)
            {

                var itemToUpdate = new CartItem
                {
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity++
                };

                context.CartItems.Update(itemToUpdate);
                context.SaveChanges();

                return Ok();
            }
            else
            {

                return Ok("No change");
            }

        }
    }
}
