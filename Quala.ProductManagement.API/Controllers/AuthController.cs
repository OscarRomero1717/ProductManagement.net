using Catalog.QueriesService;
using Catalog.QueriesService._01.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quala.ProductManagement.API.Controllers
{


    namespace Quala.ProductManagement.API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        [AllowAnonymous]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;
            private readonly ILogger<AuthController> _logger;

            public AuthController(IAuthService authService, ILogger<AuthController> logger)
            {
                _authService = authService;
                _logger = logger;
            }

            [HttpPost("login")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        _logger.LogWarning("Login con modelo no valido ");
                        return BadRequest(ModelState);
                    }



                    var result = await _authService.AuthenticateAsync(request.Username, request.Password);

                    if (!result.IsAuthenticated)
                    {
                        _logger.LogWarning("Login fallido para {Username}", request.Username);
                        return Unauthorized(new { message = result.Message });
                    }

                    return Ok(new AuthResponseDto
                    {
                        Token = result.Token,
                        ExpiresIn = result.ExpiresIn,
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en login {Username}", request.Username);
                    return StatusCode(500, "Error de servidor");
                }
            }




        }
    }
}