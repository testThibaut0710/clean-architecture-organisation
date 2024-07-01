using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserRegistrationAPI.Interfaces;
using UserRegistrationAPI.Models;
using UserRegistrationAPI.Models.DTO;
using UserRegistrationAPI.Services;

namespace UserRegistrationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.AddEnvironmentVariables();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserContext>(opts =>
            {
                opts.UseMySQL(builder.Configuration["DB_STRING_SECRET"]);
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TOKEN_KEY_SECRET"])),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });
            builder.Services.AddScoped<IUserRepo<User, string>, UserRepo>();
            builder.Services.AddScoped<IService<UserRegisterDTO, UserDTO>, UserService>();
            builder.Services.AddScoped<ITokenGenerate<UserDTO, string>, TokenService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.MapControllers();

            var url = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5080";
            app.Urls.Add(url);

            app.Run();
        }
    }
}
