using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Domain.Models
{
    public class PonudeByPage
    {
        public ICollection<PonudaShort> Ponude { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }

    public class PonudaShort
    {
        public Guid Id { get; set; }
        public int BrojPonude { get; set; }
        public DateOnly Datum { get; set; }
        public decimal IznosPonude { get; set; }
    }

}
