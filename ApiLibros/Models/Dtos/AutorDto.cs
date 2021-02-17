using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class AutorDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }
        public string NombreCompleto { get; set; } 
        public DateTime FechaNacimiento { get; set; }
        public string CiudadProcedencia { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
