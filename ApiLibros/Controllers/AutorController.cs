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
    [Route("api/Autor")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly IAutorRepository _ctRepo;

        private readonly IMapper _mapper;

        public AutorController(IAutorRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;

        }

        /// <summary>
        /// Optener todos los autores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAutores()
        {
            var listaAutores = _ctRepo.GetAutores();

            var listaAutorDto = new List<AutorDto>();

            foreach (var lista in listaAutores)
            {
                listaAutorDto.Add(_mapper.Map<AutorDto>(lista));
            }
            return Ok(listaAutorDto);
        }

        /// <summary>
        /// Optener un autor individual
        /// </summary>
        /// <param name="autorId">Este es el id del autor </param>
        /// <returns></returns>
        [HttpGet("{autorId:int}", Name = "GetAutor")]
        public IActionResult GetAutor(int autorId)
        {
            var itemAutor = _ctRepo.GetAutor(autorId);
            if (itemAutor == null)
            {
                 return NotFound();
            }

            var itemAutorDto = _mapper.Map<AutorDto>(itemAutor);

            return Ok(itemAutorDto);
        }

        /// <summary>
        /// Crear un nuevo autor
        /// </summary>
        /// <param name="autorDto">Contiene la info para crear un nuevo autor</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CrearAutor([FromBody] AutorDto autorDto)
        {
            if (autorDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteAutor(autorDto.Nombre, autorDto.Apellido))
            {
                ModelState.AddModelError("", "El autor ya existe.");
                return StatusCode(404, ModelState);
            }
            var autor = _mapper.Map<Autor>(autorDto);
            if(!_ctRepo.CrearAutor(autor))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el autor {autorDto.Nombre} {autorDto.Apellido}.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAutor", new { autorId = autor.Id}, autor);
        }

        /// <summary>
        /// Actualizar un autor existente
        /// </summary>
        /// <param name="autorId"></param>
        /// <param name="autorDto"></param>
        /// <returns></returns>
        [HttpPatch("{autorId:int}", Name = "ActualizarAutor")]
        public IActionResult ActualizarAutor(int autorId, [FromBody] AutorDto autorDto)
        {
            if(autorDto == null || autorId != autorDto.Id)
            {
                return BadRequest(ModelState);
            }
            var autor = _mapper.Map<Autor>(autorDto);

            if (!_ctRepo.ActualizarAutor(autor))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el autor {autorDto.Nombre} {autorDto.Apellido}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Borrar un autor existente
        /// </summary>
        /// <param name="autorId"></param>
        /// <returns></returns>
        [HttpDelete("{autorId:int}", Name = "BorrarAutor")]
        public IActionResult BorrarAutor(int autorId)
        {
            
            // var autor = _mapper.Map<Autor>(autorDto);

            if (!_ctRepo.ExisteAutor(autorId))
            {
                return NotFound();
            }

            var autorDto = _ctRepo.GetAutor(autorId);

            if (!_ctRepo.BorrarAutor(autorDto))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el autor {autorDto.Nombre} {autorDto.Apellido}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
