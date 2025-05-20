using FluentValidation;
using Ponude.Application.Contracts;
using Ponude.Application.Interfaces;
using Ponude.Application.Mappers;
using Ponude.Domain.Entities;
using Ponude.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Services
{
    public class PonudeService : IPonudeService
    {
        private readonly IPonudeRepository _repository;
        private readonly IValidator<PonudaCreateDTO> _ponudaCreateValidator;
        private readonly IValidator<PonudaDTO> _ponudaValidator;

        public PonudeService(IPonudeRepository repository, IValidator<PonudaCreateDTO> ponudaCreateValidator, IValidator<PonudaDTO> ponudaValidator)
        {
            _repository = repository;
            _ponudaCreateValidator = ponudaCreateValidator;
            _ponudaValidator = ponudaValidator;
        }

        public async Task<PonudaDTO> CreateAsync(PonudaCreateDTO ponudaCreateDTO, CancellationToken token)
        {
            await _ponudaCreateValidator.ValidateAndThrowAsync(ponudaCreateDTO, token);
            var ponuda = new Ponuda
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };

            var lastBrojPonude = await _repository.LastBrojPonudeAsync(token);
            ponuda.BrojPonude = lastBrojPonude + 1;
            ponuda.Datum = DateOnly.FromDateTime(DateTime.UtcNow);

            foreach (var stavkaCreateDTO in ponudaCreateDTO.Stavke)
            {
                var novaStavka = new Stavka
                {
                    Id = Guid.NewGuid(),
                    Artikl = stavkaCreateDTO.Artikl,
                    Cijena = stavkaCreateDTO.Cijena,
                    Kolicina = stavkaCreateDTO.Kolicina,
                    UkupnaCijena = StavkaUkupnaCijena(stavkaCreateDTO.Cijena, stavkaCreateDTO.Kolicina),
                };
                ponuda.Stavke.Add(novaStavka);
            }

            if (ponuda.Stavke.Count > 0)
                ponuda.IznosPonude = ponuda.Stavke.Sum(s => s.UkupnaCijena);
            else
                ponuda.IznosPonude = 0m;

            var result = await _repository.CreateAsync(ponuda, token);
            if (!result) 
                return null!;

            return ponuda.ToDTO();
        }

        public async Task<PonudaDTO> GetByIdAsync(Guid id, CancellationToken token)
        {
            var ponuda = await _repository.GetByIdAsync(id, token);
            if (ponuda == null)
                return null!;

            return ponuda.ToDTO();
        }

        public async Task<PonudaDTO> GetByBrojPonudeAsync(int brojPonude, CancellationToken token)
        {
            var ponuda = await _repository.GetByBrojPonudeAsync(brojPonude, token);
            if (ponuda == null)
                return null!;

            return ponuda.ToDTO();
        }

        public async Task<PonudeByPageDTO> GetPonudeByPageAsync(int pageNumber, int pageSize, CancellationToken token)
        {
            var ponudaByPage = await _repository.GetPonudeByPageAsync(pageNumber, pageSize, token);

            if (ponudaByPage == null)
                return null!;

            return ponudaByPage.ToDTO();

        }

        public async Task<PonudaDTO> UpdateAsync(Guid id, PonudaDTO ponudaDTO, CancellationToken token)
        {
            await _ponudaValidator.ValidateAndThrowAsync(ponudaDTO, token);

            var ponuda = await _repository.GetByIdAsync(id, token);

            if (ponuda == null)
                return null!;

            UpdateEntity(ponuda, ponudaDTO, token);

            var result = await _repository.UpdateAsync(ponuda, token);
           
            return ponuda.ToDTO();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
        {
            return
                await _repository.DeleteAsync(id, token);
        }

        private void UpdateEntity(Ponuda ponuda, PonudaDTO ponudaDTO, CancellationToken token)
        {
            ponuda.IznosPonude = ponudaDTO.IznosPonude;
            ponuda.LastUpdated = DateTime.UtcNow;

            var stavkeIds = ponudaDTO.Stavke
                .Where(s => s.Id.HasValue)
                .Select(s => s.Id.Value)
                .ToHashSet();

            var stavkeRemove = ponuda.Stavke
                .Where(s => !stavkeIds
                .Contains(s.Id))
                .ToList();

            foreach(var stavka in stavkeRemove)
            {
                ponuda.Stavke.Remove(stavka);
            }

            foreach(var stavkaDTO in ponudaDTO.Stavke)
            {
                if (stavkaDTO.Id.HasValue)
                {
                    var stavka = ponuda.Stavke.FirstOrDefault(s => s.Id == stavkaDTO.Id);

                    if (stavka != null)
                    {
                        if (!string.IsNullOrWhiteSpace(stavkaDTO.Artikl))
                            stavka.Artikl = stavkaDTO.Artikl;

                        stavka.Cijena = stavkaDTO.Cijena;
                        stavka.Kolicina = stavkaDTO.Kolicina;
                        stavka.UkupnaCijena = StavkaUkupnaCijena(stavkaDTO.Cijena, stavkaDTO.Kolicina);
                    }
                }
                else
                {
                    var novaStavka = new Stavka
                    {
                        Id = Guid.NewGuid(),
                        Artikl = stavkaDTO.Artikl,
                        Cijena = stavkaDTO.Cijena,
                        Kolicina = stavkaDTO.Kolicina,
                        UkupnaCijena = StavkaUkupnaCijena(stavkaDTO.Cijena, stavkaDTO.Kolicina),
                    };
                    ponuda.Stavke.Add(novaStavka);
                }
            }

            if (ponuda.Stavke.Count > 0)
                ponuda.IznosPonude = ponuda.Stavke.Sum(s => s.UkupnaCijena);
            else
                ponuda.IznosPonude = 0m;
        }

        private decimal StavkaUkupnaCijena(decimal cijena, int kolicina)
        {
            return cijena * kolicina;
        }
    }
}
