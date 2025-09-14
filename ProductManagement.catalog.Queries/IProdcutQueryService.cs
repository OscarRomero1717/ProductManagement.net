namespace Catalog.QueriesService
{
    public interface IProductQueryService
    {
        Task<IEnumerable<ProductoDto>> GetAllProductosAsync();

        Task<ProductoDto> GetProductoByNombreAsync( string nombre);
    }
}
