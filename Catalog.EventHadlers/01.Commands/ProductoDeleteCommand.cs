using MediatR;

namespace Catalog.EventHadlers._01.Commands
{
    public class ProductoDeleteCommand:  IRequest<bool>
    {
        public int pro_id { get; set; }

    }
}
