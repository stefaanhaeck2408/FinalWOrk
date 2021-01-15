using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class IngevoerdAntwoordMapper
    {
        public static IngevoerdAntwoordDTO MapIngevoerdAntwoordModelToIngevoerdAntwoordDTO(IngevoerdAntwoord ingevoerdAntwoord)
        {
            if (ingevoerdAntwoord == null)
            {
                throw new NullReferenceException("IngevoerdAntwoord object is null");
            }
            return new IngevoerdAntwoordDTO
            {
                GescoordeScore = ingevoerdAntwoord.GescoordeScore,
                Id = ingevoerdAntwoord.Id,
                JsonAntwoord = ingevoerdAntwoord.JsonAntwoord,                
                TeamId = ingevoerdAntwoord.TeamId,                
                VraagId = ingevoerdAntwoord.VraagId

            };
        }

        public static IngevoerdAntwoord MapIngevoerdAntwoordDTOToIngevoerdAntwoordModel(IngevoerdAntwoordDTO ingevoerdAntwoordDTO)
        {
            if (ingevoerdAntwoordDTO == null)
            {
                throw new NullReferenceException("IngevoerdAntwoordDTO is null");
            }
            return new IngevoerdAntwoord
            {
                GescoordeScore = ingevoerdAntwoordDTO.GescoordeScore,
                Id = ingevoerdAntwoordDTO.Id,
                JsonAntwoord = ingevoerdAntwoordDTO.JsonAntwoord,
                TeamId = ingevoerdAntwoordDTO.TeamId,
                VraagId = ingevoerdAntwoordDTO.VraagId
                
            };
        }
    }
}
