using AutoMapper;
using Catalog.EventHadlers._01.Commands;
using MediatR;
using ProductManagement.Infrastructure.Repositories;

namespace Catalog.EventHadlers
{
    public class ProductoDeleteEventHandler : IRequestHandler<ProductoDeleteCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductoDeleteEventHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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

                return await Task.FromResult(false);
            }
        }

        
    }
}
