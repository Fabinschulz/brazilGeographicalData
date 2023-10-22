namespace BrazilGeographicalData.src.Domain.Entities
{
    public class ListDataPagination<T>
    {
        public int Page { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
        public List<T> Data { get; set; } = new List<T>();
    }
}
