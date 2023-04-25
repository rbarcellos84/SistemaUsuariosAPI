using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Data.Contexts;
using SistemaUsuarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Repositories
{
    public class HistoricoRepository
    {
        //mapeamento para inserir dados na banco
        public void Insert(Historico historico)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o insert no banco
                sqlServerContext.Historico.Add(historico);
                sqlServerContext.SaveChanges();
            }
        }

        //mapeamente para atualizar um registro no banco 
        public void Update(Historico historico)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o update no banco
                sqlServerContext.Historico.Entry(historico).State = EntityState.Modified;
                sqlServerContext.SaveChanges();
            }
        }

        //mapeamente para remover um registro no banco 
        public void Delete(Historico historico)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o delete no banco
                sqlServerContext.Historico.Remove(historico);
                sqlServerContext.SaveChanges();
            }
        }

        //mapeamente buscar um email na tabela usuario
        public List<Historico> GetAllHistorico()
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Historico.OrderByDescending(h => h.Registro).ToList();
            }
        }

        //mapeamente buscar um email na tabela usuario
        public List<Historico> GetAllHistoricoPeriodo(DateTime dataMin, DateTime dataMax)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Historico.Where(h => h.Registro >= dataMin && h.Registro <= dataMax).OrderByDescending(h => h.Registro).ToList();
            }
        }

        //mapeamente buscar um email na tabela usuario
        public List<Historico> GetAllHistoricoIdUsuario(Guid idUsuaario)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Historico.Where(h => h.IdUsuario == idUsuaario).OrderByDescending(h => h.Registro).ToList();
            }
        }
    }
}
