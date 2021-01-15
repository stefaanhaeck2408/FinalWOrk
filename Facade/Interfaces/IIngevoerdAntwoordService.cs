using System;
using System.Collections.Generic;
using System.Text;
using Businessmodels.DTO_S;
using Businessmodels.Models;

namespace Facade.Interfaces
{
    public interface IIngevoerdAntwoordService
    {
        IEnumerable<IngevoerdAntwoordDTO> GetAllIngevoerdeAntwoord();
        Response<IngevoerdAntwoordDTO> AddIngevoerdAntwoord(IngevoerdAntwoordDTO ingevoerdAntwoordDTO);

        Response<IngevoerdAntwoordDTO> FindIngevoerdAntwoord(int id);
        Response<IngevoerdAntwoordDTO> Update(IngevoerdAntwoordDTO ingevoerdAntwoordDTO);
        Response<int> Delete(int id);
        bool Verbeter(VerbeterDTO verbeterDTO);
    }
}
