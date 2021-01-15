using API.Viewmodels.Quiz;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class AddTeamToQuizMapper
    {
        public static AddTeamToQuizViewModel MapAddTeamToQuizDTOToAddTeamToQuizViewModel(AddTeamToQuizDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("add team to quiz dto is null");
            }
            return new AddTeamToQuizViewModel
            {
                QuizId = dto.QuizId,
                TeamId = dto.TeamId

            };
        }

        public static AddTeamToQuizDTO MapAddTeamToQuizViewModelToAddTeamToQuizDTO(AddTeamToQuizViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("AddTeamToQuizModel is null");
            }
            return new AddTeamToQuizDTO
            {
                QuizId = model.QuizId,
                TeamId = model.TeamId
            };
        }
    }
}
