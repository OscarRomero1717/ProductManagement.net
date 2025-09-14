using Catalog.Domain._01.Entities;
using Catalog.Infrastructure._02.Repositories;
using Catalog.QueriesService._01.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Catalog.QueriesService
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUsuarioRepository _userRepository;

        public AuthService(IOptions<JwtSettings> jwtSettings, IUsuarioRepository userRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<AuthResultDto> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUsuarioByNombrePassword(username,password);

            if (user == null )
            {
                return new AuthResultDto { IsAuthenticated = false, Message = "Credenciales Invalidas" };
            }

            var token = GenerateJwtToken(user);
            return new AuthResultDto
            {
                IsAuthenticated = true,
                Token = token,
                ExpiresIn = _jwtSettings.ExpiryMinutes 
            };
        }

        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.or_id_usuario.ToString()),
                    new Claim(ClaimTypes.Name, user.or_nombre),                    
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
       

        
    }

    
}

