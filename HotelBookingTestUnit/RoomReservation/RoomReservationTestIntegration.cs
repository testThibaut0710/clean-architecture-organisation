using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomReservationAPI;
using RoomReservationAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace TestIntegration
{
    [TestClass]
    public class RoomReservationTestIntegration
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private static CustomWebApplicationFactoryRoomReservation<Program> _factory;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private HttpClient _client;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f1e2d3c4b5a697867564738291a3b2c1")); // Replace with your actual secret key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "user")
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
            _factory = new CustomWebApplicationFactoryRoomReservation<Program>();
        }

        [TestInitialize]
        public void TestInit()
        {
            _client = _factory.CreateClient();
        }



        [TestMethod]
        public async Task Book_ValidReservation_ReturnsOk()
        {
            // Arrange
            var reservation = new Reservation
            {
                ReservationId = 1,
                UserName = "JohnDoe",
                HotelId = 101,
                RoomId = 201,
                CheckInDate = new DateTime(2024, 06, 01),
                CheckOutDate = new DateTime(2024, 06, 05)
            };
            var token = GenerateJwtToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/GestionReservation/AjouterReservation", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Reservation Successfull!", responseString);
        }

        [TestMethod]
        public async Task Book_InvalidReservation_ReturnsBadRequest()
        {
            // Arrange
            var reservation = new Reservation();
            var token = GenerateJwtToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/GestionReservation/AjouterReservation", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
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
