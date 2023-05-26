using Magic_API.Datos;
using Magic_API.Modelos;
using Magic_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Magic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagicController : ControllerBase
    {
    
    private readonly ILogger<MagicController> _logger;

    public MagicController(ILogger<MagicController> logger)
    {
     
            _logger= logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    
    public ActionResult<IEnumerable<MagicDto>> GetMagics() 
    {
            _logger.LogInformation("todas las magic");
            return Ok(MagicStore.MagicList); 
        
    }

    [HttpGet("id", Name = "GetMagic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MagicDto> GetMagic(int? id) {

            if (id == 0)
            {
                _logger.LogError("error con la magic id: " + id);
                return BadRequest();
            }

            var Magic2 = MagicStore.MagicList.FirstOrDefault(v => v.Id == id);

            if (Magic2 == null) {
                return NotFound();
            }
            return Ok(Magic2);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MagicDto> crearMagic([FromBody] MagicDto magicDto) {

            if (!ModelState.IsValid) { 
            
                return BadRequest(ModelState);
            }
            
            if (magicDto == null) {
                return BadRequest();
            }
            if (MagicStore.MagicList.FirstOrDefault(v => v.Nombre.ToLower() == magicDto.Nombre.ToLower()) != null) {

                ModelState.AddModelError("NombreExiste", "El Magic con ese nombre ya existe");
                return BadRequest(ModelState);

            }
            if (MagicStore.MagicList.FirstOrDefault(v => v.Telefono == magicDto.Telefono) != null)
            {

                ModelState.AddModelError("TelefonoExiste", "El Magic con esa Telefono ya existe");
                return BadRequest(ModelState);

            }

            if (magicDto.Id > 0) { 
               return StatusCode(StatusCodes.Status500InternalServerError);
            }
             
            magicDto.Id = MagicStore.MagicList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;

            MagicStore.MagicList.Add(magicDto);

            return CreatedAtRoute("GetMagic", new { id = magicDto.Id }, magicDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteMagic(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var Magic = MagicStore.MagicList.FirstOrDefault(v => v.Id == id);

            if (Magic == null ) { 

               return NotFound();
            }

            MagicStore.MagicList.Remove(Magic);

            return NoContent();

        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMagic(int id, [FromBody] MagicDto magicDto) {

            if (id != magicDto.Id )
            {
                return BadRequest();
            }

            var Magic = MagicStore.MagicList.FirstOrDefault(v => v.Id == id);

            Magic.Nombre = magicDto.Nombre;
            Magic.Apeliido = magicDto.Apeliido;
            Magic.Edad = magicDto.Edad;
            Magic.Telefono = magicDto.Telefono;
            

            if (Magic == null)
            {

                return NotFound();
            }

            return NoContent();


        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateParcialMagic(int id,JsonPatchDocument<MagicDto> patchDto)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var Magic = MagicStore.MagicList.FirstOrDefault(v => v.Id == id);

            patchDto.ApplyTo(Magic, ModelState);


            if (Magic == null)
            {

                return NotFound();
            }

            if (!ModelState.IsValid) { 
            return BadRequest(ModelState);
            
            }

            return NoContent();


        }


    }
}
