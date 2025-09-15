using Catalog.Domain._01.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Catalog.QueriesService
{
    public class TokenJWT : ITokenJWT
    {

        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenJWT> _logger;

        public TokenJWT(IOptions<JwtSettings> jwtSettings, ILogger<TokenJWT> logger)
        {

            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }
                

        public string GenerateToken(Usuario user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            try
            {
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.or_id_usuario.ToString()),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.or_nombre),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                        Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generando JWT para usuario {UserId}", user.or_id_usuario);
                throw;
            }
        }
    }
}

