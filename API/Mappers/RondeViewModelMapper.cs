using API.Viewmodels.Rondes;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class RondeViewModelMapper
    {
        public static RondeViewModelResponse MapRondeDTOToRondeViewModelResponse(RondeDTO rondeDTO)
        {
            if (rondeDTO == null)
            {
                throw new NullReferenceException("ronde object is null");
            }
            return new RondeViewModelResponse
            {
                Id = rondeDTO.Id,
                Naam = rondeDTO.Naam,
                UpdatedAt = rondeDTO.UpdatedAt

            };
        }

        public static RondeDTO MapRondeViewModelResponseToRondeDTO(RondeViewModelResponse rondemodel)
        {
            if (rondemodel == null)
            {
                throw new NullReferenceException("ronde model is null");
            }
            return new RondeDTO
            {
                Id = rondemodel.Id,
                Naam = rondemodel.Naam,
                UpdatedAt = rondemodel.UpdatedAt

            };
        }

        public static RondeViewModelRequest MapQuizDTOToQuizViewModelRequest(RondeDTO rondeDTO)
        {
            if (rondeDTO == null)
            {
                throw new NullReferenceException("ronde object is null");
            }
            return new RondeViewModelRequest
            {            
                Naam = rondeDTO.Naam

            };
        }

        public static RondeDTO MapRondeViewModelRequestToRondeDTO(RondeViewModelRequest rondemodel)
        {
            if (rondemodel == null)
            {
                throw new NullReferenceException("ronde model is null");
            }
            return new RondeDTO
            {
                Naam = rondemodel.Naam

            };
        }
    }
}
