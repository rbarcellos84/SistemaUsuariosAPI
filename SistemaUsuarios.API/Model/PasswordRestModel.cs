using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.API.Model
{
    public class PasswordRestModel
    {
        [Required(ErrorMessage = "Por favor, informe a senha atual.")]
        public string SenhaAtual { get; set; }

        [StrongPassword(ErrorMessage = "Informe uma senha de 8 a 20 carateres e contenha 1 letra maiúscola, 1 letra minúscola, 1 numero e 1 caracter especial (@!#$%&).")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string SenhaNova { get; set; }

        [Required(ErrorMessage = "Por favor, confirme a senha do usuário.")]
        [Compare("SenhaNova", ErrorMessage = "Senhas não conferem.")]
        public string ConfirmeSenha { get; set; }
    }
}
