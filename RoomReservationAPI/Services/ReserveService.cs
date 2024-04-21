using RoomReservationAPI.Interfaces;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;

namespace RoomReservationAPI.Services
{
    public class ReserveService : IReserveService<Reservation, ReservationDTO>
    {
        private readonly IReserveRepo<Reservation, ReservationDTO> _repo;

        public ReserveService(IReserveRepo<Reservation,ReservationDTO> repo)
        {
            _repo = repo;
        }
        public Reservation Book(Reservation item)
        {
            var book = _repo.Book(item);
            if(book != null)
                return book;
            return null;
            
        }

        public ReservationDTO Cancel(ReservationDTO item)
        {
            var cancel = _repo.Cancel(item);
            if (cancel != null)
                return cancel;
            return null;
        }
    }
}
