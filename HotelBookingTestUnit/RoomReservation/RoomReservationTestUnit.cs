using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomReservationAPI.Controllers;
using RoomReservationAPI.Interfaces;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;
using System.Diagnostics;

namespace HotelBookingTestUnit.RoomReservation
{
    [TestClass]
    public class GestionReservationTests
    {
        private Mock<IReserveService<Reservation, ReservationDTO>> _mockService;
        private GestionReservation _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IReserveService<Reservation, ReservationDTO>>();
            _controller = new GestionReservation(_mockService.Object);
        }

        [TestMethod]
        public void Book_SuccessfulReservation_ReturnsOkResult()
        {
            // Arrange
            var reservation = new Reservation
            {
                ReservationId = 1,
                UserName = "JohnDoe",
                HotelId = 100,
                RoomId = 200,
                CheckInDate = new DateTime(2024, 6, 1),
                CheckOutDate = new DateTime(2024, 6, 5)
            };
            _mockService.Setup(service => service.Book(It.IsAny<Reservation>())).Returns(reservation);

            // Act
            var result = _controller.Book(reservation).Result as OkObjectResult;
            Debug.WriteLine("coucou");
            Debug.WriteLine(result.StatusCode);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual("Reservation Successfull!", result.Value);

        }

        [TestMethod]
        public void Book_UnsuccessfulReservation_ReturnsBadRequest()
        {
            // Arrange
            var reservation = new Reservation
            {
                ReservationId = 1,
                UserName = "JohnDoe",
                HotelId = 100,
                RoomId = 200,
                CheckInDate = new DateTime(2024, 6, 1),
                CheckOutDate = new DateTime(2024, 6, 5)
            };
            _mockService.Setup(service => service.Book(It.IsAny<Reservation>())).Returns((Reservation)null);

            // Act
            var result = _controller.Book(reservation).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Unable To Make A Reservation", result.Value);
        }
        [TestMethod]
        public void Cancel_SuccessfulCancellation_ReturnsOkResult()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                HotelID = 100,
                RoomID = 200
            };
            var expectedResult = new ReservationDTO
            {
                HotelID = 100,
                RoomID = 200
            };
            _mockService.Setup(service => service.Cancel(reservationDTO)).Returns(expectedResult);

            // Act
            var result = _controller.Cancel(reservationDTO).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual("Cancellation Successfull!", result.Value);
        }

        [TestMethod]
        public void Cancel_UnsuccessfulCancellation_ReturnsBadRequest()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                HotelID = 100,
                RoomID = 200
            };
            _mockService.Setup(service => service.Cancel(reservationDTO)).Returns((ReservationDTO)null);

            // Act
            var result = _controller.Cancel(reservationDTO).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.AreEqual("Reservation Cannot Be Cancelled", result.Value);
        }
    }
}
