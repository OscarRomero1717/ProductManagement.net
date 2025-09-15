using Dapper;
using Microsoft.Extensions.Logging;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Data;
using System.Data;
using System.Data.SqlClient;

namespace ProductManagement.Infrastructure.Repositories
{


    public class ProductRepository : IProductRepository
    {

        private readonly DapperContext _dbConnectionFactory;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(DapperContext dbConnectionFactory, ILogger<ProductRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {

            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();

                var result = await connection.QueryAsync<Producto>("or_obtener_todos",
                     commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al recuperar todos los productos");
                throw new ApplicationException("Se produjo un error en la base de datos al recuperar productos", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar todos los productos");
                throw;
            }
        }
        public async Task<Producto> GetByNombreAsync(string nombre)
        {

            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();

                var result = await connection.QueryFirstOrDefaultAsync<Producto>("or_buscar_productos_por_nombre", new { nombre = nombre },
                     commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al recuperar   producto");
                throw new ApplicationException("Se produjo un error en la base de datos al recuperar producto", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar  producto");
                throw;
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@rowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var result = await connection.QueryAsync<Producto>("or_eliminar_producto", parameters,
                     commandType: CommandType.StoredProcedure);
                int rowsAffected = parameters.Get<int>("@rowsAffected");
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al eliminar  producto");
                throw new ApplicationException("Se produjo un error en la base de datos al eliminar producto", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al elimianr   producto");
                throw;
            }
        }


        public async Task<bool> AddAsync(Producto product)
        {
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@codigo", product.or_codigo);
                parameters.Add("@nombre", product.or_nombre);
                parameters.Add("@descripcion", product.or_descripcion);
                parameters.Add("@referencia_interna", product.or_referencia_interna);
                parameters.Add("@precio_unitario", product.or_precio_unitario);
                parameters.Add("@estado", product.or_estado);
                parameters.Add("@unidad_medida", product.or_unidad_medida);
                parameters.Add("@fecha_creacion", product.or_fecha_creacion);
                parameters.Add("@rowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("or_insertar_producto", parameters,
                    commandType: CommandType.StoredProcedure);

                int rowsAffected = parameters.Get<int>("@rowsAffected");
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al crear el producto");
                return false;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return false;
            }
        }


        public async Task<bool> UpdateAsync(Producto product,bool dateDifferent)
        {
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@id", product.or_id);
                parameters.Add("@codigo", product.or_codigo);
                parameters.Add("@nombre", product.or_nombre);
                parameters.Add("@descripcion", product.or_descripcion);
                parameters.Add("@referencia_interna", product.or_referencia_interna);
                parameters.Add("@precio_unitario", product.or_precio_unitario);
                parameters.Add("@estado", product.or_estado);
                parameters.Add("@unidad_medida", product.or_unidad_medida);
                parameters.Add("@fecha_creacion", product.or_fecha_creacion);
                parameters.Add("@actualizarFecha", !dateDifferent);
                parameters.Add("@rowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);
              
                

                await connection.ExecuteAsync("or_actualizar_producto", parameters,
                    commandType: CommandType.StoredProcedure);

                int rowsAffected = parameters.Get<int>("@rowsAffected");
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al actualizar el producto");
                throw new ApplicationException("Se produjo un error en la base de datos al actualizar el producto", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el producto");
                throw;
            }
        }
        public async Task<Producto> GetByIdAsync(int id)
        {
            try
            {

                using var connection = _dbConnectionFactory.CreateConnection();

                var result = await connection.QueryFirstOrDefaultAsync<Producto>("or_obtener_producto_por_id", new { id = id },
                     commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error de SQL al recuperar   producto por Id");
                throw new ApplicationException("Se produjo un error en la base de datos al  producto por Id", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar  producto por Id");
                throw;
            }
        }
    }
}
