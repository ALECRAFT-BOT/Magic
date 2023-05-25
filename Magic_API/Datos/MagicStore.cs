using Magic_API.Modelos.Dto;

namespace Magic_API.Datos
{
    public static class MagicStore
    {
        public static List<MagicDto> MagicList = new List<MagicDto> {

                new MagicDto{ Id = 1, Nombre = "pablo", Apeliido = "roto", Edad = "20", Telefono = "3132341602"},
                new MagicDto{ Id = 2, Nombre = "ramiro", Apeliido = "rotes", Edad = "22", Telefono = "3132441602"} 
            }; 
        


    }
}
