using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationAPI;
using UserRegistrationAPI.Models.DTO;

namespace HotelBookingTestUnit.UserRegistration
{
    [TestClass]
    public class UserRegistrationControllerIntegrationTests
    {
        private static CustomWebApplicationFactoryUserRegistration<Program> _factory;
        private HttpClient _client;

        

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new CustomWebApplicationFactoryUserRegistration<Program>();
        }

        [TestInitialize]
        public void TestInit()
        {
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task Register_ValidUser_ReturnsOk()
        {
            var userDTO = new UserRegisterDTO
            {
                UserName = "testuser",
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                DateOfBirth = new DateTime(2000, 1, 1),
                Role = "user",
                PasswordClear = "password123" // At least 8 characters
            };

            var content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/UserRegistration/Register", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var registeredUser = JsonConvert.DeserializeObject<UserDTO>(responseString);
            Debug.WriteLine(registeredUser);
            Assert.IsNotNull(registeredUser);
            Assert.AreEqual(userDTO.UserName, registeredUser.UserName);
        }
        [TestMethod]
        public async Task Register_InvalidUser_ReturnsBadRequest()
        {
            var userDTO = new UserRegisterDTO
            {
                
            };
            // Arrange

            var content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/UserRegistration/Register", content);
            Debug.WriteLine(response);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            Register_ValidUser_ReturnsOk();
            // Arrange
            var userDTOLogin = new UserDTO
            {
                UserName = "testuser",
                Password = "password123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(userDTOLogin), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/UserRegistration/Login", content);
            Debug.WriteLine(response);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var loggedInUser = JsonConvert.DeserializeObject<UserDTO>(responseString);
            Assert.IsNotNull(loggedInUser);
            Assert.AreEqual(userDTOLogin.UserName, loggedInUser.UserName);
            // Add more assertions as needed
        }

        [TestMethod]
        public async Task Login_InvalidCredentials_ReturnsBadRequest()
        {
            Register_ValidUser_ReturnsOk();
            // Arrange
            var userDTO = new UserDTO
            {
                UserName = "testuser",
                Password = "invalidpassword" // Provide an invalid password
            };

            var content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/UserRegistration/Login", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Login ou mot de passe incorrect.", responseString);
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
