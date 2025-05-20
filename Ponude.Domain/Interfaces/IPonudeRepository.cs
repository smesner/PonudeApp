using Ponude.Domain.Entities;
using Ponude.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Domain.Interfaces
{
    public interface IPonudeRepository
    {
        Task<bool> CreateAsync(Ponuda ponuda, CancellationToken token);
        Task<Ponuda> GetByIdAsync(Guid id, CancellationToken token);
        Task<Ponuda> GetByBrojPonudeAsync(int brojPonude, CancellationToken token);
        Task<PonudeByPage> GetPonudeByPageAsync(int pageNumber, int pageSize, CancellationToken token);
        Task<bool> UpdateAsync(Ponuda ponuda, CancellationToken token);
        Task<bool> DeleteAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(int brojPonude, CancellationToken token);
        Task<int> LastBrojPonudeAsync(CancellationToken token);
    }
}
