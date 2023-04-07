using Social_Network_API.Models;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Social_Network_API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using Social_Network_API.Common.Behaviors;
using Social_Network_API.Common.Middlewares;

namespace Social_Network_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            var builder = WebApplication.CreateBuilder(args);
            var logger = new Logger();
            // Add services to the container.
            
            builder.Services.AddControllers();
            
            // YOUR CONNECTION STRING FOR DB
            string connectionString = "server=localhost;user=root;password=root;database=societydb;port=3306";
            

            
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication();
            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddDbContext<MyDBContext>(options => options.UseMySQL(connectionString));;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var tempkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:Key")));
                // tempkey.KeySize = 256;
                options.TokenValidationParameters = new()
                {
                    RequireExpirationTime = true,
                    LifetimeValidator = (before, expires, token, parameters) =>
                    {return expires > DateTime.UtcNow;},
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    /*ValidIssuer = config.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = config.GetValue<string>("Jwt:Audience"),*/
                    IssuerSigningKey = tempkey
                };
            });
            
            builder.Services.AddMediatR(typeof(Program));
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            var app = builder.Build();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            


            
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                logger.AddCountOfRequest();
                Console.WriteLine($"count of request:{logger.countOfRequests}");
                
                await next.Invoke(context);
            });
            app.MapControllers();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyHeader());
            
            app.Run();
        }
    }
}