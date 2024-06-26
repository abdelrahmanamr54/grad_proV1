﻿using grad_proV1.Data;
using grad_proV1.Dtos;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
                var usercart1 = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList().Count();
                if (addedToCart)
                {
                    var response = JsonConvert.SerializeObject(new { message = "Product added to cart", productId = productId ,usercart1});
                    return Ok(response);
                }


                 return Ok("Product successfully added to cart.");
               // return Ok(new {Msg= "Product successfully added to cart." });
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

                var usercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId ).ToList();
              //  var usercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();

               var usercart1 = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId ).ToList().Count();

                //var order = new Order();


                //order.products = new List<Product>();


                //foreach (var cartItem in usercart)
                //{
                    
                //    order.products.Add(cartItem.Product);
                //}


                //context.Orders.Add(order);
                //context.SaveChanges();

                //var ordersv1 = context.Orders.Select(e => e.products);

               double totalPrice = usercart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);


                return Ok(new
                {
                    list = usercart,
                    usercart1,
                    totalPrice


                });

                // return Ok(usercart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);

            }
        }
        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> checkout( string secNum,string address)
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
                Order order = new Order
                {
                    UserId = userId,
                     OrderDate = DateTime.Now,

                     OptionalNum=secNum,
                     UserAddress=address,
                    OrderItems = usercart.Select(ci => new OrderItem
                    {
                        Product=ci.Product,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity
                    }).ToList()
                };

                context.Orders.Add(order);
              


              
                context.CartItems.RemoveRange(usercart);
                await context.SaveChangesAsync();
            
                //  context.SaveChanges();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);

            }



        }
        [HttpGet]
        [Route("DisplayAllCartItems")]
        public ActionResult DisplayAllCartItems()
        {
            var orders = context.Orders
                                 .Include(o => o.OrderItems)
                                 .ToList();

            var viewModel = orders.Select(order => new OrderDTO
            {
                OrderId = order.Id,
                OrderItems= order.OrderItems
            }).ToList();

            return Ok();
        }                                                      
        [HttpGet]
        [Route("getAllOrder")]
        public IActionResult getAllOrder()
        {

            //var orders = context.Orders.Include(e => e.CartItems).ToList();
            return Ok();
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
        //[HttpGet]
        //[Route("getAllorder")]

        //public async Task<IActionResult> getAllorder()
        //{
        //    var orders = await context.Orders
        //                              .Include(o => o.products)
        //                              .ToListAsync();

        //    //var viewModel = new OrderViewModel
        //    //{
        //    //    Orders = orders
        //    //};

        //    return Ok(orders);
        //}
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

           // cartRepository.Delete(id);
            var Newusercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();
            var usercart1 = context.CartItems.Where(e => e.UserId == userId).ToList().Count();
            double totalPrice = Newusercart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);
            //  return Ok(Newusercart);
            return Ok(new
            {
              list=  Newusercart,
                usercart1,
                totalPrice

            });
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
           

            context.CartItems.Update(item);
            context.SaveChanges();

            var Newusercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId && !e.IsDeleted).ToList();
            
            
            var usercart1 = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId && !e.IsDeleted).ToList().Count();
            double totalPrice = Newusercart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);
            return Ok(new
            {
                list = Newusercart,
                usercart1,
                totalPrice


            });

         
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
        [HttpGet]
        [Route("GetUserOrder")]

        public IActionResult GetUserOrder()
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

                var usercart = context.Orders.Include(e => e.OrderItems).ThenInclude(o=>o.Product).Where(e => e.UserId == userId).ToList();
                //var usercart = context.CartItems.Include(e => e.Product).Where(e => e.UserId == userId).ToList();



                var usercart1 = context.Orders.Include(e => e.products).Where(e => e.UserId == userId).ToList().Count();

              


                var ordersv1 = context.Orders.Select(e => e.products);

               


                return Ok(
                
                usercart
               


                );

               
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);

            }
        }

        [HttpGet]
        [Route("GetUserOrderById")]
        public IActionResult GetUserOrderById(int id)
        {

            var selectedOrder = context.Orders.Include(e => e.OrderItems).ThenInclude(o => o.Product).Where(e=>e.Id==id);
        //    double totalPrice = selectedOrder.Sum(cartItem => cartItem.OrderItems.p * cartItem.Quantity);

            return Ok(selectedOrder);
        }


        }

}
