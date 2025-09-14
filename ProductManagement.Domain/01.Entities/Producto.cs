using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain
{
    public class Producto
    {
        public int or_id { get; set; }

        [Required(ErrorMessage = "El código del producto es obligatorio.")]
        public int or_codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(250, ErrorMessage = "El nombre no puede exceder los 250 caracteres.")]
        public string or_nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string or_descripcion { get; set; }

        [Required(ErrorMessage = "La referencia interna es obligatoria.")]
        [StringLength(100, ErrorMessage = "La referencia interna no puede exceder los 100 caracteres.")]
        public string or_referencia_interna { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor a 0.")]
        public decimal or_precio_unitario { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(Activo|Inactivo)$", ErrorMessage = "El estado debe ser 'Activo' o 'Inactivo'.")]
        public string or_estado { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
        [StringLength(50, ErrorMessage = "La unidad de medida no puede exceder los 50 caracteres.")]
        public string or_unidad_medida { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Producto), nameof(ValidarFechaCreacion))]
        public DateTime or_fecha_creacion { get; set; }

        // 🔹 Validador personalizado para fecha
        public static ValidationResult ValidarFechaCreacion(DateTime fecha, ValidationContext context)
        {
            if (fecha < DateTime.UtcNow.Date)
            {
                return new ValidationResult("La fecha de creación no puede ser anterior a la fecha actual.");
            }
            return ValidationResult.Success;
        }
    }

}



