using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ponude.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Infrastructure.Persistence.Configurations
{
    public class PonudaConfiguration : IEntityTypeConfiguration<Ponuda>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Ponuda> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.BrojPonude).HasMaxLength(20);
            builder.HasIndex(p => p.BrojPonude).IsUnique();
            builder.Property(p => p.Datum).IsRequired();
            builder.Property(p => p.IznosPonude).IsRequired().HasPrecision(18, 2);
            builder.Property(p => p.Created).HasDefaultValueSql("GETUTCDATE()");
        }
    }

    public class StavkaConfiguration : IEntityTypeConfiguration<Stavka>
    {
        public void Configure(EntityTypeBuilder<Stavka> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Artikl).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Cijena).IsRequired().HasPrecision(18, 2);
            builder.Property(s => s.Kolicina).IsRequired();
            builder.Property(s => s.UkupnaCijena).IsRequired().HasPrecision(18, 2);
        }
    }
}
