using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HotelInformationAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Diagnostics;
using HotelInformationAPI;

namespace TestIntegration
{
    [TestClass]
    public class HotelControllerIntegrationTests
    {
        private static CustomWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f1e2d3c4b5a697867564738291a3b2c1")); // Replace with your actual secret key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "staff")
            };

            var token = new JwtSecurityToken(
                issuer: "your_issuer",  // Replace with your issuer
                audience: "your_audience",  // Replace with your audience (if needed)
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Set a suitable expiration time
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [TestInitialize]
        public void TestInit()
        {
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task AddHotel_Success_ReturnsOk()
        {
            // Arrange
            var hotel = new Hotel
            {
                Id=105,
                Name = "Hôtel Test",
                Description = "Description de l'hôtel Test. Cet hôtel est destiné aux tests d'intégration.",
                Address = "123 Rue Test",
                ContactNumber = "0123456789",
                City = "TestCity",
                Country = "TestCountry",
                AverageRating = 4.5,
                NumberOfRooms = 50,
                Price = 100.00
            };

            var token = GenerateJwtToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/GestionHotel/AddHotel", content);
            Debug.WriteLine(response);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Hotel Information Successfully Added!", responseString);
        }

        [TestMethod]
        public async Task ModificationHotel_Success_ReturnsOk()
        {
            // Arrange
            var hotel = new Hotel
            {
                Id = 105,
                Name = "Nouveau nom d'hôtel",
                Description = "Description mise à jour de l'hôtel. Cet hôtel a été récemment rénové.",
                Address = "456 Rue Mise à Jour",
                ContactNumber = "9876543210",
                City = "NewCity",
                Country = "NewCountry",
                AverageRating = 4.0,
                NumberOfRooms = 60,
                Price = 150.00
            };
            var token = GenerateJwtToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/GestionHotel/PutHotel", content);
       
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Hotel Information Successfully Updated!", responseString);
        }

        [TestMethod]
        public async Task RemoveHotel_Success_ReturnsOk()
        {
            // Arrange
            int hotelId = 105;

            // Act
            var token = GenerateJwtToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"/api/GestionHotel/DeleteHotel?HotelID={hotelId}");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("Hotel Information Successfully Deleted!", responseString);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _client.Dispose();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
