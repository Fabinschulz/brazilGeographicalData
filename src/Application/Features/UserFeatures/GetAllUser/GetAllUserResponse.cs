using BrazilGeographicalData.src.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser
{
    public sealed record GetAllUserResponse
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<User> Data { get; set; }

        public GetAllUserResponse(ListDataPagination<User> users)
        {
            Page = users.Page;
            TotalPages = users.TotalPages;
            TotalItems = users.TotalItems;
            Data = users.Data;
        }
    }
}
