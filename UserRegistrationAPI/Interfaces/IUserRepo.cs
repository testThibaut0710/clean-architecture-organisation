namespace UserRegistrationAPI.Interfaces
{
    public interface IUserRepo<T,K>
    {
        T Add(T item);
        T Get(K key);
    }
}
