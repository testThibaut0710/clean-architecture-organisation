using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserRegistrationAPI.Controllers;
using UserRegistrationAPI.Interfaces;
using UserRegistrationAPI.Models.DTO;

namespace TestUnitaire
{
    [TestClass]
    public class UserRegistrationControllerTests
    {
        private Mock<IService<UserRegisterDTO, UserDTO>> _mockService;
        private UserRegistrationController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IService<UserRegisterDTO, UserDTO>>();
            _controller = new UserRegistrationController((UserRegistrationAPI.Interfaces.IService<UserRegisterDTO, UserDTO>)_mockService.Object);
        }

        [TestMethod]
        public void Register_SuccessfulRegistration_ReturnsOkResult()
        {
            // Arrange
            var userDTO = new UserRegisterDTO
            {
                UserName = "john_doe",
                PasswordClear = "password1234" // Au moins 8 caractères
            };
            var expectedResult = new UserDTO
            {
                UserName = "john_doe",
                Password = "password1234" // Au moins 8 caractères
            };
            _mockService.Setup(service => service.Register(It.IsAny<UserRegisterDTO>())).Returns(expectedResult);

            // Act
            var result = _controller.Register(userDTO).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void Register_UnsuccessfulRegistration_ReturnsBadRequest()
        {
            // Arrange
            var userDTO = new UserRegisterDTO
            {
                UserName = "john_doe",
                PasswordClear = "short" // Moins de 8 caractères
            };
            _mockService.Setup(service => service.Register(It.IsAny<UserRegisterDTO>())).Returns((UserDTO)null);

            // Act
            var result = _controller.Register(userDTO).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Impossible de s'enregistrer !", result.Value);
        }


        [TestMethod]
        public void Login_SuccessfulLogin_ReturnsOkResult()
        {
            // Arrange
            var userDTO = new UserDTO { /* Initialisez les propriétés appropriées */ };
            var expectedResult = new UserDTO { /* Initialisez les propriétés appropriées */ };
            _mockService.Setup(service => service.Login(It.IsAny<UserDTO>())).Returns(expectedResult);

            // Act
            var result = _controller.Login(userDTO).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(expectedResult, result.Value);
        }

        [TestMethod]
        public void Login_UnsuccessfulLogin_ReturnsBadRequest()
        {
            // Arrange
            var userDTO = new UserDTO { /* Initialisez les propriétés appropriées */ };
            _mockService.Setup(service => service.Login(It.IsAny<UserDTO>())).Returns((UserDTO)null);

            // Act
            var result = _controller.Login(userDTO).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Login ou mot de passe incorrect.", result.Value);
        }
    }
}
