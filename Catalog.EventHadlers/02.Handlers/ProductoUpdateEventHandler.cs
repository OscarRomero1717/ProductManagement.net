using AutoMapper;
using Catalog.EventHadlers._01.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Repositories;
using System.Reflection;

namespace Catalog.EventHadlers
{
    public class ProductoUpdateEventHandler : IRequestHandler<ProductoUpdateCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductoUpdateEventHandler> _logger;

        public ProductoUpdateEventHandler(IProductRepository productRepository, IMapper mapper, ILogger<ProductoUpdateEventHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(ProductoUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var producutoBd = _productRepository.GetByIdAsync(request.Id);
                //caso especial: fecha no cambia en update y  en bd genera expecion por intentar actualizar con  fecha menor a día de hoy, 
                var result = _productRepository.UpdateAsync(_mapper.Map<Producto>(request), CompareDateEquals(request.FechaCreacion, producutoBd.Result.or_fecha_creacion));
                return await Task.FromResult(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada en {Method}", MethodBase.GetCurrentMethod().Name);
                return await Task.FromResult(false);
            }
        }

       
        private static bool CompareDateEquals(DateTime fecha1, DateTime fecha2)
        {
            return fecha1.Date == fecha2.Date;
        }


    }
}
