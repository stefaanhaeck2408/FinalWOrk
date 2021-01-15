using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class RondeMapper
    {
        public static RondeDTO MapRondeModelToRondeDTO(Ronde ronde)
        {
            if (ronde == null)
            {
                throw new NullReferenceException("ronde object is null");
            }
            return new RondeDTO
            {
                Id = ronde.Id,
                Naam = ronde.Naam,
                UpdatedAt = ronde.UpdatedAt

            };
        }

        public static Ronde MapRondeDTOToRondeModel(RondeDTO rondeDTO)
        {
            if (rondeDTO == null)
            {
                throw new NullReferenceException("RondeDTO is null");
            }
            return new Ronde
            {
                Id = rondeDTO.Id,
                Naam = rondeDTO.Naam,

            };
        }
    }
}
