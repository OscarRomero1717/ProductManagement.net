using System.ComponentModel.DataAnnotations;

namespace Catalog.QueriesService
{
    public class ProductoDto
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El código del producto es obligatorio.")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(250, ErrorMessage = "El nombre no puede exceder los 250 caracteres.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La referencia interna es obligatoria.")]
        [StringLength(100, ErrorMessage = "La referencia interna no puede exceder los 100 caracteres.")]
        public string RefInterna { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor a 0.")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(Activo|Inactivo)$", ErrorMessage = "El estado debe ser 'Activo' o 'Inactivo'.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
        [StringLength(50, ErrorMessage = "La unidad de medida no puede exceder los 50 caracteres.")]
        public string UnidadMedida { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }


    }

    
}
