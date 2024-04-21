namespace RoomReservationAPI.Interfaces
{
    public interface IReserveRepo<T,K>
    {
        T Book(T item);
        K Cancel(K item);
    }
}
