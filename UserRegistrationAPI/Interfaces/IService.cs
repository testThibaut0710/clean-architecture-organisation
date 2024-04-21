namespace UserRegistrationAPI.Interfaces
{
    public interface IService<T,K>
    {
       K Register(T details);
       K Login(K details);
       
    }
}
