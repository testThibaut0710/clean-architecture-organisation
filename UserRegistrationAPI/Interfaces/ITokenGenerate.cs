namespace UserRegistrationAPI.Interfaces
{
    public interface ITokenGenerate<T, K>
    {
        K GenerateToken(T item);
    }
}
