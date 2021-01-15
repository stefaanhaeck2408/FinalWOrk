using System;
using System.Collections.Generic;
using System.Text;
using Businessmodels.DTO_S;
using Businessmodels.Models;

namespace Facade.Interfaces
{
    public interface IVragenService
    {
        IEnumerable<VraagDTO> GetAllVragen();
        Response<VraagDTO> AddVraag(VraagDTO companyDTO);

        Response<VraagDTO> FindVraag(int id);
        Response<VraagDTO> Update(VraagDTO vraagDTO);
        Response<int> Delete(int id);
        Response<AddVraagToRondeDTO> AddVraagToRonde(AddVraagToRondeDTO dto);
        Response<int> DeleteVraagFromRonde(AddVraagToRondeDTO dto);

        Response<IEnumerable<VraagDTO>> GetAllQuestionsFromOneRonde(int id);

    }
}
