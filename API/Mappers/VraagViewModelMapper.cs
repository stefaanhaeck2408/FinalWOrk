using API.Viewmodels.Vragen;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class VraagViewModelMapper
    {
        public static VragenViewModelReponse MapVraagDTOToVraagViewModel(VraagDTO vraagDTO)
        {
            if (vraagDTO == null)
            {
                throw new NullReferenceException("vraagDTO object is null");
            }
            return new VragenViewModelReponse
            {
                Id = vraagDTO.Id,
                MaxScoreVraag = vraagDTO.MaxScoreVraag,
                TypeVraagId = vraagDTO.TypeVraagId,
                VraagStelling = vraagDTO.VraagStelling,
                JsonCorrecteAntwoord = vraagDTO.JsonCorrecteAntwoord,
                JsonMogelijkeAntwoorden = vraagDTO.JsonMogelijkeAntwoorden,
                UpdatedAt = vraagDTO.UpdatedAt

            };
        }

        public static VraagDTO MapViewModelToAntwoordDTO(VragenViewModelReponse viewmodel)
        {
            if (viewmodel == null)
            {
                throw new NullReferenceException("vraag model is null");
            }
            return new VraagDTO
            {
                Id = viewmodel.Id,
                MaxScoreVraag = viewmodel.MaxScoreVraag,
                TypeVraagId = viewmodel.TypeVraagId,
                JsonCorrecteAntwoord = viewmodel.JsonCorrecteAntwoord,
                JsonMogelijkeAntwoorden = viewmodel.JsonMogelijkeAntwoorden,
                VraagStelling = viewmodel.VraagStelling,
                UpdatedAt = viewmodel.UpdatedAt

            };
        }

        public static VragenViewModelRequest MapVraagDTOToVraagViewModelRequest(VraagDTO vraagDTO)
        {
            if (vraagDTO == null)
            {
                throw new NullReferenceException("vraagDTO object is null");
            }
            return new VragenViewModelRequest
            {
                //Id = vraagDTO.Id,
                MaxScoreVraag = vraagDTO.MaxScoreVraag,
                TypeVraagId = vraagDTO.TypeVraagId,
                VraagStelling = vraagDTO.VraagStelling,
                JsonCorrecteAntwoord = vraagDTO.JsonCorrecteAntwoord,
                JsonMogelijkeAntwoorden = vraagDTO.JsonMogelijkeAntwoorden

            };
        }

        public static VraagDTO MapViewModelToAntwoordDTO(VragenViewModelRequest viewmodel)
        {
            if (viewmodel == null)
            {
                throw new NullReferenceException("vraag model is null");
            }
            return new VraagDTO
            {
                //Id = viewmodel.Id,
                MaxScoreVraag = viewmodel.MaxScoreVraag,
                TypeVraagId = viewmodel.TypeVraagId,
                JsonCorrecteAntwoord = viewmodel.JsonCorrecteAntwoord,
                JsonMogelijkeAntwoorden = viewmodel.JsonMogelijkeAntwoorden,
                VraagStelling = viewmodel.VraagStelling

            };
        }
    }
}
