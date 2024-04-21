using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HotelInformationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HotelFilterController : ControllerBase
    {
        private readonly IHotelSummaryService<Hotel, string, double, int> _service;
        public HotelFilterController(IHotelSummaryService<Hotel,string,double,int> service)
        {
            _service = service;
        }
        [HttpGet("GetAllHotels")]
        [ProducesResponseType(typeof(ICollection<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ICollection<Hotel>> FetchAllHotels()
        {
            var hotels = _service.GetAll();
            if (hotels != null)
                return Created("Hotels List", hotels);
            return BadRequest("Oops! Pas d'hotel disponible.");
        }
        [HttpGet("GetAllHotelsByLocation")]
        [ProducesResponseType(typeof(ICollection<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ICollection<Hotel>> FetchByLocation(string location)
        {
            var hotels = _service.GetByLocation(location);
            if (hotels != null)
                return Created("Hotels List", hotels);
            return BadRequest("Oops! Pas d'hotel disponible à cet endroit.");
        }

        [HttpGet("GetHotelsByPrice")]
        [ProducesResponseType(typeof(ICollection<Hotel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ICollection<Hotel>> FetchByPriceRange(double minRange)
        {
            var hotels = _service.GetByPriceRange(minRange);
            if (hotels != null)
                return Created("Hotels List", hotels);
            return BadRequest("Oops! Pas d'hotel disponible à ce prix.");
        }

        [HttpGet("GetAvailableRooms")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> FetchCount(string hotelName)
        {
            var hotelCount = _service.GetCount(hotelName);
            if (hotelCount != 0)
                return Created("Available Rooms Count", hotelCount);
            return BadRequest("Oops! Pas de chambre disponible.");
        }
    }
}
