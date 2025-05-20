using Microsoft.EntityFrameworkCore;
using Ponude.Domain.Entities;
using Ponude.Domain.Interfaces;
using Ponude.Domain.Models;
using Ponude.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Infrastructure.Repository
{
    public class PonudeRepository : IPonudeRepository
    {
        private readonly PonudeDbContext _context;

        public PonudeRepository(PonudeDbContext context) => _context = context;

        public async Task<bool> CreateAsync(Ponuda ponuda, CancellationToken token)
        {
            _context.Ponude.Add(ponuda);
            var result = await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<Ponuda> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Ponude
                .Include(s => s.Stavke)
                .FirstOrDefaultAsync(p => p.Id == id, token);
        }

        public async Task<Ponuda> GetByBrojPonudeAsync(int brojPonude, CancellationToken token)
        {
            return await _context.Ponude
                .Include(s => s.Stavke)
                .FirstOrDefaultAsync(p => p.BrojPonude == brojPonude, token);
        }

        public async Task<PonudeByPage> GetPonudeByPageAsync(int pageNumber, int pageSize, CancellationToken token)
        {
            var total = await _context.Ponude.AsNoTracking().CountAsync(token);

            return new PonudeByPage
            {
                Total = total,
                PageIndex = pageNumber,
                PageSize = pageSize,
                Ponude = await _context.Ponude
                            .AsNoTracking()
                            .OrderBy(p => p.BrojPonude)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .Select(p => new PonudaShort
                            {
                                Id = p.Id,
                                BrojPonude = p.BrojPonude,
                                Datum = p.Datum,
                                IznosPonude = p.IznosPonude
                            })
                            .ToListAsync(token)
            };

        }

        public async Task<bool> UpdateAsync(Ponuda ponuda, CancellationToken token)
        {
            //foreach (var stavka in ponuda.Stavke)
            //{
            //    _context.Entry(stavka).State = await _context.PonudeStavke
            //                                        .AnyAsync(s => s.Id == stavka.Id, token) 
            //                                        ? EntityState.Modified : EntityState.Added;
            //}
            var result = await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
        {
            var ponuda = await _context.Ponude
                                .Include(p => p.Stavke)
                                .FirstOrDefaultAsync(p => p.Id == id, token);
            if(ponuda is null) return false;

            _context.Ponude.Remove(ponuda);
            var result = await _context.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        {
            return 
                await _context.Ponude
                        .AsNoTracking()
                        .AnyAsync(p => p.Id == id, token);
        }

        public async Task<bool> ExistsAsync(int brojPonude, CancellationToken token)
        {
            return 
                await _context.Ponude
                        .AsNoTracking()
                        .AnyAsync(p => p.BrojPonude == brojPonude, token);
        }

        public async Task<int> LastBrojPonudeAsync(CancellationToken token)
        {
            if(!_context.Ponude.Any())
                return 0;
            return await _context.Ponude
                            .AsNoTracking()
                            .MaxAsync(p => p.BrojPonude, token);
        }
    }
}
