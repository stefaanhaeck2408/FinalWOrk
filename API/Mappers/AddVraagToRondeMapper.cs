using API.Viewmodels.Vragen;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class AddVraagToRondeMapper
    {
        public static VraagRondeViewModel MapAddVraagToRondeDTOToAddVraagToRondeViewModel(AddVraagToRondeDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("AddVraagToRondeDTO is null");
            }
            return new VraagRondeViewModel
            {
                RondeId = dto.RondeId,
                VraagId = dto.VraagId

            };
        }

        public static AddVraagToRondeDTO MapAddVraagToRondeViewModelToAddVraagToRondeDTO(VraagRondeViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("AddVraagToRondeViewModel is null");
            }
            return new AddVraagToRondeDTO
            {
                RondeId = model.RondeId,
                VraagId = model.VraagId
            };
        }
    }
}
