using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HotelInformationAPI;
using HotelInformationAPI.Models;
using System;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<HotelContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add DbContext using an in-memory database for testing.
            services.AddDbContext<HotelContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryHotelTest");
            });

            // Build the service provider.
            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (HotelContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HotelContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    db.InformationsHotel.Add(new Hotel
                    {
                        Id = 101,
                        Name = "Grand Hotel Luxueux",
                        Description = "Un hôtel luxueux offrant un séjour mémorable.",
                        Address = "123 Rue test",
                        ContactNumber = "0467000000",
                        City = "Montpellier",
                        Country = "FRANCE",
                        AverageRating = 1,
                        NumberOfRooms = 10,
                        Price = 50,
                    });
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                }
            }
        });
    }
}
