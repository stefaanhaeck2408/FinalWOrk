using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class QuizMapper
    {
        public static QuizDTO MapQuizModelToQuizDTO(Quiz quiz)
        {
            if (quiz == null)
            {
                throw new NullReferenceException("quiz object is null");
            }
            return new QuizDTO
            {
                Id = quiz.Id,
                Naam = quiz.Naam,
                EmailCreator = quiz.EmailCreator,
                UpdatedAt = quiz.UpdatedAt,
                FreeQuiz = quiz.FreeQuiz,
                EntryFee = quiz.EntryFee.ToString()

            };
        }

        public static Quiz MapQuizDTOToQuizModel(QuizDTO quizDTO)
        {
            if (quizDTO == null)
            {
                throw new NullReferenceException("QuizDTO is null");
            }
            return new Quiz
            {
                Id = quizDTO.Id,
                Naam = quizDTO.Naam,
                EmailCreator = quizDTO.EmailCreator,
                FreeQuiz = quizDTO.FreeQuiz,
                EntryFee = Int32.Parse( quizDTO.EntryFee)
            };
        }
    }
}
