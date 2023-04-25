using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Messages.Model
{
    //modelo de dados de envio de email
    public class EmailMessageModel
    {
        //destinatario
        public string MailTo { get; set; }

        //Assunto
        public string Subject { get; set; }

        //copo da mensagem
        public string Body { get; set; }
    }
}
