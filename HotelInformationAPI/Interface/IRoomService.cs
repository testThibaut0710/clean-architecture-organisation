namespace HotelInformationAPI.Interface
{
    public interface IRoomService<T,K>
    {
        T Add(T item);
        K Delete(K item);
    }
}
