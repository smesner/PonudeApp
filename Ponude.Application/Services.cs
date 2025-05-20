
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Ponude.Application.Interfaces;
using Ponude.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application
{
    public static class DIServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPonudeService, PonudeService>();
            services.AddScoped<IArtikliService, ArtikliService>();
            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Scoped);

            return services;
        }
    }
}
