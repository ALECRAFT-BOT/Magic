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
    public IEnumerable<MagicDto> GetMagics() 
    {
            return new List<MagicDto>
            {
                new MagicDto{ Id = 1, Nombre = "pablo", Apeliido = "roto", Edad = "20", Telefono = "3132341602"},
                new MagicDto{ Id = 2, Nombre = "ramiro", Apeliido = "rotes", Edad = "22", Telefono = "3132441602"}

            };
        
    }


    }
}
