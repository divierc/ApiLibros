using ApiLibros.Data;
using ApiLibros.Models;
using ApiLibros.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class LibroRepository : ILibroRepository
    {
        private readonly ApplicationDbContext _db;

        public LibroRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Actualizar(Libro libro)
        {
            _db.Libro.Update(libro);
            return Guardar();
        }

        public bool Borrar(Libro libro)
        {
            _db.Libro.Remove(libro);
            return Guardar();
        }

        public IEnumerable<Libro> Buscar(string titulo)
        {
            IQueryable<Libro> query = _db.Libro;
            if(!string.IsNullOrEmpty(titulo))
            {
                query = query.Where( e => e.Titulo.Contains(titulo) );
            }

            return query.ToList();
        }

        public bool Crear(Libro libro)
        {
            _db.Libro.Add(libro);
            return Guardar();
        }

        public bool Existe(string titulo)
        {
            var valor=_db.Libro.Any(l => l.Titulo.ToLower().Trim() == titulo.ToLower().Trim());
            return valor;
        }

        public bool Existe(int id)
        {
            return _db.Libro.Any(l => l.Id == id);
        }

        public ICollection<Libro> Get()
        {
            return _db.Libro.OrderBy(l => l.Titulo).ToList();
        }

        public Libro Get(int Id)
        {
            return _db.Libro.FirstOrDefault(l => l.Id == Id);
        }

        public ICollection<Libro> GetLibrosPorAutor(int autorId)
        {
            return _db.Libro.Include(a => a.Autor).Where(a => a.autorId == autorId).ToList();
        }

        public ICollection<Libro> GetLibrosPorEditorial(int editorialId)
        {
            return _db.Libro.Include(a => a.Editorial).Where(a => a.editorialId == editorialId).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0;
        }
    }
}
