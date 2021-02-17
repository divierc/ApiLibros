using ApiLibros.Models;
using ApiLibros.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.LibrosMapper
{
    public class LibrosMappers: Profile
    {
        public LibrosMappers()
        {
            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<Libro, LibroDto>().ReverseMap();
            CreateMap<Libro, LibroCreateDto>().ReverseMap();
            CreateMap<Libro, LibroUpdateDto>().ReverseMap();
            CreateMap<Editorial, EditorialDto>().ReverseMap();
        }
    }
}
