using System.Collections.Generic;
using Businessmodels.DTO_S;
using Businessmodels.Models;

namespace Facade.Interfaces
{
    public interface ITeamService
    {
        IEnumerable<TeamDTO> GetAllTeams();
        IEnumerable<TeamDTO> GetAllTeamsFromOneUser(string email);
        Response<TeamDTO> AddTeam(TeamDTO teamDTO);

        Response<TeamDTO> FindTeam(int id);
        Response<TeamDTO> FindTeamByPin(string pin);
        Response<TeamDTO> Update(TeamDTO teamDTO);
        Response<int> Delete(int id);

    }
}
