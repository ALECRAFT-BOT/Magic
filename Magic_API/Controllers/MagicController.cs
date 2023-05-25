using Magic_API.Datos;
using Magic_API.Modelos;
using Magic_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagicController : ControllerBase
    {

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    
    public ActionResult<IEnumerable<MagicDto>> GetMagics() 
    {
            return Ok(MagicStore.MagicList); 
        
    }

    [HttpGet("id", Name = "GetMagic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MagicDto> GetMagic(int? id) {

            if (id == 0)
            {
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

    }
}
