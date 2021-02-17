using ApiLibros.Data;
using ApiLibros.Models;
using ApiLibros.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class AutorRepository : IAutorRepository
    {
        private readonly ApplicationDbContext _db;
        public AutorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool ActualizarAutor(Autor autor)
        {
            _db.Autor.Update(autor);
            return Guardar();
        }

        public bool BorrarAutor(Autor autor)
        {
            _db.Autor.Remove(autor);
            return Guardar();
        }

        public bool CrearAutor(Autor autor)
        {
            _db.Autor.Add(autor);
            return Guardar();
        }

        public bool ExisteAutor(string nombre, string apellido)
        {
            bool valor = _db.Autor.Any(a => 
                                       a.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && 
                                       a.Apellido.ToLower().Trim() == apellido.ToLower().Trim());
            return valor;
        }

        public bool ExisteAutor(int id)
        {
            return _db.Autor.Any(a => a.Id == id);
        }

        public Autor GetAutor(int autorId)
        {
            return _db.Autor.FirstOrDefault(a => a.Id == autorId);
        }

        public ICollection<Autor> GetAutores()
        {
            return _db.Autor.OrderBy(a => a.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
