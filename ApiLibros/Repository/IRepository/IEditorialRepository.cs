using ApiLibros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository.IRepository
{
    public interface IEditorialRepository
    {
        ICollection<Editorial> Get();
        Editorial Get(int Id);
        bool Existe(string nombre);
        bool Existe(int id);
        bool Crear(Editorial editorial);
        bool Actualizar(Editorial editorial);
        bool Borrar(Editorial editorial);
        bool Guardar();
    }
}
