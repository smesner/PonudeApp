using Ponude.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Interfaces
{
    public interface IArtikliService
    {
        Task<ArtiklDTO> CreateAsync(ArtiklCreateDTO artikl, CancellationToken token);
        Task<ArtiklDTO> GetByIdAsync(Guid id, CancellationToken token);
        Task<List<ArtiklDTO>> GetAllAsync(CancellationToken token);
        Task<List<ArtiklDTO>> SearchAsync(string query, CancellationToken token);
        Task<ArtiklDTO> UpdateAsync(Guid id, ArtiklDTO artikl, CancellationToken token);
        Task<bool> DeleteAsync(Guid id, CancellationToken token);
    }
}
