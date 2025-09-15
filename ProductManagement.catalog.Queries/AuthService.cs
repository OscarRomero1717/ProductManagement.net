using Catalog.Domain._01.Entities;
using Catalog.Infrastructure._02.Repositories;
using Catalog.QueriesService._01.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Catalog.QueriesService
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUsuarioRepository _userRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenJWT _tokenJWT;

        public AuthService(IOptions<JwtSettings> jwtSettings, IUsuarioRepository userRepository, ILogger<AuthService> logger, ITokenJWT tokenJWT)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _logger = logger;
            _tokenJWT = tokenJWT;
        }

        public async Task<AuthResultDto> AuthenticateAsync(string username, string password)
        {

            try
            {
                var user = await _userRepository.GetUsuarioByNombrePassword(username, password);

                if (user == null)
                {
                    return new AuthResultDto { IsAuthenticated = false, Message = "Credenciales Invalidas" };
                }

                var token = _tokenJWT.GenerateToken(user);
                return new AuthResultDto
                {
                    IsAuthenticated = true,
                    Token = token,
                    ExpiresIn = _jwtSettings.ExpiryMinutes
                };
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, "Excepción no controlada en {Method}", MethodBase.GetCurrentMethod().Name);
                throw;

            }
            
        }
       

        
    }

    
}

