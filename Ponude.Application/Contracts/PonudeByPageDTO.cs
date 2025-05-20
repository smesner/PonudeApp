using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Contracts
{
    public class PonudeByPageDTO
    {
        public IEnumerable<PonudaShortDTO> PonudaPage {get; init;} = Enumerable.Empty<PonudaShortDTO>();

        public int PageIndex { get; init; }
        public int Total {  get; init;}
    }

    public class PonudaShortDTO
    {
        public Guid Id { get; set; }
        public int BrojPonude { get; set; }
        public DateOnly Datum { get; set; }
        public decimal IznosPonude { get; set; }
    }
}
