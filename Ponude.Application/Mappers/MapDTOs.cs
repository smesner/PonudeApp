using Ponude.Application.Contracts;
using Ponude.Domain.Entities;
using Ponude.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponude.Application.Mappers
{
    public static class MapDTOs
    {

        public static PonudaDTO ToDTO(this Ponuda ponuda)
        {
            return new PonudaDTO
            {
                Id = ponuda.Id,
                BrojPonude = ponuda.BrojPonude,
                Datum = ponuda.Datum,
                IznosPonude = ponuda.IznosPonude,
                Stavke = ponuda.Stavke.Select(s =>
                    new StavkaDTO
                    {
                        Id = s.Id,
                        Artikl = s.Artikl,
                        Cijena = s.Cijena,
                        Kolicina = s.Kolicina,
                        UkupnaCijena = s.UkupnaCijena
                    }).ToList()
            };
        }

        public static Ponuda ToEntity(this PonudaDTO ponudaDTO)
        {
            return new Ponuda
            {
                Id = (Guid)ponudaDTO.Id.Value,
                IznosPonude = ponudaDTO.IznosPonude,
                LastUpdated = DateTime.UtcNow,
                Stavke = ponudaDTO.Stavke.Select(stavkaDTO => new Stavka
                {
                    Id = stavkaDTO.Id is null ? Guid.NewGuid() : stavkaDTO.Id.Value,
                    Artikl = stavkaDTO.Artikl,
                    Cijena = stavkaDTO.Cijena,
                    Kolicina = stavkaDTO.Kolicina,
                    UkupnaCijena = stavkaDTO.UkupnaCijena
                }).ToList()
            };
        }

        public static PonudaDTO ToDTO(this PonudaCreateDTO ponudaCreateDTO)
        {
            return new PonudaDTO
            {
                Datum = ponudaCreateDTO.Datum,
                IznosPonude = ponudaCreateDTO.IznosPonude,
                Stavke = ponudaCreateDTO.Stavke.Select(s => new StavkaDTO
                {
                    Artikl = s.Artikl,
                    Cijena = s.Cijena,
                    Kolicina = s.Kolicina,
                    UkupnaCijena = s.UkupnaCijena
                }).ToList()
            };
        }

        public static PonudeByPageDTO ToDTO(this PonudeByPage ponudaCreateDTO)
        {
            return new PonudeByPageDTO
            {
                Total = ponudaCreateDTO.Total,
                PageIndex = ponudaCreateDTO.PageIndex,
                PonudaPage = ponudaCreateDTO.Ponude.Select(
                    p => new PonudaShortDTO { 
                        Id = p.Id,
                        BrojPonude = p.BrojPonude,
                        Datum = p.Datum,
                        IznosPonude = p.IznosPonude
                    })
            };
        }

        public static ArtiklDTO ToDTO(this Artikl artikl)
        {
            return new ArtiklDTO
            {
                Id = artikl.Id,
                Naziv = artikl.Naziv
            };
        }
    }
}
