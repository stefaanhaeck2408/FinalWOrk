using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappers;
using API.Viewmodels.IngevoerdAntwoord;
using Facade.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IngevoerdAntwoordController : ControllerBase
    {
        private readonly IIngevoerdAntwoordService _service;

        public IngevoerdAntwoordController(IIngevoerdAntwoordService service) {
            _service = service;
        }

        /// <summary>
        /// Get All Ingevoerde Antwoorden
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() {
            var ingevoerdAntwoordDTOs = _service.GetAllIngevoerdeAntwoord();

            if (ingevoerdAntwoordDTOs == null) {
                return null;
            }

            var ingevoerdAntwoordModels = new List<IngevoerdAntwoordViewModelResponse>();

            foreach (var antwoord in ingevoerdAntwoordDTOs) {
                ingevoerdAntwoordModels.Add(IngevoerdAntwoordViewModelMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelResponse(antwoord));
            }

            return Ok(ingevoerdAntwoordModels);
        }
        /// <summary>
        /// Delete a specific Ingevoerd Antwoord
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
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
        /// Get a specific Ingevoerd Antwoord
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {     
            try
            {
                var response = _service.FindIngevoerdAntwoord(id);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }
                var ingevoerdAntwoordReturn = IngevoerdAntwoordViewModelMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelResponse(response.DTO);
                return Ok(ingevoerdAntwoordReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a specific Ingevoerd antwoord
        /// </summary>
        /// <param name="ingevoerdAntwoordViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] IngevoerdAntwoordViewModelResponse ingevoerdAntwoordViewModel)
        {
            try
            {
                var ingevoerdAntwoordDTO = IngevoerdAntwoordViewModelMapper.MapIngevoerdModelViewModelResponseToIngevoerdAntwoordDTO(ingevoerdAntwoordViewModel);
                var ingevoerdAntwoord = _service.Update(ingevoerdAntwoordDTO);
                if (ingevoerdAntwoord.DidError) {
                    return BadRequest(ingevoerdAntwoord.Errors);
                }


                var ingevoerdAntwoordReturn = IngevoerdAntwoordViewModelMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelResponse(ingevoerdAntwoord.DTO);
                return Ok(ingevoerdAntwoordReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        /// <summary>
        /// Create an ingevoerd antwoord
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST/Todo
        ///     {
        ///         "id": 1,
        ///         "teamId": 1,
        ///         "JsonAntwoord": "JsonAntwoord",
        ///     }
        /// </remarks>
        /// <param name="ingevoerdAntwoordViewModel"></param>
        /// <returns>A newly created Ingevoerd Antwoord item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(IngevoerdAntwoordViewModelRequest ingevoerdAntwoordViewModel)
        {            
            try
            {
                var createDTO = IngevoerdAntwoordViewModelMapper.MapIngevoerdModelViewModelRequestToIngevoerdAntwoordDTO(ingevoerdAntwoordViewModel);
                var ingevoerdAntwoordDTO = _service.AddIngevoerdAntwoord(createDTO);

                if (ingevoerdAntwoordDTO.DidError) {
                    return BadRequest(ingevoerdAntwoordDTO.Errors);
                }

                var createdIngevoerdAntwoord = IngevoerdAntwoordViewModelMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelResponse(ingevoerdAntwoordDTO.DTO);
                return Ok(createdIngevoerdAntwoord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}