using BrazilGeographicalData.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrazilGeographicalData.src.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
