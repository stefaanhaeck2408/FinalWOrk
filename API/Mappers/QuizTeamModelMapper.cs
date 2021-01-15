using API.Viewmodels.Quiz;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class QuizTeamModelMapper
    {
        public static QuizTeamViewModel MapQuizTeamDTOToModel(QuizTeamDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("QuizTeamDTO is null");
            }
            return new QuizTeamViewModel
            {
                QuizId = dto.QuizId,
                TeamId = dto.TeamId

            };
        }

        

        
    }
}
