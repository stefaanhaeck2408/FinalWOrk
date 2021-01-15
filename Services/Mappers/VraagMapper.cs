using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class VraagMapper
    {
        public static VraagDTO MapVraagModelToVraagDTO(Vraag vraag)
        {
            if (vraag == null)
            {
                throw new NullReferenceException("Vraag object is null");
            }
            return new VraagDTO
            {
                Id = vraag.Id,
                MaxScoreVraag = vraag.MaxScoreVraag,
                //TypeVraagDTO = vraag.TypeVraag,
                TypeVraagId = vraag.TypeVraagId,
                VraagStelling = vraag.VraagStelling,
                JsonCorrecteAntwoord = vraag.JsonCorrecteAntwoord,
                JsonMogelijkeAntwoorden = vraag.JsonMogelijkeAntwoorden,
                UpdatedAt = vraag.UpdatedAt

            };
        }

        public static Vraag MapVraagDTOToVraagModel(VraagDTO vraagDTO)
        {
            if (vraagDTO == null)
            {
                throw new NullReferenceException("VraagDTO is null");
            }
            return new Vraag
            {
                Id = vraagDTO.Id,
                MaxScoreVraag = vraagDTO.MaxScoreVraag,
                //TypeVraag = (TypeVraag)vraagDTO.TypeVraagDTO,
                TypeVraagId = vraagDTO.TypeVraagId,
                VraagStelling = vraagDTO.VraagStelling,
                JsonCorrecteAntwoord = vraagDTO.JsonCorrecteAntwoord,
                JsonMogelijkeAntwoorden = vraagDTO.JsonMogelijkeAntwoorden,
                

            };
        }
    }
}
