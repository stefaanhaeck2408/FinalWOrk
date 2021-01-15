using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappers;
using API.Viewmodels.StripeAccount;
using Businessmodels.DTO_S;
using Businessmodels.Models;
using Facade.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //https://stripe.com/docs/connect/collect-then-transfer-guide
        IStripeAccountService _stripeService;

        public AccountController(IStripeAccountService stripeAccountService)
        {
            _stripeService = stripeAccountService;
        }

        /// <summary>
        /// Create account
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CreateStripeAccountViewModel model)
        {
            if (model.userEmail == null) {
                return BadRequest("User email cannot be empty");
            }
            try
            {                
                StripeConfiguration.ApiKey = "sk_test_51HvkPWGZgacNuBfZVUQCM2e3KO5iuMqwmIKnqujztMX0UvbkCyFJWhccLBPjKXxck92QwyMiVun9s7AS6zie6uFs00XbjswQBt";

                var options = new AccountCreateOptions
                {
                    Type = "express",
                };

                var service = new AccountService();
                var account = service.Create(options);

                var optionsLink = new AccountLinkCreateOptions
                {
                    Account = account.Id,
                    RefreshUrl = "https://makeaquiz.azurewebsites.net/somethingWentWrong",
                    ReturnUrl = "https://makeaquiz.azurewebsites.net/createFullQuiz",
                    Type = "account_onboarding",
                };

                var serviceAccount = new AccountLinkService();
                var accountLink = serviceAccount.Create(optionsLink);

                if (accountLink != null)
                {
                    var responseFromGet = _stripeService.GetByEmail(model.userEmail);
                    
                    if (responseFromGet.DTO != null)
                    {
                        var newDTO = responseFromGet.DTO;
                        newDTO.StripeAccountId = account.Id;
                        var response = _stripeService.Update(newDTO);
                        if (response.DidError) {
                            return BadRequest();
                        }
                        return Ok(accountLink.Url.ToString());
                    }
                    else {
                        var response = _stripeService.Create(new StripeAccountDTO { StripeAccountId = account.Id, UserEmail = model.userEmail });
                        if (response.DidError)
                        {
                            return BadRequest();
                        }
                        return Ok(accountLink.Url.ToString());
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email) {
            try
            {
                var response = _stripeService.GetByEmail(email);
                if (response.DidError) {
                    return BadRequest(response.Errors);
                }

                StripeConfiguration.ApiKey = "sk_test_51HvkPWGZgacNuBfZVUQCM2e3KO5iuMqwmIKnqujztMX0UvbkCyFJWhccLBPjKXxck92QwyMiVun9s7AS6zie6uFs00XbjswQBt";
                var service = new AccountService();
                var account = service.Get(response.DTO.StripeAccountId);

                if (account.ChargesEnabled == true)
                {
                    var stripeAccountViewModel = StripeAccountViewModelMapper.MapStripeAccountDTOToStripeAccount(response.DTO);

                    return Ok(stripeAccountViewModel);
                }
                else {
                    return BadRequest(new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = "Something went wrong when onboarding new strip account" } } });
                }
            }
            catch (Exception ex) {
                return BadRequest(new Response<StripeAccountDTO>() { Errors = new List<Error>() { new Error() { Type = ErrorType.Exception, Message = ex.Message } } });
            }
        }

        [HttpGet("{accountId}")]
        public void RedirectFromStripe(string accountId) {
            Console.WriteLine("haha");

            var service = new AccountService();
            var account = service.Get(accountId);

            if (account.ChargesEnabled == true) {
                //PaymentIntent(accountId);
            }

        }        

        [HttpGet]
        public IActionResult PaymentIntent(string emailCreator, int fee)
        {
            try
            {
                var accountResponse = _stripeService.GetByEmail(emailCreator);

                if (!accountResponse.DidError)
                {
                    StripeConfiguration.ApiKey = "sk_test_51HvkPWGZgacNuBfZVUQCM2e3KO5iuMqwmIKnqujztMX0UvbkCyFJWhccLBPjKXxck92QwyMiVun9s7AS6zie6uFs00XbjswQBt";

                    var service = new PaymentIntentService();
                    var createOptions = new PaymentIntentCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                        Amount = fee,
                        Currency = "eur",
                        ApplicationFeeAmount = (long?)(fee * 0.05),
                        TransferData = new PaymentIntentTransferDataOptions
                        {
                            Destination = accountResponse.DTO.StripeAccountId,
                        },
                    };
                    var serviceReponse = service.Create(createOptions);

                    return Ok(serviceReponse.ClientSecret);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
