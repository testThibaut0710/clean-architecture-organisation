using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using HotelInformationAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace HotelInformationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "staff")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService<Room, RoomDTO> _roomService;
        public RoomController(IRoomService<Room,RoomDTO> roomService)
        {
             _roomService = roomService;
        }
        [HttpPost("AddRoomInfos")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Room> Add(Room room)
        {
            var roomAddResult = _roomService.Add(room);
            if (roomAddResult != null)
                return Ok("Les informations ont été correctement ajoutées!");
            return BadRequest("Une erreur s'est produite");
        }
        [HttpDelete("DeleteRoomInfos")]
        [ProducesResponseType(typeof(RoomDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoomDTO> DeleteProject(RoomDTO roomDTO)
        {
            var roomDeleteResult = _roomService.Delete(roomDTO);
            if (roomDeleteResult != null)
                return Ok("Les informations de l'hotel ont été supprimées!");
            return BadRequest("Une erreur s'est produite");
        }
    }
}
