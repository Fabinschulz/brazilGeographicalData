using System;
using System.Security.Cryptography;

namespace BrazilGeographicalData.src
{
    public static class Settings
    {
        public static string JwtKey { get; } = GenerateJwtKey();

        private static string GenerateJwtKey()
        {
            const int keySize = 32; // Tamanho da chave em bytes

            using (var rng = new RNGCryptoServiceProvider())
            {
                var keyBytes = new byte[keySize];
                rng.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }
    }
}
