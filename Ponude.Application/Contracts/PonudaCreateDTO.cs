using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Contracts
{
    public class PonudaCreateDTO
    {
        public DateOnly? Datum { get; set; }
        public decimal IznosPonude { get; set; }
        public List<StavkaCreateDTO> Stavke { get; set; } = new();
    }

    public class StavkaCreateDTO
    {
        public required string Artikl { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public decimal UkupnaCijena { get; set; }
    }
}
