using HotelInformationAPI.Interface;
using HotelInformationAPI.Models;

namespace HotelInformationAPI.Services
{
    public class HotelService : IService<Hotel, int>
    {
        private readonly IRepo<Hotel, int> _repo;

        public HotelService(IRepo<Hotel,int> repo) 
        { 
            _repo = repo;
        }
        public Hotel Add(Hotel hotel)
        {
            var addInfo =  _repo.Add(hotel);
            if (addInfo != null)
                return addInfo;
            return null;
        }
        public Hotel Delete(int identifiant)
        {
            var delInfo = _repo.Delete(identifiant);
            if(delInfo != null) 
                return delInfo;
            return null;
        }

        public Hotel Update(Hotel hotel)
        {
            var updateInfo = _repo.Update(hotel);
            if(updateInfo != null)
                return updateInfo;
            return null;
        }
    }
}
