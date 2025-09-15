using Catalog.EventHadlers._01.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Infrastructure.Repositories;
using System.Reflection;

namespace Catalog.EventHadlers
{
    public class ProductoDeleteEventHandler : IRequestHandler<ProductoDeleteCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductoDeleteEventHandler> _logger;


        public ProductoDeleteEventHandler(IProductRepository productRepository, ILogger<ProductoDeleteEventHandler> logger)
        {
            _productRepository = productRepository;
            _logger= logger;


        }

        public async Task<bool> Handle(ProductoDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var result = _productRepository.DeleteAsync(request.pro_id);           
                return await Task.FromResult(result.Result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada en {Method}", MethodBase.GetCurrentMethod().Name);
                return await Task.FromResult(false);
            }
        }

        
    }
}
