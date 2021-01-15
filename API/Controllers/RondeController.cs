using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappers;
using API.Viewmodels.Rondes;
using Facade.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RondeController : ControllerBase
    {
        private readonly IRondeService _service;

        public RondeController(IRondeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all rondes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var rondesDTOs = _service.GetAllRondes();

            if (rondesDTOs == null)
            {
                return null;
            }

            var rondesModels = new List<RondeViewModelResponse>();

            foreach (var ronde in rondesDTOs)
            {
                rondesModels.Add(RondeViewModelMapper.MapRondeDTOToRondeViewModelResponse(ronde));
            }

            return Ok(rondesModels);
        }
        

        /// <summary>
        /// Delete a specific ronde
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var responseObject = _service.Delete(id);
                if (responseObject.DidError)
                {
                    return BadRequest(responseObject.Errors);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get a specific Ronde
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var response = _service.FindRonde(id);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }
                var rondeReturn = RondeViewModelMapper.MapRondeDTOToRondeViewModelResponse(response.DTO);
                return Ok(rondeReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a specific ronde
        /// </summary>
        /// <param name="rondeViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] RondeViewModelResponse rondeViewModel)
        {
            try
            {
                var rondeDTO = RondeViewModelMapper.MapRondeViewModelResponseToRondeDTO(rondeViewModel);
                var ronde = _service.Update(rondeDTO);
                if (ronde == null)
                {
                    return BadRequest("Error updating ronde");
                }

                if (ronde.DidError)
                {
                    return BadRequest(ronde.Errors);
                }

                var rondeReturn = RondeViewModelMapper.MapRondeDTOToRondeViewModelResponse(ronde.DTO);
                return Created("/api/quiz/GetById/" + rondeReturn.Id, rondeReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Create a new quiz
        /// </summary>
        /// <param name="rondeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(RondeViewModelRequest rondeViewModel)
        {
            try
            {
                var createDTO = RondeViewModelMapper.MapRondeViewModelRequestToRondeDTO(rondeViewModel);
                var response = _service.AddRonde(createDTO);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }


                var createdRonde = RondeViewModelMapper.MapRondeDTOToRondeViewModelResponse(response.DTO);

                return Created("/api/quiz/GetById/" + createdRonde.Id, createdRonde);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all rondes in a quiz
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAllRondesInAQuiz(int id)
        {
            var quizRondeDTOs = _service.findAllRondesInAQuiz(id);

            if (quizRondeDTOs == null)
            {
                return null;
            }

            var rondesModels = new List<RondeViewModelResponse>();

            foreach (var ronde in quizRondeDTOs.DTO)
            {
                rondesModels.Add(RondeViewModelMapper.MapRondeDTOToRondeViewModelResponse(ronde));
            }

            return Ok(rondesModels);
        }
    }
}