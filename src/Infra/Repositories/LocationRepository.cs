using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(DataContext context) : base(context)
        {
        }

        public async Task<ListDataPagination<Location>> GetAll(int page, int size, int? IBGECode, string? state, string? city, string? orderBy)
        {
            var query = BuildBaseQuery();

            ApplyIBGECodeFilter(ref query, IBGECode);
            ApplyStateFilter(ref query, state);
            ApplyCityFilter(ref query, city);

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = ApplyOrderBy(query, orderBy);
            }

            var totalItems = await query.CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / size);

            var data = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new ListDataPagination<Location>
            {
                Page = page,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Data = data
            };
        }

        private IQueryable<Location> BuildBaseQuery()
        {
            return _context.Set<Location>().AsQueryable();
        }

        private void ApplyIBGECodeFilter(ref IQueryable<Location> query, int? IBGECode)
        {
            ApplyFilterIfTrue(IBGECode > 0, x => EF.Property<int>(x, "IBGECode") == IBGECode, ref query);
        }

        private void ApplyStateFilter(ref IQueryable<Location> query, string? state)
        {
            ApplyFilterIfNotEmpty(state, x => EF.Property<string>(x, "State") != null && EF.Property<string>(x, "State").Contains(state), ref query);
        }

        private void ApplyCityFilter(ref IQueryable<Location> query, string? city)
        {
            ApplyFilterIfNotEmpty(city, x => EF.Property<string>(x, "City") != null && EF.Property<string>(x, "City").Contains(city), ref query);
        }

        private void ApplyFilterIfNotEmpty(string? value, Expression<Func<Location, bool>> filter, ref IQueryable<Location> query)
        {
            if (!string.IsNullOrEmpty(value))
            {
                query = query.Where(filter);
            }
        }

        private void ApplyFilterIfTrue(bool condition, Expression<Func<Location, bool>> filter, ref IQueryable<Location> query)
        {
            query = query.Where(condition ? filter : x => true);
        }

        private IQueryable<Location> ApplyOrderBy(IQueryable<Location> query, string orderBy)
        {
            var parameter = Expression.Parameter(typeof(Location), "x");
            var property = Expression.Property(parameter, orderBy.Split('_')[0]);
            var lambda = Expression.Lambda(property, parameter);

            return orderBy.EndsWith("_desc") ? Queryable.OrderByDescending(query, (dynamic)lambda) : Queryable.OrderBy(query, (dynamic)lambda);
        }
    }
}
