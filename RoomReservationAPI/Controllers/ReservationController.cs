using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservationAPI.Interfaces;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;
namespace RoomReservationAPI.Controllers
{
    [Route("api/GestionReservation")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class GestionReservation : ControllerBase
    {
        private readonly IReserveService<Reservation, ReservationDTO> _service;
        public GestionReservation(IReserveService<Reservation, ReservationDTO> service)
        {
            _service = service;
        }
        [HttpPost("AjouterReservation")]
        [ProducesResponseType(typeof(Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Reservation> Book(Reservation reservation)
        {
            var roomReserveResult = _service.Book(reservation);
            if (roomReserveResult != null)
                return Ok("Reservation Successfull!");
            return BadRequest("Unable To Make A Reservation");
        }
        [HttpDelete("SupprimerReservation")]
        [ProducesResponseType(typeof(ReservationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReservationDTO> Cancel(ReservationDTO reservationDTO)
        {
            var roomCancelResult = _service.Cancel(reservationDTO);
            if (roomCancelResult != null)
                return Ok("Cancellation Successfull!");
            return BadRequest("Reservation Cannot Be Cancelled");
        }
    }
}
