using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class QuizTeamMapper
    {
        public static QuizTeamDTO QuizTeamModelToDTO(QuizTeamTussentabel quizTeam)
        {
            if (quizTeam == null)
            {
                throw new NullReferenceException("quizTeam object is null");
            }
            return new QuizTeamDTO
            {
                QuizId = quizTeam.QuizId,
                TeamId = quizTeam.TeamId

            };
        }

        public static QuizTeamTussentabel QuizTeamDTOToModel(QuizTeamDTO quizTeamDTO)
        {
            if (quizTeamDTO == null)
            {
                throw new NullReferenceException("quizTeamDTO is null");
            }
            return new QuizTeamTussentabel
            {
                QuizId = quizTeamDTO.QuizId,
                TeamId = quizTeamDTO.TeamId
            };
        }
    }
}
