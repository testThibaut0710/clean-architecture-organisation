using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using HotelInformationAPI.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace HotelInformationAPI.Services
{
    public class RoomRepo : IRoomRepo<Room, RoomDTO>
    {
        private readonly HotelContext _hotelContext;

        public RoomRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public Room Add(Room room)
        {
            try
            {
                var hotel = _hotelContext.InformationsHotel.ToList();
                foreach(var item in hotel)
                {
                    if (item.Id == room.HotelId)
                    {
                        var rooms = _hotelContext.InformationsChambre.ToList();
                        foreach(var roomItem in rooms)
                        {
                            if (roomItem.HotelId == room.HotelId && roomItem.Id == room.Id)
                                return null;
                        }
                        _hotelContext.InformationsChambre.Add(room);
                        _hotelContext.SaveChanges();
                        return room;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(room);
            }
            return null;
        }

        public RoomDTO Delete(RoomDTO roomDTO)
        {
            try
            {
                var hotel = _hotelContext.InformationsChambre.ToList();
                foreach (var item in hotel)
                {
                    if((item.HotelId == roomDTO.HotelID)&&(item.Id == roomDTO.RoomID))
                    {
                        _hotelContext.InformationsChambre.Remove(item);
                        RoomDTO room = new RoomDTO();
                        room.HotelID = item.HotelId;
                        room.RoomID = item.Id;
                        _hotelContext.SaveChanges();
                        return room;
                    }   
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(roomDTO);
            }
            return null;
        }
    }
}
