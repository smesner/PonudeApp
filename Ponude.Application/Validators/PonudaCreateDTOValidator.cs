using FluentValidation;
using Ponude.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Validators
{
    public class PonudaCreateDTOValidator : AbstractValidator<PonudaCreateDTO>
    {
        public PonudaCreateDTOValidator()
        {
            RuleFor(p => p.Datum).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
            RuleFor(p => p.IznosPonude).NotEmpty().GreaterThan(0m);
            RuleForEach(s => s.Stavke).ChildRules(stavka =>
            {
                stavka.RuleFor(p => p.Artikl).NotEmpty().WithMessage("Naziv artikla mora biti prisutan.");
                stavka.RuleFor(p => p.Cijena).NotEmpty();
                stavka.RuleFor(p => p.Kolicina).NotEmpty().GreaterThan(0);
            });
        }
    }
}
