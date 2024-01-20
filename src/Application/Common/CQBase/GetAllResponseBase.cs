using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Common.CQBase
{
    public class GetAllResponseBase<T> where T : class
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<T> Data { get; set; }

        public GetAllResponseBase(ListDataPagination<T> entity)
        {
            Page = entity.Page;
            TotalPages = entity.TotalPages;
            TotalItems = entity.TotalItems;
            Data = entity.Data;
        }
    }
}
