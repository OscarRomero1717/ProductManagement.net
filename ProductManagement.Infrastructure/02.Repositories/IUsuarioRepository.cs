using Catalog.Domain._01.Entities;

namespace Catalog.Infrastructure._02.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByNombrePassword(string nombre, string password);
    }
}
