using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;

namespace HotelInformationAPI.Services
{
    public class HotelSummaryService : IHotelSummaryService<Hotel, string, double, int>
    {
        private readonly IRepo<Hotel, int> _depotHotel;

        public HotelSummaryService(IRepo<Hotel, int> depotHotel)
        {
            _depotHotel = depotHotel;
        }

        public ICollection<Hotel> GetAll()
        {
            var hotels = _depotHotel.GetAll();
            if (hotels != null)
                return hotels;

            return null;

        }

        public ICollection<Hotel> GetByLocation(string localisation)
        {
            List<Hotel> hotels = new();
            var hotel = _depotHotel.GetAll();
            if (hotel != null)
            {
                foreach (var item in hotel)
                {
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
                    if (item.City.ToLower() == localisation.ToLower())
                    {
                        hotels.Add(item);
                    }
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
                }
                return hotels;
            }

            return null;

        }

        public ICollection<Hotel> GetByPriceRange(double gammePrix)
        {
            List<Hotel> hotels = new();
            var hotel = _depotHotel.GetAll();
            if (hotel != null)
            {
                foreach (var item in hotel)
                {
                    if (gammePrix <= item.Price)
                    {
                        hotels.Add(item);
                    }
                }
                return hotels;
            }

            return null;

        }

        public int GetCount(string nomHotel)
        {
            var hotels = _depotHotel.GetAll();
            if (hotels != null)
            {
                foreach (var item in hotels)
                {
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
                    if (item.Name.ToLower() == nomHotel.ToLower())
                    {
                        return item.NumberOfRooms;
                    }
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
                }
            }
            return 0;
        }
    }
}
