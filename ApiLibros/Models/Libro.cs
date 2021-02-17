using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        public string Titulo  { get; set; }
        public int Agno { get; set; }
        public string Genero { get; set; }
        public int NumeroPaginas { get; set; }
        public int editorialId { get; set; }
        [ForeignKey("editorialId")]
        public Editorial Editorial { get; set; }
        public int autorId { get; set; }
        [ForeignKey("autorId")]
        public Autor Autor{ get; set; }

    }
}
