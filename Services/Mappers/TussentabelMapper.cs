using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class TussentabelMapper
    {
        public static AddTeamToQuizDTO AddTeamToQuizEntityToDTO(QuizTeamTussentabel entity) {
            if (entity == null) {
                throw new NullReferenceException("QuizTeamTussentabel object is null");
            }
            return new AddTeamToQuizDTO
            {
                QuizId = entity.QuizId,
                TeamId = entity.TeamId
            };
        }

        public static QuizTeamTussentabel AddTeamToQuizDTOToEntity(AddTeamToQuizDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("AddTeamToQuizDTO object is null");
            }
            return new QuizTeamTussentabel
            {
                QuizId = dto.QuizId,
                TeamId = dto.TeamId
            };
        }

        public static QuizRondeTussentabel AddRondeToQuizDTOToEntity(AddRondeToQuizDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("AddRondeToQuizDTO object is null");
            }
            return new QuizRondeTussentabel
            {
                QuizId = dto.QuizId,
                RondeId = dto.RondeId
            };
        }

        public static AddRondeToQuizDTO AddRondeToQuizEntityToDTO(QuizRondeTussentabel entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("QuizRondeTussentabel object is null");
            }
            return new AddRondeToQuizDTO
            {
                QuizId = entity.QuizId,
                RondeId = entity.RondeId
            };
        }

        public static AddVraagToRondeDTO VraagRondeEntityToDTO(RondeVraagTussentabel entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("RondeVraagTussentabel object is null");
            }
            return new AddVraagToRondeDTO
            {
                RondeId = entity.RondeId,
                VraagId = entity.VraagId
            };
        }

        public static RondeVraagTussentabel VraagRondeDTOToEntity(AddVraagToRondeDTO dto)
        {
            if (dto == null)
            {
                throw new NullReferenceException("AddVraagToRondeDTO object is null");
            }
            return new RondeVraagTussentabel
            {
                RondeId = dto.RondeId,
                VraagId = dto.VraagId
            };
        }
       
    }
}
