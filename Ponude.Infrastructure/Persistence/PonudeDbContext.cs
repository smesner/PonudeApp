using Microsoft.EntityFrameworkCore;
using Ponude.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Infrastructure.Persistence
{
    public class PonudeDbContext : DbContext
    {
        public PonudeDbContext(DbContextOptions<PonudeDbContext> options) : base(options)
        {
        }

        public DbSet<Ponuda> Ponude {  get; set; }
        public DbSet<Stavka> PonudeStavke { get; set; }
        public DbSet<Artikl> Artikli {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PonudeDbContext).Assembly);
        }
    }
}
