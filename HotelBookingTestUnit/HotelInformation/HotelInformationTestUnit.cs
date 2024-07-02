using HotelInformationAPI.Controllers;
using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestUnitaire
{
    [TestClass]
    public class HotelControllerTests
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private Mock<IService<Hotel, int>> _mockService;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private HotelController _controller;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IService<Hotel, int>>();
            _controller = new HotelController(_mockService.Object);
        }

        [TestMethod]
        public void Add_SuccessfulAdd_ReturnsOkResult()
        {
            // Arrange
            var hotel = new Hotel
            {
                Id = 2,
                Name = "Hôtel Test",
                Description = "Description de l'hôtel Test. Cet hôtel est destiné aux tests unitaires.",
                Address = "123 Rue Test",
                ContactNumber = "0123456789",
                City = "TestCity",
                Country = "TestCountry",
                AverageRating = 4.5,
                NumberOfRooms = 50,
                Price = 100.00
            };
            _mockService.Setup(service => service.Add(It.IsAny<Hotel>())).Returns(hotel);

            // Act
            var result = _controller.Add(hotel).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual("Hotel Information Successfully Added!", result.Value);
        }

        [TestMethod]
        public void Add_UnsuccessfulAdd_ReturnsBadRequest()
        {
            // Arrange
            var hotel = new Hotel
            {
                Id = 2,
                Name = "Hôtel Test",
                Description = "Description de l'hôtel Test. Cet hôtel est destiné aux tests unitaires.",
                Address = "123 Rue Test",
                ContactNumber = "0123456789",
                City = "TestCity",
                Country = "TestCountry",
                AverageRating = 4.5,
                NumberOfRooms = 50,
                Price = 100.00
            };
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            _mockService.Setup(service => service.Add(It.IsAny<Hotel>())).Returns((Hotel)null);
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

            // Act
            var result = _controller.Add(hotel).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Information Could Not Be Added!", result.Value);
        }

        [TestMethod]
        public void Update_SuccessfulUpdate_ReturnsOkResult()
        {
            // Arrange
            var hotel = new Hotel
            {
                Id = 2,
                Name = "Nouveau hotel Test",
                Description = "Description de l'hôtel Test. Cet hôtel est destiné aux tests unitaires.",
                Address = "123 Rue Test",
                ContactNumber = "0123456789",
                City = "TestCity",
                Country = "TestCountry",
                AverageRating = 4.5,
                NumberOfRooms = 50,
                Price = 100.00
            };
            _mockService.Setup(service => service.Update(It.IsAny<Hotel>())).Returns(hotel);

            // Act
            var result = _controller.Update(hotel).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual("Hotel Information Successfully Updated!", result.Value);
        }

        [TestMethod]
        public void Update_UnsuccessfulUpdate_ReturnsBadRequest()
        {
            // Arrange
            var hotel = new Hotel();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            _mockService.Setup(service => service.Update(It.IsAny<Hotel>())).Returns((Hotel)null);
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

            // Act
            var result = _controller.Update(hotel).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Information Could Not Be Updated!", result.Value);
        }

        [TestMethod]
        public void Delete_SuccessfulDelete_ReturnsOkResult()
        {
            // Arrange
            var hotel = new Hotel();
            int hotelId = 2;
            _mockService.Setup(service => service.Delete(hotelId)).Returns(hotel);

            // Act
            var result = _controller.Delete(hotelId).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual("Hotel Information Successfully Deleted!", result.Value);
        }

        [TestMethod]
        public void Delete_UnsuccessfulDelete_ReturnsBadRequest()
        {
            // Arrange
            int hotelId = 1;
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            _mockService.Setup(service => service.Delete(hotelId)).Returns((Hotel)null);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.

            // Act
            var result = _controller.Delete(hotelId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Information Could Not Be Deleted", result.Value);
        }
    }
}
