using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Services.PasswordService;
using BrazilGeographicalData.src.Application.Services.TokenServices;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<User> GetAuthenticatedUser(ClaimsPrincipal user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
        }

        public async Task<LoggedUser> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);
            var hashedPassword = PasswordService.HashPassword(password);
            ValidateUserForLogin(user, hashedPassword);

            var token = TokenService.GenerateToken(user);
            return CreateLoggedUser(user, token);
        }

        private async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new NotFoundException("Usuário não encontrado.");
            }

            return user;
        }

        private void ValidateUserForLogin(User user, string password)
        {

            if (!PasswordService.VerifyPasswordHash(password, user.Password))
                throw new BadRequestException(new[] { "Senha inválida." });
        }
        private LoggedUser CreateLoggedUser(User user, string token)
        {
            return new LoggedUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token,
            };
        }

        public async Task<ListDataPagination<User>> GetAll(int page, int size, string? username, string? email, bool isDeleted, string? orderBy, string? role)
        {
            var query = BuildBaseQuery();

            ApplyUsernameFilter(ref query, username);
            ApplyEmailFilter(ref query, email);
            ApplyIsDeletedFilter(ref query, isDeleted);
            ApplyRoleFilter(ref query, role);

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = ApplyOrderBy(query, orderBy);
            }

            var totalItems = await query.CountAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / size);

            var data = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new ListDataPagination<User>
            {
                Page = page,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Data = data
            };
        }

        private IQueryable<User> BuildBaseQuery()
        {
            return _context.Set<User>().AsQueryable();
        }

        private void ApplyUsernameFilter(ref IQueryable<User> query, string? username)
        {
            ApplyFilterIfNotEmpty(username, x => EF.Property<string>(x, "Username") != null && EF.Property<string>(x, "Username").Contains(username), ref query);
        }

        private void ApplyEmailFilter(ref IQueryable<User> query, string? email)
        {
            ApplyFilterIfNotEmpty(email, x => EF.Property<string>(x, "Email") != null && EF.Property<string>(x, "Email").Contains(email), ref query);
        }

        private void ApplyIsDeletedFilter(ref IQueryable<User> query, bool isDeleted)
        {
            ApplyFilterIfTrue(isDeleted, x => EF.Property<bool>(x, "IsDeleted") == isDeleted, x => EF.Property<bool?>(x, "IsDeleted") == false || EF.Property<bool?>(x, "IsDeleted") == null, ref query);
        }

        private void ApplyRoleFilter(ref IQueryable<User> query, string? role)
        {
            ApplyFilterIfNotEmpty(role, x => EF.Property<string>(x, "Role") != null && EF.Property<string>(x, "Role").Contains(role), ref query);
        }

        private void ApplyFilterIfNotEmpty(string? value, Expression<Func<User, bool>> filter, ref IQueryable<User> query)
        {
            if (!string.IsNullOrEmpty(value))
            {
                query = query.Where(filter);
            }
        }

        private void ApplyFilterIfTrue(bool condition, Expression<Func<User, bool>> filterTrue, Expression<Func<User, bool>> filterFalse, ref IQueryable<User> query)
        {
            query = query.Where(condition ? filterTrue : filterFalse);
        }

        private IQueryable<User> ApplyOrderBy(IQueryable<User> query, string orderBy)
        {
            var parameter = Expression.Parameter(typeof(User), "x");
            var property = Expression.Property(parameter, orderBy.Split('_')[0]);
            var lambda = Expression.Lambda(property, parameter);

            return orderBy.EndsWith("_desc") ? Queryable.OrderByDescending(query, (dynamic)lambda) : Queryable.OrderBy(query, (dynamic)lambda);
        }

    }
}
