namespace HotelInformationAPI.Interface
{
    public interface IService<T,K>
    {
        T Add(T item);
        T Update(T item);
        T Delete(K key);
    }
}
