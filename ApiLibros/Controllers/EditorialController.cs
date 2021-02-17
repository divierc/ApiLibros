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
    [Route("api/Editorial")]
    [ApiController]
    public class EditorialController : Controller
    {
        private readonly IEditorialRepository _ctRepo;

        private readonly IMapper _mapper;

        public EditorialController(IEditorialRepository ctRepo,IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Optener las editioriales
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEditoriales()
        {
            var listaEditoriales = _ctRepo.Get();

            var listaEditorialDto = new List<EditorialDto>();

            foreach (var lista in listaEditoriales)
            {
                listaEditorialDto.Add(_mapper.Map<EditorialDto>(lista));
            }
            return Ok(listaEditorialDto);
        }

        /// <summary>
        /// Optener una editorial
        /// </summary>
        /// <param name="editorialId"></param>
        /// <returns></returns>
        [HttpGet("{editorialId:int}", Name = "GetEditorial")]
        public IActionResult GetEditorial(int editorialId)
        {
            var itemEditorial = _ctRepo.Get(editorialId);
            if (itemEditorial == null)
            {
                return NotFound();
            }

            var itemEditorialDto = _mapper.Map<EditorialDto>(itemEditorial);

            return Ok(itemEditorialDto);
        }

        /// <summary>
        /// Crear una editorial
        /// </summary>
        /// <param name="editorialDto">contiene la informacion para crear la nueva editorial</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CrearEditorial([FromBody] EditorialDto editorialDto)
        {
            if (editorialDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.Existe(editorialDto.Nombre))
            {
                ModelState.AddModelError("", "La editorial ya existe.");
                return StatusCode(404, ModelState);
            }
            var editorial = _mapper.Map<Editorial>(editorialDto);
            if (!_ctRepo.Crear(editorial))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando la editorial {editorialDto.Nombre}.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEditorial", new { editorialId = editorial.Id }, editorial);
        }

        /// <summary>
        /// Actualiza una editorial ya existente
        /// </summary>
        /// <param name="editorialId"></param>
        /// <param name="editorialDto"></param>
        /// <returns></returns>
        [HttpPatch("{editorialId:int}", Name = "ActualizarEditorial")]
        public IActionResult ActualizarEditorial(int editorialId, [FromBody] EditorialDto editorialDto)
        {
            if (editorialDto == null || editorialId != editorialDto.Id)
            {
                return BadRequest(ModelState);
            }
            var editorial = _mapper.Map<Editorial>(editorialDto);

            if (!_ctRepo.Actualizar(editorial))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando la editorial {editorialDto.Nombre}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        /// <summary>
        /// Borra una editorial existente
        /// </summary>
        /// <param name="editorialId"></param>
        /// <returns></returns>
        [HttpDelete("{editorialId:int}", Name = "BorrarEditorial")]
        public IActionResult BorrarEditorial(int editorialId)
        {

            // var editorial = _mapper.Map<Editorial>(editorialDto);

            if (!_ctRepo.Existe(editorialId))
            {
                return NotFound();
            }

            var editorial = _ctRepo.Get(editorialId);

            if (!_ctRepo.Borrar(editorial))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando la editorial {editorial.Nombre}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

