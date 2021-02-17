using ApiLibros.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Editorial> Editorial { get; set; }
        public DbSet<Libro> Libro { get; set; }
    }
}
