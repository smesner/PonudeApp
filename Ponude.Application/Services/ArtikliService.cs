using Ponude.Application.Contracts;
using Ponude.Application.Interfaces;
using Ponude.Application.Mappers;
using Ponude.Domain.Entities;
using Ponude.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Services
{
    public class ArtikliService : IArtikliService
    {
        private readonly IArtikliRepository _repository;

        public ArtikliService(IArtikliRepository repository) => _repository = repository;

        public async Task<ArtiklDTO> CreateAsync(ArtiklCreateDTO artiklCreateDTO, CancellationToken token)
        {
            var exists = await _repository.ExistsAsync(artiklCreateDTO.Naziv, token);
            if (exists)
                return null;

            var artikl = new Artikl
            {
                Id = Guid.NewGuid(),
                Naziv = artiklCreateDTO.Naziv
            };

            var result = await _repository.CreateAsync(artikl, token);
            if (!result)
                return null;

            return artikl.ToDTO();
        }

        public async Task<ArtiklDTO> GetByIdAsync(Guid id, CancellationToken token)
        {
            var artikl = await _repository.GetByIdAsync(id, token);
            if (artikl is null)
                return null!;

            return artikl.ToDTO();
        }

        public async Task<List<ArtiklDTO>> GetAllAsync(CancellationToken token)
        {
            var artikli = await _repository.GetAllAsync(token);

            if (artikli is null)
                return null!;

            return artikli.Select(a => a.ToDTO()).ToList();
        }

        public async Task<List<ArtiklDTO>> SearchAsync(string query, CancellationToken token)
        {
            var artikli = await _repository.SearchAsync(query, token);
            if (artikli is null)
                return null!;

            return artikli.Select(a => a.ToDTO()).ToList();
        }

        public async Task<ArtiklDTO> UpdateAsync(Guid id, ArtiklDTO artiklDTO, CancellationToken token)
        {
            var artikl = await _repository.GetByIdAsync(id, token);
            if (artikl is null)
                return null!;

            artikl.Naziv = artiklDTO.Naziv;
            var result = _repository.UpdateAsync(artikl, token);
            
            return artikl.ToDTO();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
        {
            return
                await _repository.DeleteAsync(id, token);
        }
    }
}
