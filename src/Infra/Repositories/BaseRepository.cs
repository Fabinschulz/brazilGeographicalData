using BrazilGeographicalData.src.Domain.Common;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                entity.IsDeleted = true;

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                await Task.CompletedTask;
                return true;
            }

            return false;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
