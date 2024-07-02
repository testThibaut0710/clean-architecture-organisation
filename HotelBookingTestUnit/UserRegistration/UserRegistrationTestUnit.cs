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
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private Mock<IService<UserRegisterDTO, UserDTO>> _mockService;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private UserRegistrationController _controller;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

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
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            _mockService.Setup(service => service.Register(It.IsAny<UserRegisterDTO>())).Returns((UserDTO)null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.

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
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            _mockService.Setup(service => service.Login(It.IsAny<UserDTO>())).Returns((UserDTO)null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.

            // Act
            var result = _controller.Login(userDTO).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Login ou mot de passe incorrect.", result.Value);
        }
    }
}
