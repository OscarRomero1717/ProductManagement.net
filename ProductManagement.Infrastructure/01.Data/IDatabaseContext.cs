using System.Data;

namespace ProductManagement.Infrastructure
{
    public interface IDatabaseContext
    {
        IDbConnection CreateConnection();
        Task<IDbConnection> CreateOpenConnectionAsync();
    }
}
