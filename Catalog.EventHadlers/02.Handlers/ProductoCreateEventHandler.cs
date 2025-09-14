using AutoMapper;
using MediatR;
using ProductManagement.Domain;
using ProductManagement.Infrastructure.Repositories;

namespace Catalog.EventHadlers
{
    public class ProductoCreateEventHandler : IRequestHandler<ProductoCreateCommand,bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductoCreateEventHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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

                return await Task.FromResult(false);
            }
        }
    }
}
