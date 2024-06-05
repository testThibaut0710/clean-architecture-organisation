using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoomReservationAPI.Interfaces;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;
using RoomReservationAPI.Services;
using System.Text;

namespace RoomReservationAPI
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
            builder.Services.AddDbContext<ReservationContext>(opts =>
            {
                opts.UseMySQL(builder.Configuration.GetConnectionString(builder.Configuration["DB_STRING_SECRET"]));
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
            builder.Services.AddScoped<IReserveRepo<Reservation, ReservationDTO>, ReserveRepo>();
            builder.Services.AddScoped<IReserveService<Reservation, ReservationDTO>, ReserveService>();
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