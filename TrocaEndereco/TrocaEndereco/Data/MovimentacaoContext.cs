using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaEndereco.Models;

namespace TrocaEndereco.Data
{
    public class MovimentacaoContext : DbContext
    {

        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Endereco> Endereco { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=CAsa\\SQLEXPRESS;Initial Catalog=MOVIMENTACAO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //O fluent API vai varrer todo assembly em busca das classes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovimentacaoContext).Assembly);
        }

    }
}
