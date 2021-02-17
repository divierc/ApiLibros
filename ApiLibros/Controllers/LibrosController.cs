using ApiLibros.Models;
using ApiLibros.Models.Dtos;
using ApiLibros.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Controllers
{
    [Route("api/Libros")]
    [ApiController]
    public class LibrosController : Controller
    {
        private readonly ILibroRepository _ctRepo;

        private readonly IMapper _mapper;
        public LibrosController(ILibroRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Optener todos los libros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLibros()
        {
            var listaLibros = _ctRepo.Get();

            var listaLibroDto = new List<LibroDto>();

            foreach (var lista in listaLibros)
            {
                listaLibroDto.Add(_mapper.Map<LibroDto>(lista));
            }
            return Ok(listaLibroDto);
        }

        /// <summary>
        /// Optener solo un libro 
        /// </summary>
        /// <param name="libroId">id del libro a consultar</param>
        /// <returns></returns>
        [HttpGet("{libroId:int}", Name = "GetLibro")]
        public IActionResult GetLibro(int libroId)
        {
            var itemLibro = _ctRepo.Get(libroId);
            if (itemLibro == null)
            {
                return NotFound();
            }

            var itemLibroDto = _mapper.Map<LibroDto>(itemLibro);

            return Ok(itemLibroDto);
        }

        /// <summary>
        /// Optener libros por Autor
        /// </summary>
        /// <param name="autorId"></param>
        /// <returns></returns>
        [HttpGet("GetLibrosPorAutor/{autorId:int}")]
        public IActionResult GetLibrosPorAutor(int autorId)
        {
            var listaLibro = _ctRepo.GetLibrosPorAutor(autorId);

            if(listaLibro == null)
            {
                return NotFound();
            }
            var itemLibro = new List<LibroDto>(); 
            
            foreach (var item in listaLibro)
            {
                itemLibro.Add(_mapper.Map<LibroDto>(item));
            }
            return Ok(itemLibro);
        }

        /// <summary>
        /// Optener libros por Editorial
        /// </summary>
        /// <param name="editorialId"></param>
        /// <returns></returns>
        [HttpGet("GetLibrosPorEditorial/{editorialId:int}")]
        public IActionResult GetLibrosPorEditorial(int editorialId)
        {
            var listaLibro = _ctRepo.GetLibrosPorEditorial(editorialId);

            if (listaLibro == null)
            {
                return NotFound();
            }
            var itemLibro = new List<LibroDto>();

            foreach (var item in listaLibro)
            {
                itemLibro.Add(_mapper.Map<LibroDto>(item));
            }
            return Ok(itemLibro);

        }

        /// <summary>
        /// Optener libros por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _ctRepo.Buscar(nombre);
                if(resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Error recuperando datos de la aplicacion.");
            }
        }

        /// <summary>
        /// Crear un libro
        /// </summary>
        /// <param name="libroDto">parametros que debe tener para crear un libro</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CrearLibro([FromBody] LibroDto libroDto)
        {
            if (libroDto == null)
            {
                return BadRequest(ModelState);
            }



            if (_ctRepo.Existe(libroDto.Titulo))
            {
                ModelState.AddModelError("", "El libro ya existe.");
                return StatusCode(404, ModelState);
            }
            var libro = _mapper.Map<Libro>(libroDto);
            if (!_ctRepo.Crear(libro))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el libro {libro.Titulo}.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetLibro", new { libroId = libro.Id }, libro);
        }

        /// <summary>
        /// Actualizar un libro
        /// </summary>
        /// <param name="libroId">id del libro a actualizar </param>
        /// <param name="libroDto">parametros nuevos a ser actualizados</param>
        /// <returns></returns>
        [HttpPatch("{libroId:int}", Name = "ActualizarLibro")]
        public IActionResult ActualizarLibro(int libroId, [FromBody] LibroDto libroDto)
        {
            if (libroDto == null || libroId != libroDto.Id)
            {
                return BadRequest(ModelState);
            }
            var libro = _mapper.Map<Libro>(libroDto);

            if (!_ctRepo.Actualizar(libro))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el libro {libro.Titulo}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Borrar un libro existente
        /// </summary>
        /// <param name="libroId">is del libro a borrar</param>
        /// <returns></returns>
        [HttpDelete("{libroId:int}", Name = "BorrarLibro")]
        public IActionResult BorrarLibro(int libroId)
        {

            if (!_ctRepo.Existe(libroId))
            {
                return NotFound();
            }

            var libro = _ctRepo.Get(libroId);

            if (!_ctRepo.Borrar(libro))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el libro {libro.Titulo}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
