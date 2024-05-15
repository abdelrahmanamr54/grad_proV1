
using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using grad_proV1.Repository;
using gradproV1.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace grad_proV1
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        //      private readonly IConfiguration _configuration;

        //public Program(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        var builder = WebApplication.CreateBuilder(args);
         

            //CROS.2
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      //AllowAnyOrigin()
                                      policy.WithOrigins("http://localhost:4200") 
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                      .AllowCredentials();
                                      //.
                                      //WithOrigins("http://localhost:49566")
                                  });
            });



            //void ConfigureServices(IServiceCollection services)
            //{
            //    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true, // Set to true if you want to validate issuer
            //         ValidateAudience = true, // Set to true if you want to validate audience
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = GetSigningKey(), // Replace with your signing key logic
            //         Issuer = "Your_Issuer", // Replace with the issuer string from your token
            //         Audience = "Your_Audience" // Replace with the audience string from your token
            //     };
            // });
            //}

            //SymmetricSecurityKey GetSigningKey()
            //{
            //    Implement your logic to retrieve the signing key from a secure location
            //    (e.g., configuration file, environment variable)
            //    var signingKey = "Your_Signing_Key_Here"; // Replace with your actual key (in bytes)
            //    return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
            //}

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
                ).AddJwtBearer(e => e.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<I_ProductRepositery, ProductRepositery>();
            builder.Services.AddTransient<I_ProviderRepositery, ProviderRepositery>();
            builder.Services.AddTransient<I_ServiceRepositery, ServiceRepositery>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IVendorRepository, VendorRepository>();
            builder.Services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddTransient<ICartRepository, CartRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //var app = builder.Build();

            var app = builder.Build();
            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
                context.Response.Headers.Add("Access-Control-Allow-Headers",
                "Content-Type, Authorization");
                //context.Response.Headers.Add("Access-Control-Allow-Headers",
                // "Content-Type, Authorization, x-requested-with, x-signalr-user-agent");
               context.Response.Headers.Add("Access-Control-Allow-Credentials", "true"); // Add this line
                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                    return Task.CompletedTask;
                }
                return next();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
                c.RoutePrefix = string.Empty;
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //    void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            //       {



            //   app.UseCors(x => x
            //       .AllowAnyOrigin()
            //       .AllowAnyMethod()
            //       .AllowAnyHeader());

            //           app.UseHttpsRedirection();



            //}
            //void ConfigureServices(IServiceCollection services)
            //{
            //    services.AddCors(options =>
            //    {
            //        options.AddPolicy("AllowOrigin",
            //            builder => builder.WithOrigins("http://localhost:4200")
            //            .AllowAnyHeader()
            //            .AllowAnyMethod());
            //    });

            // Other service configurations...
            // }

            //void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            //{
            //    // Other middleware configurations...

            //    app.UseCors("AllowOrigin");

            //    // Other middleware configurations...
            //}
            // static void Main(string[] args)
            //{
            //    CreateHostBuilder(args).Build().Run();
            //}

            //static IHostBuilder CreateHostBuilder(string[] args) =>
            //    Host.CreateDefaultBuilder(args)
            //        .ConfigureWebHostDefaults(webBuilder =>
            //        {
            //            webBuilder.ConfigureServices(services =>
            //            {
            //                services.AddCors(options =>
            //                {
            //                    options.AddPolicy("AllowOrigin",
            //                        builder => builder.WithOrigins("http://localhost:4200")
            //                        .AllowAnyHeader()
            //                        .AllowAnyMethod());
            //                });
            //            });

            //            webBuilder.Configure(app =>
            //            {
            //                app.UseCors("AllowOrigin");
            //                // Other middleware configurations...
            //            });
            //        });
            //void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            //{
            //    Other middleware configurations...

            //     Use authentication
            //     app.UseAuthentication();

            //    app.UseAuthorization();

            //    Other configurations...

            //    app.UseEndpoints(endpoints =>
            //    {
            //        endpoints.MapControllers();
            //    });
            //}
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});


           app.MapControllers();

            app.Run();
        }
    }
}