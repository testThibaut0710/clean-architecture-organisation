using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using HotelInformationAPI.Models.DTO;
using HotelInformationAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelInformationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<HotelContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });
            builder.Services.AddScoped<IRepo<Hotel, int>, HotelRepo>();
            builder.Services.AddScoped<IService<Hotel, int>, HotelService>();
            builder.Services.AddScoped<IRoomRepo<Room,RoomDTO>, RoomRepo>();
            builder.Services.AddScoped<IRoomService<Room, RoomDTO>, RoomService>();
            builder.Services.AddScoped<IHotelSummaryService<Hotel, string, double, int>, HotelSummaryService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}