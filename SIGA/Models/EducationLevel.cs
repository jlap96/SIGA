using System.ComponentModel.DataAnnotations;

namespace SIGA.Models
{
    public class EducationLevel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 45, MinimumLength = 7, ErrorMessage = "La longitud del campo {0} debe estar entre {2} y {1} carácteres")]
        public string Nombre { get; set; }

        [Display(Name = "Costo Inscripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal? CostoInscripcion { get; set; }

        [Display(Name = "Costo Colegiatura")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal? CostoColegiatura { get; set; }   
    }
}
