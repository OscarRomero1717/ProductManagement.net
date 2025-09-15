using Catalog.Domain._01.Entities;

namespace Catalog.QueriesService
{
    public interface ITokenJWT
    {

        string GenerateToken(Usuario user);
    }
}
