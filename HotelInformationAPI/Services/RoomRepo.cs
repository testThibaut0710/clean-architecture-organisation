using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using HotelInformationAPI.Models.DTO;
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
                foreach (var item in hotel)
                {
                    if (item.Id == room.HotelId)
                    {
                        var rooms = _hotelContext.InformationsChambre.ToList();
                        foreach (var roomItem in rooms)
                        {
                            if (roomItem.HotelId == room.HotelId && roomItem.Id == room.Id)
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
                                return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
                        }
                        _hotelContext.InformationsChambre.Add(room);
                        _hotelContext.SaveChanges();
                        return room;
                    }
                }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
                return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(room);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }

        public RoomDTO Delete(RoomDTO roomDTO)
        {
            try
            {
                var hotel = _hotelContext.InformationsChambre.ToList();
                foreach (var item in hotel)
                {
                    if ((item.HotelId == roomDTO.HotelID) && (item.Id == roomDTO.RoomID))
                    {
                        _hotelContext.InformationsChambre.Remove(item);
                        RoomDTO room = new()
                        {
                            HotelID = item.HotelId,
                            RoomID = item.Id
                        };
                        _hotelContext.SaveChanges();
                        return room;
                    }
                }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
                return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(roomDTO);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
    }
}
