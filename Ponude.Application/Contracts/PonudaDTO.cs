using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Contracts
{
    public class PonudaDTO
    {
        public Guid? Id { get; set; }
        public int? BrojPonude { get; set; }
        public DateOnly? Datum { get; set; }
        public decimal IznosPonude { get; set; }
        public List<StavkaDTO> Stavke { get; set; } = new();
    }

    public class StavkaDTO
    {
        public Guid? Id { get; set; }
        public string Artikl { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public decimal UkupnaCijena { get; set; }
    }
}
