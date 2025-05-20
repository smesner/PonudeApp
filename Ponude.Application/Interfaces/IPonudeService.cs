using Ponude.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Interfaces
{
    public interface IPonudeService
    {
        Task<PonudaDTO> CreateAsync(PonudaCreateDTO ponuda, CancellationToken token);
        Task<PonudaDTO> GetByIdAsync(Guid id, CancellationToken token);
        Task<PonudaDTO> GetByBrojPonudeAsync(int brojPonude, CancellationToken token);
        Task<PonudeByPageDTO> GetPonudeByPageAsync(int pageNumber, int pageSize, CancellationToken token);
        Task<PonudaDTO> UpdateAsync(Guid id, PonudaDTO ponudaDTO, CancellationToken token);
        Task<bool> DeleteAsync(Guid id, CancellationToken token);
    }
}
