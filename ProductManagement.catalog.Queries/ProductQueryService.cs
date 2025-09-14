using AutoMapper;
using Catalog.QueriesService;
using Microsoft.Extensions.Logging;
using ProductManagement.Infrastructure.Repositories;
using System.Reflection;

namespace ProductManagement.catalog.Queries
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductQueryService> _logger;

        public ProductQueryService(IProductRepository productRepository, IMapper mapper, ILogger<ProductQueryService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductoDto>> GetAllProductosAsync()
        {

            try
            {
                var collection = await _productRepository.GetAllAsync();
                var productosList = collection.ToList();               
                return _mapper.Map<List<ProductoDto>>(productosList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada" + MethodBase.GetCurrentMethod().Name);
                throw;
            }
           
        }

        public async Task<ProductoDto> GetProductoByNombreAsync( string nombre)
        {
            try
            {
                var collection = await _productRepository.GetByNombreAsync(nombre);
                return _mapper.Map<ProductoDto>(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada" + MethodBase.GetCurrentMethod().Name);
                throw;
            }
           
        }
    }
}