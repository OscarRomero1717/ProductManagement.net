using Catalog.Domain._01.Entities;
using Dapper;
using Microsoft.Extensions.Logging;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure._02.Repositories
{
    internal class UsuarioRepository : IUsuarioRepository
    {


        private readonly DapperContext _dbConnectionFactory;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(DapperContext dbConnectionFactory, ILogger<UsuarioRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }
        public async Task<Usuario> GetUsuarioByNombrePassword(string usuario, string password)
        {
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();

                var result = await connection.QueryFirstOrDefaultAsync<Usuario>("spUsuarios_BuscarPorCredenciales", new { or_usuario = usuario, or_contrasena = password },
                     commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al recuperar Usuario");
                throw new ApplicationException("Se produjo un error en la base de datos al recuperar Usuario", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar  Usuario");
                throw;
            }
        }
    }
}

