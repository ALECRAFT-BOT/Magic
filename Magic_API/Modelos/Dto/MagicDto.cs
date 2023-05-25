using System.ComponentModel.DataAnnotations;

namespace Magic_API.Modelos.Dto
{
    public class MagicDto
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }

        public string Apeliido { get; set; }

        public string Edad { get; set; }

        public string Telefono { get; set; }
    }
}
