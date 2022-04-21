using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrocaEndereco.Models;

namespace TrocaEndereco.Data.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(p => p.Local);

            builder.Property(p => p.Produto)
                   .IsRequired()
                   .HasColumnType("varchar(6)");

            builder.Property(p => p.Saldo)
                   .IsRequired()
                   .HasColumnType("varchar(100)");
        }
    }
}
