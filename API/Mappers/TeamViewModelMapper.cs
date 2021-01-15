using API.Viewmodels.Team;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class TeamViewModelMapper
    {
        public static TeamViewModelRequest MapTeamDTOToTeamViewModelRequest(TeamDTO teamDTO)
        {
            if (teamDTO == null)
            {
                throw new NullReferenceException("team object is null");
            }
            return new TeamViewModelRequest
            {
                //Id = teamDTO.Id,
                Naam = teamDTO.Naam

            };
        }

        public static TeamDTO MapTeamViewModelRequestToTeamDTO(TeamViewModelRequest teammodel)
        {
            if (teammodel == null)
            {
                throw new NullReferenceException("team model is null");
            }
            return new TeamDTO
            {
                //Id = teammodel.Id,
                Naam = teammodel.Naam,
                Email = teammodel.Email,
                EmailCreator = teammodel.EmailCreator

            };
        }

        public static TeamViewModelResponse MapTeamDTOToTeamViewModelResponse(TeamDTO teamDTO)
        {
            if (teamDTO == null)
            {
                throw new NullReferenceException("team object is null");
            }
            return new TeamViewModelResponse
            {
                Id = teamDTO.Id,
                Naam = teamDTO.Naam,
                QuizId = teamDTO.QuizId,
                PIN = teamDTO.PIN,
                Email = teamDTO.Email,
                EmailCreator = teamDTO.EmailCreator,
                UpdatedAt = teamDTO.UpdatedAt,
                TeamPaidAllready = teamDTO.TeamPaidAllready

            };
        }

        public static TeamDTO MapTeamViewModelResponseToDTO(TeamViewModelResponse teammodel)
        {
            if (teammodel == null)
            {
                throw new NullReferenceException("team model is null");
            }
            return new TeamDTO
            {
                Id = teammodel.Id,
                Naam = teammodel.Naam,
                QuizId = teammodel.QuizId,
                PIN = teammodel.PIN,
                Email = teammodel.Email,
                EmailCreator = teammodel.EmailCreator,
                UpdatedAt = teammodel.UpdatedAt,
                TeamPaidAllready = teammodel.TeamPaidAllready
            };
        }
    }
}
