using ApiLibros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository.IRepository
{
    public interface IAutorRepository
    {
        ICollection<Autor> GetAutores();
        Autor GetAutor(int autorId);
        bool ExisteAutor(string nombre, string apellido);
        bool ExisteAutor(int id);
        bool CrearAutor(Autor autor);
        bool ActualizarAutor(Autor autor);
        bool BorrarAutor(Autor autor);
        bool Guardar();

    }
}
