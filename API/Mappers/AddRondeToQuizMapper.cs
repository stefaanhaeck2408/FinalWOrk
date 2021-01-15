using API.Viewmodels.Quiz;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class AddRondeToQuizMapper
    {
        public static AddRondeToQuizViewModel MapAddRondeToQuizDTOToAddRondeToQuizViewModel(AddRondeToQuizDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("add ronde to quiz dto is null");
            }
            return new AddRondeToQuizViewModel
            {
                QuizId = dto.QuizId,
                RondeId = dto.RondeId

            };
        }

        public static AddRondeToQuizDTO MapAddRondeToQuizViewModelToAddRondeToQuizDTO(AddRondeToQuizViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("AddRondeToQuizViewModel is null");
            }
            return new AddRondeToQuizDTO
            {
                QuizId = model.QuizId,
                RondeId = model.RondeId
            };
        }
    }
}
