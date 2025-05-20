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
    public class ArtiklConfiguration : IEntityTypeConfiguration<Artikl>
    {
        public void Configure(EntityTypeBuilder<Artikl> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Naziv).HasMaxLength(100);
        }
    }
}
