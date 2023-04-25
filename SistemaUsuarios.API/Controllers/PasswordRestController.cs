using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaUsuarios.API.Model;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;

namespace SistemaUsuarios.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordRestController : ControllerBase
    {
        [HttpPut]
        public IActionResult Put(PasswordRestModel model)
        {
            try
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                HistoricoRepository historicoRepository = new HistoricoRepository();
                Usuario usuario = new Usuario();
                Historico historico = new Historico();

                //obter o email da chave do token
                var email = User.Identity.Name;

                //obterndo dados do usuario
                usuario = usuarioRepository.GetByEmailSenha(email, MD5Helper.Encrypt(model.SenhaAtual));

                if (usuario != null)
                {
                    //atualizar usuario
                    usuario.Senha = MD5Helper.Encrypt(model.SenhaNova);
                    usuario.DataAtualizacao = DateTime.Now;
                    usuarioRepository.Update(usuario);

                    //registrar historico
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.Registro = DateTime.Now;
                    historico.Operacao = "Alteração de senha";
                    historico.Detalhes = JsonConvert.SerializeObject(usuario);
                    historicoRepository.Insert(historico);

                    return StatusCode(200, new { mensagem = $"Senha alterada com sucesso!" });
                }
                else
                {
                    return StatusCode(400, new { mensagem = $"Senha atual invalida." });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = $"Falha ao gravar a senha. {e}." });
            }
        }
    }
}
