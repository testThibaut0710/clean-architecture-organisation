namespace HotelInformationAPI.Interface
{
    public interface IRoomRepo<T,K>
    {
        T Add(T item);
        K Delete(K item);
    }
}
