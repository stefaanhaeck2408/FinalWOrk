using System;
using System.Collections.Generic;
using System.Text;
using Businessmodels.DTO_S;
using Businessmodels.Models;

namespace Facade.Interfaces
{
    public interface IQuizService
    {
        IEnumerable<QuizDTO> GetAllQuizen();
        IEnumerable<QuizDTO> GetAllQuizesFromOneUser(string email);
        Response<QuizDTO> AddQuiz(QuizDTO quizDTO);

        Response<QuizDTO> FindQuiz(int id);
        Response<QuizDTO> Update(QuizDTO quizDTO);
        Response<int> Delete(int id);
        Response<AddTeamToQuizDTO> AddTeamToQuiz(AddTeamToQuizDTO dto);
        Response<int> DeleteTeamFromQuiz(AddTeamToQuizDTO dto);
        Response<AddRondeToQuizDTO> AddRondeToQuiz(AddRondeToQuizDTO dto);
        Response<int> DeleteRondeFromQuiz(AddRondeToQuizDTO dto);

        Response<IEnumerable<TeamDTO>> GetAllTeamsFromQuiz(int quizId);
    }
}
