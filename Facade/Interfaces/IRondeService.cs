using Businessmodels.DTO_S;
using Businessmodels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Interfaces
{
    public interface IRondeService
    {
        IEnumerable<RondeDTO> GetAllRondes();
        Response<RondeDTO> AddRonde(RondeDTO rondeDTO);

        Response<RondeDTO> FindRonde(int id);
        Response<RondeDTO> Update(RondeDTO rondeDTO);
        Response<int> Delete(int id);

        Response<IEnumerable<RondeDTO>> findAllRondesInAQuiz(int quizid);


    }
}
