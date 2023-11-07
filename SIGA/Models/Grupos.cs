using System.ComponentModel.DataAnnotations;

namespace SIGA.Models
{
    public class Grupos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 45, MinimumLength = 2, ErrorMessage = "La longitud del campo {0} debe estar entre {2} y {1} carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 45, MinimumLength = 10, ErrorMessage = "La longitud del campo {0} debe estar entre {2} y {1} carácteres")]
        public string Titular { get; set; }
    }
}
