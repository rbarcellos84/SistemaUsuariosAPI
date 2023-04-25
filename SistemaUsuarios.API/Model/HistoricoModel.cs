using System.ComponentModel.DataAnnotations;
namespace SistemaUsuarios.API.Model
{
    public class HistoricoModel
    {
        public Guid IdHistorico { get; set; }
        public DateTime Registro { get; set; }
        public string Operacao { get; set; }
    }
}
