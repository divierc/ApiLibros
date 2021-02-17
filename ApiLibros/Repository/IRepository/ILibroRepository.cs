using ApiLibros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository.IRepository
{
    public interface ILibroRepository
    {
        ICollection<Libro> Get();
        ICollection<Libro> GetLibrosPorAutor(int autorId);
        ICollection<Libro> GetLibrosPorEditorial(int editorialId);

        Libro Get(int Id);
        bool Existe(string titulo);
        IEnumerable<Libro> Buscar(string titulo);
        bool Existe(int id);
        bool Crear(Libro libro);
        bool Actualizar(Libro libro);
        bool Borrar(Libro libro);
        bool Guardar();
    }
}

