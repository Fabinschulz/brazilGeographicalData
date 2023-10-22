using BrazilGeographicalData.src.Domain.Common;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<ListDataPagination<T>> GetAll(int page, int size, string? searchString, string? email, bool isDeleted, string? orderBy);
        Task<T> GetById(Guid id);
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id, T entity);
    }
}
