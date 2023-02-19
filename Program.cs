using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Social_Network_API.Models;
using System.Configuration;
using Social_Network_API.Database;
using Microsoft.EntityFrameworkCore;

namespace Social_Network_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            var logger = new Logger();
            // Add services to the container.
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            string connectionString = "server=localhost;user=root;password=root;database=societydb;port=3306";
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MyDBContext>(options => options.UseMySQL(connectionString));;
            
            var app = builder.Build();
            
            
            

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
            
            app.Run();
        }
    }
}