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
    public class IngevoerdAntwoordService : IIngevoerdAntwoordService
    {
        private readonly ISQLRepository<IngevoerdAntwoord> _repository;

        public IngevoerdAntwoordService(ISQLRepository<IngevoerdAntwoord> repository)
        {
            _repository = repository;
        }
        public Response<IngevoerdAntwoordDTO> AddIngevoerdAntwoord(IngevoerdAntwoordDTO ingevoerdAntwoordDTO)
        {
            try
            {
                IngevoerdAntwoordValidator validator = new IngevoerdAntwoordValidator();

                ValidationResult results = validator.Validate(ingevoerdAntwoordDTO);

                if (results.IsValid)
                {
                    var ingevoerdAntwoord = IngevoerdAntwoordMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordModel(ingevoerdAntwoordDTO);
                    var ingevoerdAntwoordEntity = _repository.Add(ingevoerdAntwoord);
                    _repository.SaveChanges();
                    var returnDTO = IngevoerdAntwoordMapper.MapIngevoerdAntwoordModelToIngevoerdAntwoordDTO(ingevoerdAntwoordEntity);
                    var response = new Response<IngevoerdAntwoordDTO>()
                    {
                        DTO = returnDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<IngevoerdAntwoordDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };                                     

                }
            }
            catch (Exception ex) {
                return new Response<IngevoerdAntwoordDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
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

        public Response<IngevoerdAntwoordDTO> FindIngevoerdAntwoord(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<IngevoerdAntwoordDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.ValidationError, Message = "De id mag niet 0 zijn" } } };
                }
                var ingevoerdAntwoord = _repository.GetById(id);
                return new Response<IngevoerdAntwoordDTO> { DTO = IngevoerdAntwoordMapper.MapIngevoerdAntwoordModelToIngevoerdAntwoordDTO(ingevoerdAntwoord) };
            }
            catch (Exception ex)
            {
                return new Response<IngevoerdAntwoordDTO>() { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }           
        }

        public IEnumerable<IngevoerdAntwoordDTO> GetAllIngevoerdeAntwoord()
        {
            IEnumerable<IngevoerdAntwoord> ingevoerdeAntwoorden = _repository.GetAll();
            var ingevoerdeAntwoordenDTOs = new List<IngevoerdAntwoordDTO>();
            foreach (IngevoerdAntwoord antwoord in ingevoerdeAntwoorden)
            {
                ingevoerdeAntwoordenDTOs.Add(IngevoerdAntwoordMapper.MapIngevoerdAntwoordModelToIngevoerdAntwoordDTO(antwoord));
            }
            return ingevoerdeAntwoordenDTOs;
        }

        public Response<IngevoerdAntwoordDTO> Update(IngevoerdAntwoordDTO ingevoerdAntwoordDTO)
        {
            try
            {
                IngevoerdAntwoordValidator validator = new IngevoerdAntwoordValidator();
                ValidationResult results = validator.Validate(ingevoerdAntwoordDTO);

                if (results.IsValid)
                {
                    var ingevoerdAntwoord = IngevoerdAntwoordMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordModel(ingevoerdAntwoordDTO);
                    var ingevoerdAntwoordEntity = _repository.Update(ingevoerdAntwoord);
                    _repository.SaveChanges();
                    var returnDTO = IngevoerdAntwoordMapper.MapIngevoerdAntwoordModelToIngevoerdAntwoordDTO(ingevoerdAntwoordEntity);
                    var response = new Response<IngevoerdAntwoordDTO>
                    {
                        DTO = returnDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<IngevoerdAntwoordDTO> { Errors = results.Errors.Select(x => new Error { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<IngevoerdAntwoordDTO> { Errors = new List<Error>() { new Error { Type = ErrorType.Exception, Message = ex.Message } } };
            }
            
        }

        public bool Verbeter(VerbeterDTO verbeterDTO) {
            //Validation nog schrijven weet nog niet wanneer en hoe dit toegepast zal worden
            if (verbeterDTO.JsonAntwoord.Contains(verbeterDTO.JsonIngevoerdAntwoordTeam))
            {
                var ingevoerdAntwoord = _repository.GetById(verbeterDTO.IngevoerdAntwoordId);
                ingevoerdAntwoord.GescoordeScore = verbeterDTO.MaxScore;

                _repository.Update(ingevoerdAntwoord);
                return true;
            }
            else {
                return false;
            }
            
        }
    }
}
