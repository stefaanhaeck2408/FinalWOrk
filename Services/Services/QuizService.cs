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

namespace Services.Services
{
    public class QuizService : IQuizService
    {
        private readonly ITeamQuizRondeUnitOfWork _teamQuizRondeUnitOfWork;


        public QuizService(ITeamQuizRondeUnitOfWork quizteamQuizUnitOfWork)
        {
            _teamQuizRondeUnitOfWork = quizteamQuizUnitOfWork;
        }
        public Response<QuizDTO> AddQuiz(QuizDTO quizDTO)
        {
            try
            {
                QuizValidator validator = new QuizValidator();
                ValidationResult results = validator.Validate(quizDTO);

                if (results.IsValid)
                {
                    var quiz = QuizMapper.MapQuizDTOToQuizModel(quizDTO);
                    var quizEntity = _teamQuizRondeUnitOfWork.QuizRepository.Add(quiz);
                    _teamQuizRondeUnitOfWork.Commmit();
                    var quizEntityDTO = QuizMapper.MapQuizModelToQuizDTO(quizEntity);
                    var response = new Response<QuizDTO>
                    {
                        DTO = quizEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<QuizDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<QuizDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };
            }


        }        

        public Response<int> Delete(int id)
        {
            try
            {
                if (id <= 0) {
                    return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var responseEntity = _teamQuizRondeUnitOfWork.QuizRepository.Remove(id);
                var rows = _teamQuizRondeUnitOfWork.QuizRepository.SaveChanges();

                return new Response<int>() { DTO = rows };
            }
            catch (Exception ex)
            {
                return new Response<int>() { Errors = new List<Error>() { new Error {Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public Response<QuizDTO> FindQuiz(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<QuizDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var quiz = _teamQuizRondeUnitOfWork.QuizRepository.GetById(id);
                var quizReturn = QuizMapper.MapQuizModelToQuizDTO(quiz);

                return new Response<QuizDTO>() { DTO = quizReturn };
            }
            catch (Exception ex)
            {
                return new Response<QuizDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }           
            
        }

        public IEnumerable<QuizDTO> GetAllQuizen()
        {
            var quizen = _teamQuizRondeUnitOfWork.QuizRepository.GetAll();
            var quizenDTOs = new List<QuizDTO>();
            foreach (Quiz quiz in quizen)
            {
                quizenDTOs.Add(QuizMapper.MapQuizModelToQuizDTO(quiz));
            }

            return quizenDTOs;
        }

        public IEnumerable<QuizDTO> GetAllQuizesFromOneUser(string emailUser)
        {
            var quizen = _teamQuizRondeUnitOfWork.QuizRepository.GetWhere(x => x.EmailCreator == emailUser);
            var quizenDTOs = new List<QuizDTO>();
            foreach (Quiz quiz in quizen)
            {
                quizenDTOs.Add(QuizMapper.MapQuizModelToQuizDTO(quiz));
            }

            return quizenDTOs;
        }

        public Response<QuizDTO> Update(QuizDTO quizDTO)
        {
            try
            {
                QuizValidator validator = new QuizValidator();
                ValidationResult results = validator.Validate(quizDTO);

                if (results.IsValid)
                {
                    var quiz = QuizMapper.MapQuizDTOToQuizModel(quizDTO);
                    var quizEntity = _teamQuizRondeUnitOfWork.QuizRepository.Update(quiz);
                    _teamQuizRondeUnitOfWork.QuizRepository.SaveChanges();
                    var quizEntityDTO = QuizMapper.MapQuizModelToQuizDTO(quizEntity);
                    var response = new Response<QuizDTO>
                    {
                        DTO = quizEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<QuizDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex) {
                return new Response<QuizDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };

            }

        }

        public Response<AddTeamToQuizDTO> AddTeamToQuiz(AddTeamToQuizDTO dto)
        {
            try
            {
                AddTeamToQuizValidator validator = new AddTeamToQuizValidator();
                ValidationResult results = validator.Validate(dto);

                if (results.IsValid)
                {
                    var tussentabelEntity = TussentabelMapper.AddTeamToQuizDTOToEntity(dto);
                    var returnTussentabel = _teamQuizRondeUnitOfWork.QuizTeamTussentabelRepository.Add(tussentabelEntity);
                    _teamQuizRondeUnitOfWork.Commmit();
                    var returnTussentabelDTO = TussentabelMapper.AddTeamToQuizEntityToDTO(returnTussentabel);
                    var response = new Response<AddTeamToQuizDTO>
                    {
                        DTO = returnTussentabelDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<AddTeamToQuizDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex) 
            {
                return new Response<AddTeamToQuizDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };
            }



        }

        public Response<AddRondeToQuizDTO> AddRondeToQuiz(AddRondeToQuizDTO dto)
        {
            try
            {
                RondeQuizValidator validator = new RondeQuizValidator();
                ValidationResult results = validator.Validate(dto);

                if (results.IsValid)
                {
                    var tussentabelEntity = TussentabelMapper.AddRondeToQuizDTOToEntity(dto);
                    var returnTussentabel = _teamQuizRondeUnitOfWork.QuizRondeTussentabelRepository.Add(tussentabelEntity);
                    _teamQuizRondeUnitOfWork.Commmit();
                    var returnTussentabelDTO = TussentabelMapper.AddRondeToQuizEntityToDTO(returnTussentabel);
                    var response = new Response<AddRondeToQuizDTO>
                    {
                        DTO = returnTussentabelDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<AddRondeToQuizDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex) {
                return new Response<AddRondeToQuizDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };

            }



        }

        public Response<int> DeleteTeamFromQuiz(AddTeamToQuizDTO dto)
        {
            try
            {
                AddTeamToQuizValidator validator = new AddTeamToQuizValidator();
                ValidationResult results = validator.Validate(dto);
                if (results.IsValid)
                {
                    var tussentabelWaarden = _teamQuizRondeUnitOfWork.QuizTeamTussentabelRepository.GetWhere(t => t.QuizId == dto.QuizId && t.TeamId == dto.TeamId);
                    foreach (var waarde in tussentabelWaarden)
                    {
                        _teamQuizRondeUnitOfWork.QuizTeamTussentabelRepository.Remove(waarde.Id);
                    }

                    var rows = _teamQuizRondeUnitOfWork.QuizTeamTussentabelRepository.SaveChanges();
                    return new Response<int> { DTO = rows};
                }

                return new Response<int>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
            }
            catch (Exception ex)
            {
                return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public Response<int> DeleteRondeFromQuiz(AddRondeToQuizDTO dto)
        {
            try
            {
                RondeQuizValidator validator = new RondeQuizValidator();
                ValidationResult results = validator.Validate(dto);

                if (results.IsValid)
                {
                    var tussentabelWaarden = _teamQuizRondeUnitOfWork.QuizRondeTussentabelRepository.GetWhere(t => t.QuizId == dto.QuizId && t.RondeId == dto.RondeId);
                    foreach (var waarde in tussentabelWaarden)
                    {
                        _teamQuizRondeUnitOfWork.QuizRondeTussentabelRepository.Remove(waarde.Id);
                    }

                    var rows = _teamQuizRondeUnitOfWork.QuizRondeTussentabelRepository.SaveChanges();
                    return new Response<int> {DTO = rows };
                }
                else
                {
                    return new Response<int>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                    
                }
            }
            catch (Exception ex) {
                return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };

            }

        }

        public Response<IEnumerable<TeamDTO>> GetAllTeamsFromQuiz(int quizId) {
            try
            {
                var tussentabelWaarden = _teamQuizRondeUnitOfWork.QuizTeamTussentabelRepository.GetWhere(x => x.QuizId == quizId);
                List<TeamDTO> teamDTOs = new List<TeamDTO>();

                foreach (var item in tussentabelWaarden)
                {
                    teamDTOs.Add(TeamMapper.MapTeamModelToTeamDTO(_teamQuizRondeUnitOfWork.TeamRepository.GetWhere(x => x.Id == item.TeamId).FirstOrDefault()));
                }

                return new Response<IEnumerable<TeamDTO>> { DTO = teamDTOs };

            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<TeamDTO>>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }
    }
}
