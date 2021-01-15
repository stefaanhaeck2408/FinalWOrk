using API.Viewmodels;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class QuizViewModelMapper
    {
        public static QuizViewModelResponse MapQuizDTOToQuizViewModel(QuizDTO quizDTO)
        {
            if (quizDTO == null)
            {
                throw new NullReferenceException("quiz object is null");
            }
            return new QuizViewModelResponse
            {
                Id = quizDTO.Id,
                Naam = quizDTO.Naam,
                UpdatedAt = quizDTO.UpdatedAt,
                FreeQuiz= quizDTO.FreeQuiz,
                EmailCreator = quizDTO.EmailCreator,
                EntryFee = quizDTO.EntryFee

            };
        }

        public static QuizDTO MapQuizViewModelToQuizDTO(QuizViewModelResponse quizmodel)
        {
            if (quizmodel == null)
            {
                throw new NullReferenceException("quiz model is null");
            }
            return new QuizDTO
            {
                Id = quizmodel.Id,
                Naam = quizmodel.Naam,
                EmailCreator = quizmodel.EmailCreator,

            };
        }

        public static QuizViewModelRequest MapQuizDTOToQuizViewModelRequest(QuizDTO quizDTO)
        {
            if (quizDTO == null)
            {
                throw new NullReferenceException("quiz object is null");
            }
            return new QuizViewModelRequest
            {
                //Id = quizDTO.Id,
                Naam = quizDTO.Naam

            };
        }

        public static QuizDTO MapQuizViewModelRequestToQuizDTO(QuizViewModelRequest quizmodel)
        {
            if (quizmodel == null)
            {
                throw new NullReferenceException("quiz model is null");
            }
            return new QuizDTO
            {
                //Id = quizmodel.Id,
                Naam = quizmodel.Naam,
                EmailCreator = quizmodel.EmailCreator,
                FreeQuiz = quizmodel.FreeQuiz,
                EntryFee = quizmodel.EntryFee

            };
        }
    }
}
