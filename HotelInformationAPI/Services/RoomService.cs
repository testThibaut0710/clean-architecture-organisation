using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using HotelInformationAPI.Models.DTO;

namespace HotelInformationAPI.Services
{
    public class RoomService:IRoomService<Room,RoomDTO>
    {
        private readonly IRoomRepo<Room, RoomDTO> _roomRepo;

        public RoomService(IRoomRepo<Room,RoomDTO> roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public Room Add(Room item)
        {
            var add = _roomRepo.Add(item);
            if(add != null)
                return item;
            return null;
        }

        public RoomDTO Delete(RoomDTO item)
        {
            var delete = _roomRepo.Delete(item);
            if (delete != null)
                return item;
            return null;
        }
    }
}
