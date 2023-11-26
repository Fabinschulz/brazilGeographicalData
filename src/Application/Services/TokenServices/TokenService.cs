using BrazilGeographicalData.src.Application.Services.Extensions;
using BrazilGeographicalData.src.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrazilGeographicalData.src.Application.Services.TokenServices
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            //Estancia do manipulador de Token
            var tokenHandler = new JwtSecurityTokenHandler();
            //Chave da classe SSettings. O Token Handler espera um Array de Bytes, por isso é necessário converter
            var key = Encoding.ASCII.GetBytes(Settings.JwtKey);

            var claims = user.GetClaims();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Claims que vão compor o token
                Expires = DateTime.UtcNow.AddHours(8),
                //Assinatura do token, serve para identificar quem mandou o token e garantir que o token não foi alterado no meio do caminho.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Gerando o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
