using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.API.Model;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;
using Newtonsoft.Json;

namespace SistemaUsuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(RegisterModel model)
        {
            try
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                HistoricoRepository historicoRepository = new HistoricoRepository();

                if (usuarioRepository.GetByEmail(model.Email) != null)
                {
                    return StatusCode(400, new { mensagem = "O e-mail informado ja existe no nosso cadastro!" });
                }
                else
                {
                    //cadastrar usuario
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = Guid.NewGuid();
                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = MD5Helper.Encrypt(model.Senha);
                    usuarioRepository.Insert(usuario);

                    //registrar historico
                    Historico historico = new Historico();
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.Registro = DateTime.Now;
                    historico.Operacao = "Cadastro de usuário";
                    historico.Detalhes = JsonConvert.SerializeObject(usuario);
                    historicoRepository.Insert(historico);

                    return StatusCode(201, new { mensagem = $"Usuario {usuario.Nome} cadastrado com sucesso!" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = $"Falha ao gravar o e-mail. {e}" });
            }
        }
    }
}
