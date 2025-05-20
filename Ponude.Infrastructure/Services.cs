using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Ponude.Domain.Interfaces;
using Ponude.Infrastructure.Persistence;
using Ponude.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Infrastructure
{
    public static class DIServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PonudeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PonudeDBConnection")));
            
            services.AddScoped<IPonudeRepository, PonudeRepository>();
            services.AddScoped<IArtikliRepository, ArtikliRepository>();

            return services;
        }
    }
}
