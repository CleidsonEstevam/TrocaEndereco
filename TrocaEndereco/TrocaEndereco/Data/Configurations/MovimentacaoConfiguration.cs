using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaEndereco.Models;

namespace TrocaEndereco.Data.Configurations
{
    public class MovimentacaoConfiguration : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.ToTable("Movimentacao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.EnderecoOrigem)
                   .IsRequired()
                   .HasColumnType("varchar(12)");

            builder.Property(p => p.EnderecoDestino)
                   .IsRequired()
                   .HasColumnType("varchar(12)");

            builder.Property(p => p.Produto)
                   .IsRequired()
                   .HasColumnType("varchar(8)");

            builder.Property(p => p.Quantidade)
                   .IsRequired()
                   .HasColumnType("varchar(20)");
        }
    }
}
