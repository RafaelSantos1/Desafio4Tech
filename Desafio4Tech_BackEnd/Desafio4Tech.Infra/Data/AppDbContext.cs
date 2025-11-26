

using Desafio4Tech.Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio4Tech.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PlanoModel> Planos { get; set; }
        public DbSet<BeneficiarioModel> Beneficiarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BeneficiarioModel>()
              .HasOne(b => b.Plano)
              .WithMany(p => p.Beneficiarios)
              .HasForeignKey(b => b.IdPlano)
              .OnDelete(DeleteBehavior.Restrict); // ou Cascade, se preferir
        }

    }
}
