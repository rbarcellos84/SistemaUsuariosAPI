using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.API.Model
{
    /// <summary>
    /// Modelo de dados para o ENDPOINT de cadastro de usuario
    /// </summary>
    public class RegisterModel
    {
        [Required(ErrorMessage = "Por favor, informe o nome do usuário.")]
        [MinLength(6,ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o e-mail do usuário.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de e-mail válido.")]
        public string Email { get; set; }

        [StrongPassword(ErrorMessage ="Informe uma senha de 8 a 20 carateres e contenha 1 letra maiúscola, 1 letra minúscola, 1 numero e 1 caracter especial (@!#$%&).")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Por favor, confirme a senha do usuário.")]
        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        public string ConfirmeSenha { get; set; }
    }

    /// <summary>
    /// classe para implementar uma validação customizada para o campo de senha
    /// </summary>
    
    public class StrongPassword : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                var senha = value.ToString();
                return senha.Length >= 8 && senha.Length <= 20
                    && senha.Any(char.IsUpper) //pelo menos 1 caracter em caixa alta 
                    && senha.Any(char.IsLower) //pelo menos 1 caracter em caixa baixa
                    && senha.Any(char.IsDigit) //pelo menos 1 digito
                    && (
                        senha.Contains("@") ||
                        senha.Contains("#") ||
                        senha.Contains("!") ||
                        senha.Contains("$") ||
                        senha.Contains("%") ||
                        senha.Contains("&")
                    );
            }
            return false;
        }
    }

}
