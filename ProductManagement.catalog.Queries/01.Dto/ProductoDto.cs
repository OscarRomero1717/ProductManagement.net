namespace Catalog.QueriesService
{
    public class ProductoDto
    {

        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string RefInterna { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Estado { get; set; }
        public string UnidadMedida { get; set; }
        public DateTime FechaCreacion { get; set; }


    }

    
}
