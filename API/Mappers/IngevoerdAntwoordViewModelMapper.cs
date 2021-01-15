using API.Viewmodels.IngevoerdAntwoord;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class IngevoerdAntwoordViewModelMapper
    {
        public static IngevoerdAntwoordViewModelResponse MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelResponse(IngevoerdAntwoordDTO ingevoerdAntwoordDTO)
        {
            if (ingevoerdAntwoordDTO == null)
            {
                throw new NullReferenceException("ingevoerdAntwoordDTO object is null");
            }
            return new IngevoerdAntwoordViewModelResponse
            {
                GescoordeScore = ingevoerdAntwoordDTO.GescoordeScore,
                Id = ingevoerdAntwoordDTO.Id,
                JsonAntwoord = ingevoerdAntwoordDTO.JsonAntwoord,
                TeamId = ingevoerdAntwoordDTO.TeamId,
                VraagId = ingevoerdAntwoordDTO.VraagId,
                UpdatedAt = ingevoerdAntwoordDTO.UpdatedAt

            };
        }

        public static IngevoerdAntwoordDTO MapIngevoerdModelViewModelResponseToIngevoerdAntwoordDTO(IngevoerdAntwoordViewModelResponse ingevoerdviewmodel)
        {
            if (ingevoerdviewmodel == null)
            {
                throw new NullReferenceException("ingevoerdviewmodel is null");
            }
            return new IngevoerdAntwoordDTO
            {
                GescoordeScore = ingevoerdviewmodel.GescoordeScore,
                //Id = ingevoerdviewmodel.Id,
                JsonAntwoord = ingevoerdviewmodel.JsonAntwoord,
                TeamId = ingevoerdviewmodel.TeamId,
                VraagId = ingevoerdviewmodel.VraagId,
                UpdatedAt = ingevoerdviewmodel.UpdatedAt

            };
        }

        public static IngevoerdAntwoordViewModelRequest MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelRequest(IngevoerdAntwoordDTO ingevoerdAntwoordDTO)
        {
            if (ingevoerdAntwoordDTO == null)
            {
                throw new NullReferenceException("ingevoerdAntwoordDTO object is null");
            }
            return new IngevoerdAntwoordViewModelRequest
            {
                GescoordeScore = ingevoerdAntwoordDTO.GescoordeScore,
                //Id = ingevoerdAntwoordDTO.Id,
                Antwoord = ingevoerdAntwoordDTO.JsonAntwoord,
                TeamId = ingevoerdAntwoordDTO.TeamId,
                VraagId = ingevoerdAntwoordDTO.VraagId

            };
        }

        public static IngevoerdAntwoordDTO MapIngevoerdModelViewModelRequestToIngevoerdAntwoordDTO(IngevoerdAntwoordViewModelRequest ingevoerdviewmodel)
        {
            if (ingevoerdviewmodel == null)
            {
                throw new NullReferenceException("ingevoerdviewmodel is null");
            }
            return new IngevoerdAntwoordDTO
            {
                GescoordeScore = ingevoerdviewmodel.GescoordeScore,
                //Id = ingevoerdviewmodel.Id,
                JsonAntwoord = ingevoerdviewmodel.Antwoord,
                TeamId = ingevoerdviewmodel.TeamId,
                VraagId = ingevoerdviewmodel.VraagId

            };
        }
    }
}
