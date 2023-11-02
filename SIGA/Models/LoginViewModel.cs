using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIGA.Models
{
    public class LoginViewModel
    {
        [DisplayName("usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [RegularExpression(@"^\S*$", ErrorMessage = "El campo {0} no puede contener espacios")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string Email { get; set; }

        [DisplayName("contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
