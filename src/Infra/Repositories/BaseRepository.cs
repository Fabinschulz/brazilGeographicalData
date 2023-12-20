using BrazilGeographicalData.src.Domain.Common;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
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

        public async Task<ListDataPagination<T>> GetAll(int page, int size, string? username, string? email, bool isDeleted, string? orderBy, string? role)
        {
            var query = _context.Set<T>().AsQueryable();

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(x => EF.Property<string>(x, "Username") != null && EF.Property<string>(x, "Username").Contains(username));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => EF.Property<string>(x, "Email") != null && EF.Property<string>(x, "Email").Contains(email));
            }

            if (isDeleted)
            {
                query = query.Where(x => EF.Property<bool>(x, "IsDeleted") == isDeleted);
            }
            else
            {
                query = query.Where(x => EF.Property<bool?>(x, "IsDeleted") == false || EF.Property<bool?>(x, "IsDeleted") == null);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "username_asc":
                        query = query.OrderBy(x => EF.Property<string>(x, "Username"));
                        break;
                    case "username_desc":
                        query = query.OrderByDescending(x => EF.Property<string>(x, "Username"));
                        break;
                    case "email_asc":
                        query = query.OrderBy(x => EF.Property<string>(x, "Email"));
                        break;
                    case "email_desc":
                        query = query.OrderByDescending(x => EF.Property<string>(x, "Email"));
                        break;
                    case "isdeleted_asc":
                        query = query.OrderBy(x => EF.Property<bool>(x, "IsDeleted"));
                        break;
                    case "isdeleted_desc":
                        query = query.OrderByDescending(x => EF.Property<bool>(x, "IsDeleted"));
                        break;
                    case "role_asc":
                        query = query.OrderBy(x => EF.Property<string>(x, "Role"));
                        break;
                    case "role_desc":
                        query = query.OrderByDescending(x => EF.Property<string>(x, "Role"));
                        break;
                    default:
                        query = query.OrderBy(x => EF.Property<string>(x, "Username"));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(x => EF.Property<string>(x, "Role") != null && EF.Property<string>(x, "Role").Contains(role));
            }

            var totalItems = await query.CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / size);

            var data = await query.Skip((page - 1) * size).Take(size).ToListAsync();

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
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
            return entity;
        }

        public async Task Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
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
