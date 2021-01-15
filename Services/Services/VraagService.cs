using Businessmodels.DTO_S;
using Businessmodels.Models;
using DL.Models;
using DL.Repositories;
using DL.UnitOfWork;
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
    public class VraagService : IVragenService
    {
        private readonly IRondeVraagUnitOfWork _rondeVraagUnitOfWork;

        public VraagService(IRondeVraagUnitOfWork rondeVraagUnitOfWork)
        {
            _rondeVraagUnitOfWork = rondeVraagUnitOfWork;
        }

        public Response<VraagDTO> AddVraag(VraagDTO vraagDTO)
        {
            try
            {
                VraagValidation validator = new VraagValidation();
                ValidationResult results = validator.Validate(vraagDTO);

                if (results.IsValid)
                {
                    var vraag = VraagMapper.MapVraagDTOToVraagModel(vraagDTO);
                    var vraagENtity = _rondeVraagUnitOfWork.VraagRepository.Add(vraag);

                    _rondeVraagUnitOfWork.Commmit();
                    var vraagEntityDTo = VraagMapper.MapVraagModelToVraagDTO(vraagENtity);
                    var response = new Response<VraagDTO> {
                        DTO = vraagEntityDTo
                    };
                    return response;
                }
                else
                {
                    return new Response<VraagDTO> { Errors = results.Errors.Select(x => new Error { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<VraagDTO> { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
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
                var responseEntity = _rondeVraagUnitOfWork.VraagRepository.Remove(id);
                var rows = _rondeVraagUnitOfWork.VraagRepository.SaveChanges();
                return new Response<int> { DTO = rows };
            }
            catch (Exception ex)
            {
                return new Response<int>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }            
        }

        public Response<VraagDTO> FindVraag(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<VraagDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var vraag = _rondeVraagUnitOfWork.VraagRepository.GetById(id);
                var returnVraag = VraagMapper.MapVraagModelToVraagDTO(vraag);
                return new Response<VraagDTO> { DTO = returnVraag };
            }
            catch (Exception ex)
            {
                return new Response<VraagDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }            
        }

        public IEnumerable<VraagDTO> GetAllVragen()
        {
            var vragen = _rondeVraagUnitOfWork.VraagRepository.GetAll();
            var vragenDTOs = new List<VraagDTO>();
            foreach (Vraag vraag in vragen) {
                vragenDTOs.Add(VraagMapper.MapVraagModelToVraagDTO(vraag));
            }
            return vragenDTOs;
        }

        public Response<VraagDTO> Update(VraagDTO vraagDTO)
        {
            try
            {
                VraagValidation validator = new VraagValidation();
                ValidationResult results = validator.Validate(vraagDTO);

                if (results.IsValid)
                {
                    var vraag = VraagMapper.MapVraagDTOToVraagModel(vraagDTO);
                    var vraagENtity = _rondeVraagUnitOfWork.VraagRepository.Update(vraag);
                    _rondeVraagUnitOfWork.Commmit();
                    var vraagEntityDTo = VraagMapper.MapVraagModelToVraagDTO(vraagENtity);
                    var response = new Response<VraagDTO>
                    {
                        DTO = vraagEntityDTo
                    };
                    return response;
                }
                else
                {
                    return new Response<VraagDTO> { Errors = results.Errors.Select(x => new Error { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<VraagDTO> { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
            
            
        }

        public Response<AddVraagToRondeDTO> AddVraagToRonde(AddVraagToRondeDTO dto) 
        {
            try
            {
                VraagRondeValidator validator = new VraagRondeValidator();
                ValidationResult results = validator.Validate(dto);

                if (results.IsValid)
                {
                    var addVraagToRonde = TussentabelMapper.VraagRondeDTOToEntity(dto);
                    var returnEnity = _rondeVraagUnitOfWork.TussentabelRepository.Add(addVraagToRonde);
                    _rondeVraagUnitOfWork.Commmit();
                    var returnEntityDTO = TussentabelMapper.VraagRondeEntityToDTO(returnEnity);
                    var response = new Response<AddVraagToRondeDTO> {
                        DTO = returnEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<AddVraagToRondeDTO> { Errors = results.Errors.Select(x => new Error { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<AddVraagToRondeDTO> { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
            

            
        }

        public Response<int> DeleteVraagFromRonde(AddVraagToRondeDTO dto) {
            try
            {
                VraagRondeValidator validator = new VraagRondeValidator();
                ValidationResult results = validator.Validate(dto);

                if (results.IsValid)
                {
                    var tussentabelWaarden = _rondeVraagUnitOfWork.TussentabelRepository.GetWhere(t => t.RondeId == dto.RondeId && t.VraagId == dto.VraagId);
                    foreach (var waarde in tussentabelWaarden)
                    {
                        _rondeVraagUnitOfWork.TussentabelRepository.Remove(waarde.Id);
                    }
                    var rows = _rondeVraagUnitOfWork.TussentabelRepository.SaveChanges();
                    return new Response<int> { DTO = rows };
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

        public Response<IEnumerable<VraagDTO>> GetAllQuestionsFromOneRonde(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<IEnumerable<VraagDTO>>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var vragen = _rondeVraagUnitOfWork.TussentabelRepository.GetWhere(x => x.RondeId == id);
                List<VraagDTO> vraagDTOs = new List<VraagDTO>();

                foreach (var item in vragen)
                {
                    vraagDTOs.Add(VraagMapper.MapVraagModelToVraagDTO( _rondeVraagUnitOfWork.VraagRepository.GetById(item.VraagId)));
                }
                
                return new Response<IEnumerable<VraagDTO>> { DTO = vraagDTOs };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<VraagDTO>>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }
    }
}
