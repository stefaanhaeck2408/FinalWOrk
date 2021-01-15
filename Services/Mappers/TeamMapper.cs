using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class TeamMapper
    {
        public static TeamDTO MapTeamModelToTeamDTO(Team team)
        {
            /*if (team == null)
            {
                throw new NullReferenceException("Team object is null");
            }*/
            return new TeamDTO
            {
                Id = team.Id,
                Naam = team.Naam,
                Email = team.Email,
                EmailCreator = team.EmailCreator,
                PIN = team.PIN,
                QuizId = team.QuizId,
                UpdatedAt = team.UpdatedAt,
                TeamPaidAllready = team.TeamPaidAllready

            };
        }

        public static Team MapTeamDTOToTeamModel(TeamDTO teamDTO)
        {
            if (teamDTO == null)
            {
                throw new NullReferenceException("TeamDTO is null");
            }
            return new Team
            {
                Id = teamDTO.Id,
                Naam = teamDTO.Naam,
                Email = teamDTO.Email,
                EmailCreator = teamDTO.EmailCreator,
                QuizId = teamDTO.QuizId,
                PIN = teamDTO.PIN,
                TeamPaidAllready = teamDTO.TeamPaidAllready

            };
        }
    }
}
