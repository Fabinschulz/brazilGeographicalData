using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Services.PasswordService;
using BrazilGeographicalData.src.Application.Services.TokenServices;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
                throw new NotFoundException("Usuário não encontrado.");

            return user;
        }

        private void ValidateUserForLogin(User user, string password)
        {

            if (!PasswordService.VerifyPasswordHash(password, user.Password))
                throw new BadRequestException("Senha inválida.");
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

    }
}
