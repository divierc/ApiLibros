using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dtos
{
    public class EditorialDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La direccion es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        public int Telefono { get; set; }
        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        public string CorreoElectronico { get; set; }
        public int MaxLibros { get; set; }
    }
}
