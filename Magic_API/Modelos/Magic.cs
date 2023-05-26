using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_API.Modelos
{
    public class Magic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(40)]
        public string Apeliido { get; set; }
        [Required]
        public string Edad { get; set; }

        public string Telefono { get; set; }
    }
}
