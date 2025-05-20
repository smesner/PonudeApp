using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Domain.Entities
{
    public class Ponuda
    {
        public Guid Id { get; set; }
        public int BrojPonude { get; set; }
        public DateOnly Datum {  get; set; }
        public decimal IznosPonude { get; set; }
        public List<Stavka> Stavke { get; set; } = new();

        public DateTime? Created {  get; set; }
        public DateTime? LastUpdated { get; set; }
    }

    public class Stavka
    {
        public Guid Id { get; set; }
        public string Artikl { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public decimal UkupnaCijena { get; set; }

        public Guid PonudaId { get; set; }
        public Ponuda Ponuda { get; set; }
    }
}
