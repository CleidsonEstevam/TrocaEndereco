using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaEndereco.Models;

namespace TrocaEndereco.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Descricao)
                   .IsRequired()
                   .HasColumnType("varchar(150)");

            builder.Property(p => p.Fornecedor)
                   .IsRequired()
                   .HasColumnType("varchar(30)");
        }
    }
}
