namespace HotelInformationAPI.Interface
{
    public interface IRepo<T,K>
    {
        T Add(T item);
        T Update(T item);
        T Delete(K key);
        ICollection<T> GetAll();
    }
}
