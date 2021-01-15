using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappers;
using API.Viewmodels.Team;
using Facade.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service) {
            _service = service;
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var teamDTOs = _service.GetAllTeams();

            if (teamDTOs == null)
            {
                return null;
            }

            var teamModels = new List<TeamViewModelResponse>();

            foreach (var team in teamDTOs)
            {
                teamModels.Add(TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(team));
            }

            return Ok(teamModels);
        }

        /// <summary>
        /// Get all teams from one user
        /// </summary>
        /// <returns></returns>
        [HttpGet("{email}")]
        public IActionResult GetAllTeamsFromOneUser(string email)
        {
            var teamDTOs = _service.GetAllTeamsFromOneUser(email);

            if (teamDTOs == null)
            {
                return null;
            }

            var teamModels = new List<TeamViewModelResponse>();

            foreach (var team in teamDTOs)
            {
                teamModels.Add(TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(team));
            }

            return Ok(teamModels);
        }

        /// <summary>
        /// Delete a specific team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var response = _service.Delete(id);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Get a specific team by his pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        [HttpGet("{pin}")]
        public IActionResult GetByPin(string pin)
        {
            try
            {
                var response = _service.FindTeamByPin(pin);
                if (response.DidError)
                    return BadRequest(response.Errors);

                var teamViewModel = TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(response.DTO);

                return Ok(teamViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get a specific team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var response = _service.FindTeam(id);
                if (response.DidError)
                    return BadRequest(response.Errors);

                var teamViewModel = TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(response.DTO);

                return Ok(teamViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           

        }

        /// <summary>
        /// Update a specific team
        /// </summary>
        /// <param name="teamViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(TeamViewModelResponse teamViewModel)
        {
            try
            {
                var teamDTO = TeamViewModelMapper.MapTeamViewModelResponseToDTO(teamViewModel);

                var team = _service.Update(teamDTO);
                if (team.DidError) {
                    return BadRequest(team.Errors);
                }
                var teamReturn = TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(team.DTO);
                return Ok(teamReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a new team
        /// </summary>
        /// <param name="teamViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(TeamViewModelRequest teamViewModel)
        {
            try
            {
                var createDTO = TeamViewModelMapper.MapTeamViewModelRequestToTeamDTO(teamViewModel);
                var response = _service.AddTeam(createDTO);


                if (response.DidError) {
                    return BadRequest(response.Errors);
                }

                var createdTeam = TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(response.DTO);

                return Ok(createdTeam);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}