using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dtos
{
    public class LibroUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El año es obligatorio.")]
        public int Agno { get; set; }
        [Required(ErrorMessage = "El genero es obligatorio.")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El numero de paginas es obligatorio.")]
        public int NumeroPaginas { get; set; }
        [Required(ErrorMessage = "La editorial no esta registrada.")]
        public int editorialId { get; set; }
        [Required(ErrorMessage = "El autor no esta registrado.")]
        public int autorId { get; set; }
    }
}
