using BrazilGeographicalData.src.Application.Interfaces;
using BrazilGeographicalData.src.Domain.Common;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ListDataPagination<T>> GetAll(int page, int size, string? searchString, string? email, bool isDeleted, string? orderBy)
        {
            var query = _context.Set<T>().AsQueryable();

            if (!string.IsNullOrEmpty(searchString) && typeof(T).GetProperty("Name") != null)
            {
                query = query.Where(x => x.GetType().GetProperty("Name").GetValue(x).ToString().Contains(searchString));
            }

            if (!string.IsNullOrEmpty(email) && typeof(T).GetProperty("Email") != null)
            {
                query = query.Where(x => x.GetType().GetProperty("Email").GetValue(x).ToString().Contains(email));
            }

            if (isDeleted && typeof(T).GetProperty("IsDeleted") != null)
            {
                query = query.Where(x => (bool)(x.GetType().GetProperty("IsDeleted").GetValue(x) ?? false) == isDeleted);
            }


            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "email" when typeof(T).GetProperty("Email") != null:
                        query = query.OrderBy(x => typeof(T).GetProperty("Email").GetValue(x) ?? "");
                        break;
                    case "isDeleted" when typeof(T).GetProperty("IsDeleted") != null:
                        query = query.OrderBy(x => typeof(T).GetProperty("IsDeleted").GetValue(x) ?? false);
                        break;
                    default:
                        query = query.OrderBy(x => x);
                        break;
                }
            }

            var totalItems = await query.CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / size);

            var data = await query.Skip(page * size).Take(size).ToListAsync();

            return new ListDataPagination<T>
            {
                Page = page,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Data = data
            };
        }

        public async Task<T> GetById(Guid id)
        {
            var data = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<T> Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task Delete(Guid id, T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            var data = await GetById(id);
            _context.Set<T>().Remove(data);
            await Task.CompletedTask;
        }
         public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
