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
        private Mock<IService<Hotel, int>> _mockService;
        private HotelController _controller;

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
                Id=2,
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
            _mockService.Setup(service => service.Add(It.IsAny<Hotel>())).Returns((Hotel)null);

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
            _mockService.Setup(service => service.Update(It.IsAny<Hotel>())).Returns((Hotel)null);

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
            _mockService.Setup(service => service.Delete(hotelId)).Returns((Hotel)null);

            // Act
            var result = _controller.Delete(hotelId).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Information Could Not Be Deleted", result.Value);
        }
    }
}
