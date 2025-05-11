using Social_Network_API.Tools;
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
using Microsoft.Extensions.FileProviders;

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

            // Add services to the container.

            builder.Services.AddControllers();

            // YOUR CONNECTION STRING FOR DB
            string connectionString = config.GetValue<string>("connectionString");

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication();
            builder.Services.AddSingleton(config);
            builder.Services.AddDbContext<MyDBContext>(
                options => options.UseMySQL(connectionString)
            );
            ;
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tempkey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:Key"))
                    );
                    // tempkey.KeySize = 256;
                    options.TokenValidationParameters = new()
                    {
                        RequireExpirationTime = true,
                        LifetimeValidator = (before, expires, token, parameters) =>
                        {
                            return expires > DateTime.UtcNow;
                        },
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = tempkey
                    };
                });

            builder.Services.AddMediatR(typeof(Program));
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")
                    ),
                    RequestPath = "/files"
                }
            );
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader());

            
            
            app.Run();
        }
    }
}
