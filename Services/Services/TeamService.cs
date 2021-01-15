using Businessmodels.DTO_S;
using Businessmodels.Models;
using DL.Models;
using DL.Repositories;
using Facade.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Services.FluentValidators;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class TeamService : ITeamService
    {
        private readonly ISQLRepository<Team> _repository;

        public TeamService(ISQLRepository<Team> repository)
        {
            _repository = repository;
        }
        public Response<TeamDTO> AddTeam(TeamDTO teamDTO)
        {
            try
            {
                TeamRequestValidator validator = new TeamRequestValidator();
                ValidationResult results = validator.Validate(teamDTO);


                if (results.IsValid)
                {
                    var team = TeamMapper.MapTeamDTOToTeamModel(teamDTO);
                    var teamEntity = _repository.Add(team);

                    _repository.SaveChanges();

                    var teamEntityDTO = TeamMapper.MapTeamModelToTeamDTO(teamEntity);
                    var response = new Response<TeamDTO>
                    {
                        DTO = teamEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<TeamDTO> { Errors = results.Errors.Select(x => new Error { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<TeamDTO> { Errors = new List<Error>(){ new Error { Type = ErrorType.Exception, Message = ex.Message } } };
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
                _repository.Remove(id);
                var rows = _repository.SaveChanges();
                return new Response<int> { DTO = rows };
            }
            catch (Exception ex)
            {
                return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }            
        }

        public Response<TeamDTO> FindTeamByPin(string pin) {
            try
            {
                var team = _repository.GetWhere(x => x.PIN == pin).FirstOrDefault();
                if (team == null) {
                    return new Response<TeamDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.NotFound, Message = "Team not found by that pin" } } };

                }
                return new Response<TeamDTO> { DTO = TeamMapper.MapTeamModelToTeamDTO(team) };
            }
            catch (Exception ex) {
                return new Response<TeamDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };

            }
        }
        
        public Response<TeamDTO> FindTeam(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<TeamDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var team = _repository.GetById(id);
                return new Response<TeamDTO> { DTO = TeamMapper.MapTeamModelToTeamDTO(team) };
            }
            catch (Exception ex)
            {
                return new Response<TeamDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }            
        }

        public IEnumerable<TeamDTO> GetAllTeams()
        {
            var teams = _repository.GetAll();
            var teamDTOs = new List<TeamDTO>();
            foreach (Team team in teams) {
                teamDTOs.Add(TeamMapper.MapTeamModelToTeamDTO(team));
            }
            return teamDTOs;
        }

        public IEnumerable<TeamDTO> GetAllTeamsFromOneUser(string email)
        {
            var teams = _repository.GetWhere(x => x.EmailCreator == email);
            var teamDTOs = new List<TeamDTO>();
            foreach (Team team in teams)
            {
                teamDTOs.Add(TeamMapper.MapTeamModelToTeamDTO(team));
            }
            return teamDTOs;
        }
        
        public Response<TeamDTO> Update(TeamDTO teamDTO)
        {
            try
            {
                TeamValidator validator = new TeamValidator();
                ValidationResult results = validator.Validate(teamDTO);

                if (results.IsValid)
                {

                    var team = TeamMapper.MapTeamDTOToTeamModel(teamDTO);


                    var teamEntity = _repository.Update(team);

                    _repository.SaveChanges();
                    var teamEntityDTO = TeamMapper.MapTeamModelToTeamDTO(teamEntity);
                    var response = new Response<TeamDTO>
                    {
                        DTO = teamEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<TeamDTO> { Errors = results.Errors.Select(x => new Error { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<TeamDTO> { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }

        }
    }
}
