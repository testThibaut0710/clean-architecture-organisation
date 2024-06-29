using RoomReservationAPI.Interfaces;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;

namespace RoomReservationAPI.Services
{
    public class ReserveService : IReserveService<Reservation, ReservationDTO>
    {
        private readonly IReserveRepo<Reservation, ReservationDTO> _repo;

        public ReserveService(IReserveRepo<Reservation, ReservationDTO> repo)
        {
            _repo = repo;
        }
        public Reservation Book(Reservation item)
        {
            var book = _repo.Book(item);
            if (book != null)
                return book;
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.

        }

        public ReservationDTO Cancel(ReservationDTO item)
        {
            var cancel = _repo.Cancel(item);
            if (cancel != null)
                return cancel;
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return null;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
    }
}
