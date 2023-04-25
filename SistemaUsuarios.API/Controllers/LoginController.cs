using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaUsuarios.API.Model;
using SistemaUsuarios.API.Security;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;

namespace SistemaUsuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(LoginModel model)
        {
            try
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                HistoricoRepository historicoRepository = new HistoricoRepository();
                var usuario = usuarioRepository.GetByEmailSenha(model.Email, MD5Helper.Encrypt(model.Senha));

                if (usuario == null)
                {
                    return StatusCode(400, new { mensagem = $"Acesso negado ou inválido." });
                }
                else
                {
                    //gerar token
                    var token = JwtSecurity.GenerateToken(usuario.Email);

                    //registrando token
                    Historico historico = new Historico();
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.Registro = DateTime.Now;
                    historico.Operacao = "Autenticando usuario";
                    historico.Detalhes = JsonConvert.SerializeObject(usuario);
                    historicoRepository.Insert(historico);

                    //retorna token
                    return StatusCode(200, new { mensagem = $"Autenticação realiada com sucesso.", token});
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = $"Falha ao gravar o e-mail. {e}" });
            }
        }
    }
}
