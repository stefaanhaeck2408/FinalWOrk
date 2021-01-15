using Businessmodels.DTO_S;
using Businessmodels.Models;
using DL.Models;
using DL.Repositories;
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
    public class StripeAccountService : IStripeAccountService
    {
        ISQLRepository<StripeAccount> _stripeRepository;

        public StripeAccountService(ISQLRepository<StripeAccount> stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }
        public Response<StripeAccountDTO> Create(StripeAccountDTO stripeAccountDTO)
        {
            try
            {
                StripeAccountValidator validator = new StripeAccountValidator();
                ValidationResult results = validator.Validate(stripeAccountDTO);

                if (results.IsValid)
                {
                    var account = StripeAccountMapper.MapStripeAccountDTOToStripeAccountModel(stripeAccountDTO);
                    var accountResponse = _stripeRepository.Add(account);
                    _stripeRepository.SaveChanges();
                    var accountEntityDTO = StripeAccountMapper.MapStripeAccountModelToStripeAccountDTO(accountResponse);
                    var response = new Response<StripeAccountDTO>
                    {
                        DTO = accountEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<StripeAccountDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public Response<StripeAccountDTO> GetByEmail(string email)
        {
            try
            {
                if (email == null) {
                    return new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.ValidationError, Message = "Email cannot be null" } } };
                }

                var account = _stripeRepository.GetWhere(x => x.UserEmail == email).FirstOrDefault();

                if (account != null)
                {
                    var accountEntityDTO = StripeAccountMapper.MapStripeAccountModelToStripeAccountDTO(account);
                    var response = new Response<StripeAccountDTO>
                    {
                        DTO = accountEntityDTO
                    };
                    return response;
                }
                else {
                    return new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.ValidationError, Message = "No account with that email" } } };
                }                

            }
            catch (Exception ex)
            {
                return new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }

        public Response<StripeAccountDTO> GetById(string accoundId)
        {
            throw new NotImplementedException();
        }

        public Response<StripeAccountDTO> Update(StripeAccountDTO stripeAccountDTO)
        {
            try
            {
                StripeAccountValidator validator = new StripeAccountValidator();
                ValidationResult results = validator.Validate(stripeAccountDTO);

                if (results.IsValid)
                {
                    var account = StripeAccountMapper.MapStripeAccountDTOToStripeAccountModel(stripeAccountDTO);
                    var accountResponse = _stripeRepository.Update(account);
                    _stripeRepository.SaveChanges();
                    var accountEntityDTO = StripeAccountMapper.MapStripeAccountModelToStripeAccountDTO(accountResponse);
                    var response = new Response<StripeAccountDTO>
                    {
                        DTO = accountEntityDTO
                    };
                    return response;
                }
                else
                {
                    return new Response<StripeAccountDTO>() { Errors = results.Errors.Select(x => new Error() { Type = ErrorType.ValidationError, Message = x.ErrorMessage }).ToList() };
                }
            }
            catch (Exception ex)
            {
                return new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } };
            }
        }
    }
}
