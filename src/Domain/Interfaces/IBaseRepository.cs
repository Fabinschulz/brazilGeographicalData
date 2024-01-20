using BrazilGeographicalData.src.Domain.Common;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<ListDataPagination<T>> GetAll(int Page, int Size, string? Username, string? Email, bool IsDeleted, string? OrderBy, string Role);
        Task<T> GetById(Guid id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(Guid id);
    }
}
