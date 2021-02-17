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
    public class EditorialRepository : IEditorialRepository
    {

        private readonly ApplicationDbContext _db;

        public EditorialRepository(ApplicationDbContext db)
        {
            _db = db;
        }
       
        public bool Actualizar(Editorial editorial)
        {
            _db.Editorial.Update(editorial);
            return Guardar();
        }

        public bool Borrar(Editorial editorial)
        {
            _db.Editorial.Remove(editorial);
            return Guardar();
        }

        public bool Crear(Editorial editorial)
        {
            _db.Editorial.Add(editorial);
            return Guardar();
        }

        public bool Existe(string nombre)
        {
            var valor = _db.Editorial.Any(e => e.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool Existe(int id)
        {
            return _db.Editorial.Any(e => e.Id == id);
        }

        public ICollection<Editorial> Get()
        {
            return _db.Editorial.OrderBy(e => e.Nombre).ToList();
        }

        public Editorial Get(int Id)
        {
            return _db.Editorial.FirstOrDefault(e => e.Id == Id);
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
