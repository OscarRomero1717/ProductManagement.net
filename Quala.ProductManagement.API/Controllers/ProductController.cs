using Catalog.EventHadlers;
using Catalog.EventHadlers._01.Commands;
using Catalog.QueriesService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Common.Collection;
using ProductManagement.Common.Paging;

namespace Quala.ProductManagement.API.Controllers
{
    [ApiController]
    [Route("productos")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueryService _productService;
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductQueryService productService,
            IMediator mediator,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<DataCollection<ProductoDto>>> GetAll(int page = 1, int take = 10)
        {
            _logger.LogInformation("Iniciando consulta de productos. Página: {Page}, Tamaño: {Take}", page, take);

            var collection = await _productService.GetAllProductosAsync();

            if (collection == null || !collection.Any())
            {
                _logger.LogWarning("No se encontraron productos en la consulta.");
                return NotFound(new { message = "No hay productos disponibles" });
            }

            var list = await collection.AsQueryable().GetPageAsync(page, take);

            _logger.LogInformation("Consulta completada. Se retornan  productos.");

            return Ok(list);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CreateProducto(ProductoCreateCommand command)
        {
            _logger.LogInformation("Solicitud para crear un nuevo producto: {Nombre}", command.Nombre);

            var response = await _mediator.Send(command);

            if (!response)
            {
                _logger.LogError("Error al intentar crear el producto {Nombre}", command.Nombre);
                return StatusCode(500, new { message = "Error de servidor" });
            }

            var product = await _productService.GetProductoByNombreAsync(command.Nombre);

            _logger.LogInformation("Producto {Nombre} creado con éxito con ID {Id}", product.Nombre, product.Id);

            return StatusCode(StatusCodes.Status201Created, product);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductoDto>> UpdateProducto(int id, ProductoUpdateCommand command)
        {
            if (id != command.Id)
            {
                _logger.LogWarning("El id de la URL ({UrlId}) no coincide con el payload ({PayloadId})", id, command.Id);
                return BadRequest(new { message = "El id de la URL no coincide con el payload" });
            }

            _logger.LogInformation("Solicitud de actualización para producto con ID {Id}", id);

            var response = await _mediator.Send(command);

            if (!response)
            {
                _logger.LogError("Error al actualizar el producto con ID {Id}", id);
                return StatusCode(500, new { message = "Error de servidor" });
            }

            _logger.LogInformation("Producto con ID {Id} actualizado correctamente", id);

            return Ok(command);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductoDto>> DeleteProducto(int id)
        {
            _logger.LogInformation("Solicitud para eliminar producto con ID {Id}", id);

            var command = new ProductoDeleteCommand { pro_id = id };
            var response = await _mediator.Send(command);

            if (!response)
            {
                _logger.LogWarning("Intento de eliminar producto con ID {Id}, pero no se encontró en la base de datos", id);
                return NotFound(new { message = $"Producto con id {id} no encontrado" });
            }

            _logger.LogInformation("Producto con ID {Id} eliminado correctamente", id);

            return NoContent();
        }
    }
}
