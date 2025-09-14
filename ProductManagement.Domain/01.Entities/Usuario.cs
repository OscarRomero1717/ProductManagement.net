using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain._01.Entities
{
    public class Usuario
    {

        public int or_id_usuario { get; set; }
        public string or_nombre { get; set; } = string.Empty;


        public string or_usuario { get; set; } = string.Empty;


        public string or_contrasena { get; set; } = string.Empty;

        public string? or_token_actualizacion { get; set; }

        public DateTime? or_token_actualizacion_expiracion { get; set; }
    }
}
