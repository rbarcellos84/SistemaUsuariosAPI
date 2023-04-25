using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.API.Model
{
    public class PasswordRecoverModel
    {
        [Required(ErrorMessage = "Por favor, informe o e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        public string Email { get; set; }
    }
}
