using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaUsuarios.API.Model;
using SistemaUsuarios.Data.Entities;
using SistemaUsuarios.Data.Helpers;
using SistemaUsuarios.Data.Repositories;
using SistemaUsuarios.Messages.Model;
using SistemaUsuarios.Messages.Services;

namespace SistemaUsuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordRecoverController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(PasswordRecoverModel model)
        {
            try
            {
                //buscar conta no banco com email
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                HistoricoRepository historicoRepository = new HistoricoRepository();
                var usuario = usuarioRepository.GetByEmail(model.Email);
                
                //verifica se foi encontrado
                if (usuario != null)
                {
                    //gerando uma nova senha
                    var fake = new Faker();
                    var novaSenha = $"@#{fake.Internet.Password(8)}";

                    //enviando a senha por email
                    var emailMessageModel = new EmailMessageModel();
                    emailMessageModel.MailTo = usuario.Email;
                    emailMessageModel.Subject = "Recuperação de senha de acesso.";
                    emailMessageModel.Body = @$"
                        <div>
                            Olá, {usuario.Email} <br/><br/>
                            Utilise a senha: <strong>{novaSenha}</strong> para acessar sua conta. <br/><br/>
                            Att, <br/> Sistema Usuários.
                        </div>";

                    var emailMessageService = new EmailMessageService();
                    emailMessageService.Send(emailMessageModel);

                    //atualizar senha do usuario
                    usuario.Senha = MD5Helper.Encrypt(novaSenha);
                    usuario.DataAtualizacao = DateTime.Now;
                    usuarioRepository.Update(usuario);

                    //gravando historico
                    Historico historico = new Historico();
                    historico.IdHistorico = Guid.NewGuid();
                    historico.IdUsuario = usuario.IdUsuario;
                    historico.Registro = DateTime.Now;
                    historico.Operacao = "Recuperação de senha";
                    historico.Detalhes = JsonConvert.SerializeObject(usuario);
                    historicoRepository.Insert(historico);

                    return StatusCode(200, new { mensagem = $"Nova senha gerada com sucesso, verifique seu email {model.Email}." });
                }
                else
                {
                    return StatusCode(400, new { mensagem = $"Email invalido ou email não cadastrado. {model.Email}." });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = $"Falha ao recuperar a senha do e-mail. {model.Email} {e}." });
            }
        }
    }
}
