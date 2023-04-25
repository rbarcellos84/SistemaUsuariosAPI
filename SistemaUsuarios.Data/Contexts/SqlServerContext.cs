using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Data.Mappings;
using SistemaUsuarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Contexts
{
    public class SqlServerContext : DbContext
    {
        ///<summary>
        ///classe para acessar o banco de dados do sqlserver atravez do entityFramework (classe de conexao com o BD)
        ///</summary>
        //metodo para fazer com que o entityFramework possa conectar no banco de dados do projeto
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string de conexao com o banco de dados
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BdSistemaUsuario;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //metodo para incluir cada classe de mapeamento feita no projeto com o EntityFramework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adiciona cada classe de mapeamento do projeto
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new HistoricoMap());
        }

        //propriedade DbSet para cada classe de entidade, este DbSet vai disponibilizar para cada entidade os metodos de crud, insert update, delete, select
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Historico> Historico { get; set; }

        ///<summary>
        ///Depois que realizar todo o mapeamento das entidades, configurar as classes Maps, é so executar os comandos descritos abaixo, onde será preparado um script para criação de banco e seus atributos
        ///defina o processo de dados como principal, acesse ferramentas/gerenciar pacote de nuget/console de gerenciamento de pacote. sete o projeto de dados
        ///PM> Add-Migration
        ///este comando acima é para você executar o migrations inicial, sua resposta será descrita na linha debaixo
        ///PM> Build started...
        ///PM> Build succeeded.
        ///PM> To undo this action, use Remove-Migration.
        ///para executar a criação das tabelas, deve ser executado o camando abaixo
        ///PM> Update-Database
        ///resultado esperado atravez do codigo acima.
        ///PM> Build started...
        ///PM> Build succeeded.
        ///PM> Applying migration '20230424055138_Initial'.
        ///PM> Done.
        ///</summary>

        ///<summary>
        ///atualização de campo da tabela - linhas de exemplo
        ///defina o processo de dados como principal, acesse ferramentas/gerenciar pacote de nuget/console de gerenciamento de pacote. sete o projeto de dados
        ///PM> Add-Migration AddTableHistorico
        ///resultado
        ///PM> Build started...
        ///PM> Build succeeded.
        ///PM> To undo this action, use Remove-Migration.
        ///PM> Update-Database
        ///realiza a atualização da tabela, resultado
        ///PM> Build started...
        ///PM> Build succeeded.
        ///PM> Applying migration '20230424064247_AddTableHistorico'.
        ///PM> Done.
        ///</summary>
    }
}
