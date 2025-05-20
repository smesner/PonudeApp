using Microsoft.EntityFrameworkCore;
using Ponude.Domain.Entities;
using Ponude.Domain.Interfaces;
using Ponude.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Infrastructure.Repository
{
    public class ArtikliRepository : IArtikliRepository
    {
        private readonly PonudeDbContext _context;

        public ArtikliRepository(PonudeDbContext context) => _context = context;

        public async Task<bool> CreateAsync(Artikl artikl, CancellationToken token)
        {
            _context.Artikli.Add(artikl);
            var result = await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public Task<Artikl> GetByIdAsync(Guid id, CancellationToken token)
        {
            return _context.Artikli
                .FirstOrDefaultAsync(a => a.Id == id, token);
        }

        public Task<List<Artikl>> GetAllAsync(CancellationToken token)
        {
            return _context.Artikli
                .AsNoTracking()
                .ToListAsync(token);
        }

        public Task<List<Artikl>> SearchAsync(string query, CancellationToken token)
        {
            return _context.Artikli
                .AsNoTracking()
                .Where(a => a.Naziv.Contains(query))
                .ToListAsync(token);
        }

        public async Task<bool> UpdateAsync(Artikl artikl, CancellationToken token)
        {
            _context.Artikli.Update(artikl);
            var result = await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
        {
            var artikl = await _context.Artikli
                .FirstOrDefaultAsync(a => a.Id == id, token);
            if(artikl is null)
                return false;

            _context.Artikli.Remove(artikl);
            var result = await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        {
            return 
                await _context.Artikli
                .AsNoTracking()
                .AnyAsync(a => a.Id == id, token);
        }

        public async Task<bool> ExistsAsync(string naziv, CancellationToken token)
        {
            return
                await _context.Artikli
                .AsNoTracking()
                .AnyAsync(a => a.Naziv == naziv, token);
        }
    }
}
