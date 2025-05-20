using Ponude.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Domain.Interfaces
{
    public interface IArtikliRepository
    {
        Task<bool> CreateAsync(Artikl artikl, CancellationToken token);
        Task<Artikl> GetByIdAsync(Guid id, CancellationToken token);
        Task<List<Artikl>> GetAllAsync(CancellationToken token);
        Task<List<Artikl>> SearchAsync(string query, CancellationToken token);
        Task<bool> UpdateAsync(Artikl artikl, CancellationToken token);
        Task<bool> DeleteAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(string Naziv, CancellationToken token);
    }
}
