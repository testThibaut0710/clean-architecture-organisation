using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;
using System.Diagnostics;

namespace HotelInformationAPI.Services
{
    public class HotelRepo : IRepo<Hotel, int>
    {
        private readonly HotelContext _hotelContext;

        public HotelRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }

        public Hotel Add(Hotel hotel)
        {
            try
            {
                _hotelContext.InformationsHotel.Add(hotel);
                _hotelContext.SaveChanges();
                return hotel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(hotel);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }

        public Hotel Delete(int identifiant)
        {
            try
            {
                var hotelInfo = _hotelContext.InformationsHotel.FirstOrDefault(r => r.Id == identifiant);
                if (hotelInfo != null)
                {
                    var deleteRooms = _hotelContext.InformationsChambre.Where(r => r.HotelId == identifiant);
                    _hotelContext.InformationsChambre.RemoveRange(deleteRooms);
                    _hotelContext.SaveChanges();
                    Hotel info = new()
                    {
                        Id = hotelInfo.Id,
                        Name = hotelInfo.Name,
                        Description = hotelInfo.Description,
                        Address = hotelInfo.Address,
                        ContactNumber = hotelInfo.ContactNumber,
                        City = hotelInfo.City,
                        Country = hotelInfo.Country,
                        AverageRating = hotelInfo.AverageRating,
                        NumberOfRooms = hotelInfo.NumberOfRooms
                    };
                    _hotelContext.Remove(hotelInfo);
                    _hotelContext.SaveChanges();
                    return info;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }

        public ICollection<Hotel> GetAll()
        {
            try
            {
                var hotelInfo = _hotelContext.InformationsHotel;
                if (hotelInfo != null)
                {
                    return hotelInfo.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }

        public Hotel Update(Hotel hotel)
        {
            try
            {
                var hotelInfo = _hotelContext.InformationsHotel.FirstOrDefault(r => r.Id == hotel.Id);
                if (hotelInfo != null)
                {
                    hotelInfo.Id = hotel.Id;
                    hotelInfo.Name = hotel.Name;
                    hotelInfo.Description = hotel.Description;
                    hotelInfo.Address = hotel.Address;
                    hotelInfo.ContactNumber = hotel.ContactNumber;
                    hotelInfo.City = hotel.City;
                    hotelInfo.Country = hotel.Country;
                    hotelInfo.AverageRating = hotel.AverageRating;
                    hotelInfo.NumberOfRooms = hotel.NumberOfRooms;
                    _hotelContext.SaveChanges();
                    return hotelInfo;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(hotel);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
    }
}
