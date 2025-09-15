using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Repositories;
using System.Reflection;

namespace Catalog.EventHadlers
{
    public class ProductoCreateEventHandler : IRequestHandler<ProductoCreateCommand,bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductoCreateEventHandler> _logger;


        public ProductoCreateEventHandler(IProductRepository productRepository, IMapper mapper, ILogger<ProductoCreateEventHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

       
      

        async Task<bool> IRequestHandler<ProductoCreateCommand, bool>.Handle(ProductoCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var result = _productRepository.AddAsync(_mapper.Map<Producto>(request));                
                return await Task.FromResult(result.Result);

            }
            catch (Exception ex )
            {
                _logger.LogError(ex, "Excepción no controlada en {Method}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
