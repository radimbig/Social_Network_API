using Microsoft.Extensions.Logging;
using Social_Network_API.Models;
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
            
            builder.Services.AddSwaggerGen();

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
                context.Items["logger"] = logger;
                await next.Invoke(context);
            });
            app.MapControllers();
            
            app.Run();
        }
    }
}