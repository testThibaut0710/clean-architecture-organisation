namespace RoomReservationAPI.Interfaces
{
    public interface IReserveService<T,K>
    {
        T Book(T item);
        K Cancel(K item);
    }
}
