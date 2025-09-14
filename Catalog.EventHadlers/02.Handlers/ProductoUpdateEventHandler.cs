using AutoMapper;
using Catalog.EventHadlers._01.Commands;
using MediatR;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Repositories;

namespace Catalog.EventHadlers
{
    public class ProductoUpdateEventHandler : IRequestHandler<ProductoUpdateCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductoUpdateEventHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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

                return await Task.FromResult(false);
            }
        }

       
        public static bool CompareDateEquals(DateTime fecha1, DateTime fecha2)
        {
            return fecha1.Date == fecha2.Date;
        }


    }
}
