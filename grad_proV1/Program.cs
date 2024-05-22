
using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using grad_proV1.Repository;
using gradproV1.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
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

            //var key = Encoding.ASCII.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM");

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = true;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "http://bussinesshub.runasp.net/",
            //        ValidAudience = "http://localhost:4200/",
            //        IssuerSigningKey = new SymmetricSecurityKey(key)
            //    };
            //});

            //builder.Services.AddAuthorization();


            //CROS.2
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                  //AllowAnyOrigin()
                                  policy.WithOrigins("http://localhost:4200")

                          //WithOrigins("http://localhost:4200") 
                          .AllowAnyHeader()
                       .AllowAnyMethod()
                      .AllowCredentials();
                                      //.
                                      //WithOrigins("http://localhost:49566")
                                  });
            });
        

            //public  void ConfigureServices(IServiceCollection services)
            //  {
            //      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //   {
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateIssuer = true, // Set to true if you want to validate issuer
            //           ValidateAudience = true, // Set to true if you want to validate audience
            //           ValidateLifetime = true,
            //           ValidateIssuerSigningKey = true,
            //           IssuerSigningKey = GetSigningKey(), // Replace with your signing key logic
            //           Issuer = "Your_Issuer", // Replace with the issuer string from your token
            //           Audience = "Your_Audience" // Replace with the audience string from your token
            //       };
            //   });
            //  }
            //public void Configure(IApplicationBuilder app)
            //{
            //    // Middleware configurations...

            //    // Use authentication middleware
            //    app.UseAuthentication();

            //    // Use authorization middleware
            //    app.UseAuthorization();

            //    // Other middleware configurations...
            //}
            //SymmetricSecurityKey GetSigningKey()
            //{
            //    Implement your logic to retrieve the signing key from a secure location
            //    (e.g., configuration file, environment variable)
            //    var signingKey = "Your_Signing_Key_Here"; // Replace with your actual key (in bytes)
            //    return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
            //}




            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
            //     ).AddJwtBearer(e => e.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateAudience = true,
            //         ValidateIssuer = true,
            //         ValidateLifetime = true,
            //         ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //         ValidAudience = builder.Configuration["Jwt:Audience"],
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //     });
            //builder.Services.AddAuthorization();
            // builder.Services.AddHttpContextAccessor();
            //builder.Services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("RequireUserRole",
            //          policy => policy.RequireRole("user"));
            // });


            // var key = Encoding.ASCII.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM");
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(key),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });
            //   builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true, // Set to true to validate the token issuer
            //        ValidateAudience = true, // Set to true to validate the token audience
            //        ValidateLifetime = true, // Set to true to validate the token lifetime
            //        ValidateIssuerSigningKey = true, // Set to true to validate the token signature
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), // Replace with your signing key logic
            //        ValidIssuer = "http://bussinesshub.runasp.net/", // Replace with your token issuer string
            //        ValidAudience = "http://localhost:4200/", // Replace with your token audience string
            //        ClockSkew = TimeSpan.Zero // Set to a small value (e.g., 5 minutes) to account for clock differences
            //    };

            //});
            //var key = Encoding.ASCII.GetBytes("your-secret-key"); // Replace with your secret key
            //builder.services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ClockSkew = TimeSpan.Zero
            //        };
            //    });

            //// Add authorization policy if needed
            //services.AddAuthorization();

            // Add services
            // to the container.

            builder.Services.AddControllers();
            var key = builder.Configuration["Jwt:Key"];
            //Encoding.ASCII.GetBytes("MnGsJFpXngBcaNeMSqIviSJbrOwaWM");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            builder.Services.AddAuthorization(options =>
             {
                 // options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
                 // options.AddPolicy("RequireVendorRole", policy => policy.RequireRole("Vendor"));
             });
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
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); 

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
         //   app.usetoken();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.MapControllers();

            app.Run();
        }
    }
}