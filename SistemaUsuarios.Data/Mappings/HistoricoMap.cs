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
    public class HistoricoMap : IEntityTypeConfiguration<Historico>
    {
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            //criando o nome da tabela ou acessando o nome da tabela
            builder.ToTable("HISTORICO");

            //chave primaria da tabela
            builder.HasKey(u => u.IdHistorico);

            //mapeamento dos campos da tabela
            builder.Property(h => h.IdHistorico).HasColumnName("IDHISTORICO").IsRequired();
            builder.Property(h => h.IdUsuario).HasColumnName("IDUSUARIO").IsRequired();
            builder.Property(h => h.Operacao).HasColumnName("OPERACAO").HasMaxLength(50).IsRequired();
            builder.Property(h => h.Registro).HasColumnName("REGISTRO").IsRequired();
            builder.Property(h => h.Detalhes).HasColumnName("DETALHES").HasMaxLength(500).IsRequired();

            //mapeamento de 1 relacionamento, 1 para N
            builder.HasOne(h => h.Usuario) //historico tem 1 usuario
                   .WithMany(u => u.Historicos) //usuario tem n histroricos
                   .HasForeignKey(h => h.IdUsuario);
        }
    }
}
