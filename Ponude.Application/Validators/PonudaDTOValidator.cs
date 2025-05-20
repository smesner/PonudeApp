using FluentValidation;
using Ponude.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Validators
{
    public class PonudaDTOValidator : AbstractValidator<PonudaDTO>
    {
        public PonudaDTOValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.BrojPonude).NotEmpty().ExclusiveBetween(0, int.MaxValue);
            RuleFor(p => p.Datum).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
            RuleFor(p => p.IznosPonude).NotEmpty().GreaterThan(0m);
            RuleFor(p => p.Stavke.Count).GreaterThan(0);
            RuleForEach(s => s.Stavke).ChildRules(stavka =>
            {
                stavka.RuleFor(p => p.Artikl).NotEmpty().WithMessage("Naziv artikla mora biti prisutan.");
                stavka.RuleFor(p => p.Cijena).NotEmpty();
                stavka.RuleFor(p => p.Kolicina).NotEmpty().GreaterThan(0);
            });
        }
    }

}
