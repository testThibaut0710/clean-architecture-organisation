using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoomReservationAPI.Interfaces;
using RoomReservationAPI.Models;
using RoomReservationAPI.Models.DTO;
using System.Diagnostics;

namespace RoomReservationAPI.Services
{
    public class ReserveRepo : IReserveRepo<Reservation, ReservationDTO>
    {
        private readonly ReservationContext _reservationContext;

        public ReserveRepo(ReservationContext reservationContext)
        {
            _reservationContext = reservationContext;
        }
        public Reservation Book(Reservation item)
        {
            try
            {
                var reserveInfo = _reservationContext.ReservationInformation.ToList();
                if(reserveInfo != null) 
                { 
                    foreach(var reservation in reserveInfo)
                    {
                        if (reservation.HotelId == item.HotelId && reservation.RoomId == item.RoomId)
                            return null;
                    }
                    _reservationContext.ReservationInformation.Add(item);
                    _reservationContext.SaveChanges();
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(item);
            }
            return null;
        }

        public ReservationDTO Cancel(ReservationDTO item)
        {
            try
            {
                var info = _reservationContext.ReservationInformation.FirstOrDefault(id => id.HotelId == item.HotelID && id.RoomId == item.RoomID);
                if(info != null)
                {
                    ReservationDTO reservationDTO = new ReservationDTO();
                    reservationDTO.HotelID = info.HotelId;
                    reservationDTO.RoomID = info.RoomId;
                    _reservationContext.ReservationInformation.Remove(info);
                    _reservationContext.SaveChanges();
                    return reservationDTO;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(item);
            }
            return null;
        }
    }
}
