using Businessmodels.DTO_S;
using Businessmodels.Models;
using DL.Models;
using DL.UnitOfWork;
using Facade.Interfaces;
using FluentValidation.Results;
using Services.FluentValidators;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class RondeService : IRondeService
    {
        private readonly ITeamQuizRondeUnitOfWork _teamQuizRondeUnitOfWork;

        public RondeService(ITeamQuizRondeUnitOfWork quizteamQuizUnitOfWork)
        {
            _teamQuizRondeUnitOfWork = quizteamQuizUnitOfWork;
        }

        public Response<RondeDTO> AddRonde(RondeDTO rondeDTO)
        {
            try
            {
                RondeRequestValidator validator = new RondeRequestValidator();
                ValidationResult results = validator.Validate(rondeDTO);

                if (results.IsValid)
                {
                    var ronde = RondeMapper.MapRondeDTOToRondeModel(rondeDTO);
                    var rondeEntity = _teamQuizRondeUnitOfWork.RondeRepository.Add(ronde);
                    _teamQuizRondeUnitOfWork.Commmit();
                    var rondeEntityDTO = RondeMapper.MapRondeModelToRondeDTO(rondeEntity);
                    var response = new Response<RondeDTO>
                    {
                        DTO = rondeEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<RondeDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<RondeDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public Response<int> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var responseEntity = _teamQuizRondeUnitOfWork.RondeRepository.Remove(id);
                var rows = _teamQuizRondeUnitOfWork.RondeRepository.SaveChanges();

                return new Response<int>() { DTO = rows };
            }
            catch (Exception ex)
            {
                return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public Response<RondeDTO> FindRonde(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<RondeDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var ronde = _teamQuizRondeUnitOfWork.RondeRepository.GetById(id);
                var rondeReturn = RondeMapper.MapRondeModelToRondeDTO(ronde);

                return new Response<RondeDTO>() { DTO = rondeReturn };
            }
            catch (Exception ex)
            {
                return new Response<RondeDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public IEnumerable<RondeDTO> GetAllRondes()
        {
            var rondes = _teamQuizRondeUnitOfWork.RondeRepository.GetAll();
            var rondesDTOs = new List<RondeDTO>();
            foreach (Ronde ronde in rondes)
            {
                rondesDTOs.Add(RondeMapper.MapRondeModelToRondeDTO(ronde));
            }

            return rondesDTOs;
        }

        public Response<RondeDTO> Update(RondeDTO rondeDTO)
        {
            try
            {
                RondeValidator validator = new RondeValidator();
                ValidationResult results = validator.Validate(rondeDTO);

                if (results.IsValid)
                {
                    var ronde = RondeMapper.MapRondeDTOToRondeModel(rondeDTO);
                    var rondeEntity = _teamQuizRondeUnitOfWork.RondeRepository.Update(ronde);
                    _teamQuizRondeUnitOfWork.RondeRepository.SaveChanges();
                    var rondeEntityDTO = RondeMapper.MapRondeModelToRondeDTO(rondeEntity);
                    var response = new Response<RondeDTO>
                    {
                        DTO = rondeEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<RondeDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<RondeDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };

            }
        }

        public Response<IEnumerable<RondeDTO>> findAllRondesInAQuiz(int quizid) {
            try
            {
                if (quizid <= 0)
                {
                    return new Response<IEnumerable<RondeDTO>>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var responseEntities = _teamQuizRondeUnitOfWork.QuizRondeTussentabelRepository.GetWhere(x => x.QuizId == quizid);
                List<RondeDTO> rondeDTOs = new List<RondeDTO>();

                foreach (var item in responseEntities)
                {
                    var ronde = _teamQuizRondeUnitOfWork.RondeRepository.GetById(item.RondeId);
                    rondeDTOs.Add(RondeMapper.MapRondeModelToRondeDTO(ronde));
                }

                return new Response<IEnumerable<RondeDTO>>() { DTO = rondeDTOs };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<RondeDTO>>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

    }
}
