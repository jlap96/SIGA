using System.ComponentModel.DataAnnotations;

namespace SIGA.Models
{
    public class Materias
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 45, MinimumLength = 5, ErrorMessage = "La longitud del campo {0} debe estar entre {2} y {1} carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public char Estatus { get; set; }
    }
}
