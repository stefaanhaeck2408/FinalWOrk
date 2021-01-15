using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappers;
using API.Viewmodels.Vragen;
using Facade.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VragenController : ControllerBase
    {
        private readonly IVragenService _service;

        public VragenController(IVragenService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all vragen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var vragenDTOs = _service.GetAllVragen();

            if (vragenDTOs == null)
            {
                return null;
            }

            var vragenModels = new List<VragenViewModelReponse>();

            foreach (var vraag in vragenDTOs)
            {
                vragenModels.Add(VraagViewModelMapper.MapVraagDTOToVraagViewModel(vraag));
            }

            return Ok(vragenModels);
        }

        /// <summary>
        /// Delete a specific question
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
        /// Add vraag to a ronde
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddVraagToRonde(VraagRondeViewModel model) {
            try
            {
                var addVraagToRondeDTO = AddVraagToRondeMapper.MapAddVraagToRondeViewModelToAddVraagToRondeDTO(model);
                var returnDTO = _service.AddVraagToRonde(addVraagToRondeDTO);
                if (returnDTO.DidError) {
                    return BadRequest(returnDTO.Errors);
                }
                var quizReturn = AddVraagToRondeMapper.MapAddVraagToRondeDTOToAddVraagToRondeViewModel(returnDTO.DTO);
                return Ok(quizReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteVraagFromRonde(VraagRondeViewModel model) {
            try
            {
                var vraagRondeDTO = AddVraagToRondeMapper.MapAddVraagToRondeViewModelToAddVraagToRondeDTO(model);
                var response = _service.DeleteVraagFromRonde(vraagRondeDTO);
                if (response.DidError) {
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
        /// Get a specific question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var response = _service.FindVraag(id);
                if (response.DidError)
                    return BadRequest(response.Errors);

                var vraagViewModel = VraagViewModelMapper.MapVraagDTOToVraagViewModel(response.DTO);

                return Ok(vraagViewModel);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a specific question
        /// </summary>
        /// <param name="vraagViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] VragenViewModelReponse vraagViewModel)
        {
            try
            {
                var vraagDTO = VraagViewModelMapper.MapViewModelToAntwoordDTO(vraagViewModel);
                var vraag = _service.Update(vraagDTO);
                if (vraag.DidError) {
                    return BadRequest(vraag.Errors);
                }

                var vraagReturn = VraagViewModelMapper.MapVraagDTOToVraagViewModel(vraag.DTO);
                return Ok(vraagReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a specific question
        /// </summary>
        /// <param name="vraagViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(VragenViewModelRequest vraagViewModel)
        {
            try
            {
                var vraagDTO = VraagViewModelMapper.MapViewModelToAntwoordDTO(vraagViewModel);

                var response = _service.AddVraag(vraagDTO);

                if (response.DidError)
                    return BadRequest(response.Errors);

                var createdVraag = VraagViewModelMapper.MapVraagDTOToVraagViewModel(response.DTO);

                return Ok(createdVraag);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all vragen from a ronde
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAllQuestionsFromARonde(int id)
        {
            var vragenDTOs = _service.GetAllQuestionsFromOneRonde(id);

            if (vragenDTOs.DidError)
            {
                return null;
            }

            var vragenModels = new List<VragenViewModelReponse>();

            foreach (var vraag in vragenDTOs.DTO)
            {
                vragenModels.Add(VraagViewModelMapper.MapVraagDTOToVraagViewModel(vraag));
            }

            return Ok(vragenModels);
        }


    }
}