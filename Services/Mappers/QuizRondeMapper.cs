using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class QuizRondeMapper
    {
        public static QuizRondeDTO QuizRondeModelToDTO(QuizRondeTussentabel quizRonde)
        {
            if (quizRonde == null)
            {
                throw new NullReferenceException("QuizRonde object is null");
            }
            return new QuizRondeDTO
            {
                QuizId = quizRonde.QuizId,
                RondeId = quizRonde.RondeId

            };
        }

        public static QuizRondeTussentabel QuizRondeDTOToModel(QuizRondeDTO quizRondeDTO)
        {
            if (quizRondeDTO == null)
            {
                throw new NullReferenceException("quizRondeDTO is null");
            }
            return new QuizRondeTussentabel
            {
                QuizId = quizRondeDTO.QuizId,
                RondeId = quizRondeDTO.RondeId
            };
        }
    }
}
