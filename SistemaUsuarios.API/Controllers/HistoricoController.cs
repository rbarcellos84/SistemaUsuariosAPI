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
    public class HistoricoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
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
                usuario = usuarioRepository.GetByEmail(email);

                List<HistoricoModel> listaModel = new List<HistoricoModel>();
                foreach (var item in historicoRepository.GetAllHistoricoIdUsuario(usuario.IdUsuario))
                {
                    HistoricoModel historicoModel = new HistoricoModel();
                    historicoModel.IdHistorico = item.IdHistorico;
                    historicoModel.Operacao = item.Operacao;
                    historicoModel.Registro = item.Registro;
                    listaModel.Add(historicoModel);
                }

                return StatusCode(200, listaModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}
