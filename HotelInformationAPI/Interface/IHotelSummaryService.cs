namespace HotelInformationAPI.Interface
{
    public interface IHotelSummaryService<T,S,R,I>
    {
        ICollection<T> GetAll();
        ICollection<T> GetByLocation(S location);
        ICollection<T> GetByPriceRange(R range);
        I GetCount(S hotel);
    }
}
