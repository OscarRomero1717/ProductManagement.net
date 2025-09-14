using Catalog.QueriesService._01.Dto;

namespace Catalog.QueriesService
{
    public interface IAuthService
    {
        Task<AuthResultDto> AuthenticateAsync(string username, string password);
    }
}
