using SistemaUsuarios.Messages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Messages.Services
{
    public class EmailMessageService
    {
        //parametro para o envio de email
        private const string _conta = "cotinaoresponda@outlook.com";
        private const string _senha = "@Admin123456";
        private const string _smtp = "smtp-mail.outlook.com";
        private const int _port = 587;

        public void Send(EmailMessageModel model)
        {
            //criando conteudo do email
            var mailMessage = new MailMessage(_conta, model.MailTo);
            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;
            mailMessage.IsBodyHtml = true;

            //enviando o email
            var smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
