using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Entities
{
    public class Historico
    {
        public Guid IdHistorico { get; set; }
        public Guid IdUsuario { get; set; }
        public string Operacao { get; set; }
        public DateTime Registro { get; set; }
        public string Detalhes { get; set; }

        //criando um relacionamento, 1 historico esta contido para 1 usuario
        public Usuario Usuario { get; set; }
    }
}
