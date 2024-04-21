using UserRegistrationAPI.Models.DTO;

namespace UserRegistrationAPI.Interfaces
{
    public interface ITokenGenerate<T,K>
    {
        K GenerateToken(T item);
    }
}
