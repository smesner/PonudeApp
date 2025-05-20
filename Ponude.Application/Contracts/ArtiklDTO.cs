using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Contracts
{
    public class ArtiklDTO
    {
        public Guid Id { get; init; }
        public string Naziv { get; set; }
    }
}
