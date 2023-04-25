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
    public class UsuarioRepository
    {
        //mapeamento para inserir dados na banco
        public void Insert(Usuario usuario)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o insert no banco
                sqlServerContext.Usuario.Add(usuario);
                sqlServerContext.SaveChanges();
            }
        }

        //mapeamente para atualizar um registro no banco 
        public void Update(Usuario usuario)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o update no banco
                sqlServerContext.Usuario.Entry(usuario).State = EntityState.Modified;
                sqlServerContext.SaveChanges();
            }
        }

        //mapeamente para remover um registro no banco 
        public void Delete(Usuario usuario)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realizando o delete no banco
                sqlServerContext.Usuario.Remove(usuario);
                sqlServerContext.SaveChanges();
            }
        }

        //mapeamente buscar um email na tabela usuario
        public Usuario? GetByEmail(string email)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email));
            }
        }

        //mapeamente buscar um email na tabela usuario
        public Usuario? GetByEmailSenha(string email, string senha)
        {
            //conectando ao banco
            using (var sqlServerContext = new SqlServerContext())
            {
                //realiza a consulta no banco de dados
                return sqlServerContext.Usuario.FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
            }
        }
    }
}
