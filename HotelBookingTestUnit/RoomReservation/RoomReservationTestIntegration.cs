using HotelInformationAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoomReservationAPI;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestIntegration
{
    [TestClass]
    public class RoomReservationTestIntegration
    {
        private static CustomWebApplicationFactoryRoomReservation<Program> _factory;
        private HttpClient _client;

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

        [TestMethod]
        public async Task Cancel_ValidReservationDTO_ReturnsOk()
        {
            Book_ValidReservation_ReturnsOk();
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                HotelID = 101,
                RoomID = 201,
            };

            var token = GenerateJwtToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(reservationDTO), Encoding.UTF8, "application/problem+json");

            // Act
            var res = await content.ReadAsStringAsync();
            Debug.WriteLine(res);
            var response = await _client.DeleteAsync($"/api/GestionReservation/SupprimerReservation?reservationDTO={content}");
            Debug.WriteLine($"Response: {response}");
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Cancellation Successfull!", responseString);
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
