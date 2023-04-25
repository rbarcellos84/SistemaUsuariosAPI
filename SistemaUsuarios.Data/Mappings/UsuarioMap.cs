using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaUsuarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Data.Mappings
{
    ///<summary>
    ///mapeamento da entidade
    ///</summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        //metodo para realizar o mapeamento da entidade
        public void Configure(EntityTypeBuilder<Usuario>builder)
        {
            //criando o nome da tabela ou acessando o nome da tabela
            builder.ToTable("USUARIO");

            //chave primaria da tabela
            builder.HasKey(u => u.IdUsuario);

            //mapeamento dos campos da tabela
            builder.Property(u => u.IdUsuario).HasColumnName("IDUSUARIO").IsRequired();
            builder.Property(u => u.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            builder.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(150).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Senha).HasColumnName("SENHA").HasMaxLength(50).IsRequired();
            builder.Property(u => u.DataCadastro).HasColumnName("DATACRIACAO").IsRequired();
            builder.Property(u => u.DataAtualizacao).HasColumnName("DATAATUALIZACAO");
        }
    }
}
