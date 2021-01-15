using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappers;
using API.Viewmodels;
using API.Viewmodels.Quiz;
using API.Viewmodels.Team;
using Businessmodels.DTO_S;
using Facade.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;
        private readonly ITeamService _teamService;

        public QuizController(IQuizService service, ITeamService teamService) {
            _service = service;
            _teamService = teamService;
        }

        /// <summary>
        /// Get all quizes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult test()
        {
            
            return Ok("test V3");
        }

        /// <summary>
        /// Get all quizes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var quizesDTOs = _service.GetAllQuizen();

                if (quizesDTOs == null)
                {
                    return null;
                }

                var quizesModels = new List<QuizViewModelResponse>();

                foreach (var quiz in quizesDTOs)
                {
                    quizesModels.Add(QuizViewModelMapper.MapQuizDTOToQuizViewModel(quiz));
                }

                return Ok(quizesModels);
            }
            catch (Exception ex) {
                return BadRequest(ex.ToString());
            }
            
        }

        /// <summary>
        /// Get all quizes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> Webhook()
        {
            throw new NotImplementedException();
            //https://code.tutsplus.com/articles/paypal-integration-part-3-paypal-webhooks--cms-22919

            /*var clientId = "AcWrvHl9FCuTypko4GzJIP86VIR-uVgCCwvGiZCbPWGivF4apZ_X2ci04XHOK5J1_LAvKREnZdztu4gj";
            var clientSecret = "EAuxhkuW1EDMifdApibpFcs3nKpapRjFaLrCGXI_nhyjFjkrcwMOqNs0DUELt0x_7ksOMXZx9iBXPsLO";

            var sdkConfig = new Dictionary<string, string> {
                {"mode","sandbox" },
                {"clientId",clientId },
                {"clientSecret", clientSecret }
            };

            var accessToken = new OAuthTokenCredential(clientId,clientSecret,sdkConfig).GetAccessToken();



            // The APIContext object can contain an optional override for the trusted certificate.
            var apiContext = new APIContext(accessToken);

            // Get the received request's headers
            var requestheaders = HttpContext.Request.Headers;

            // Get the received request's body
            var requestBody = string.Empty;
            using (var reader = new System.IO.StreamReader(HttpContext.Request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            dynamic jsonBody = JObject.Parse(requestBody);
            string webhookId = jsonBody.id;
            var ev = WebhookEvent.Get(apiContext, webhookId);
            Console.WriteLine();

            // We have all the information the SDK needs, so perform the validation.
            // Note: at least on Sandbox environment this returns false.
            // var isValid = WebhookEvent.ValidateReceivedEvent(apiContext, ToNameValueCollection(requestheaders), requestBody, webhookId);

            switch (ev.event_type)
            {
                case "PAYMENT.CAPTURE.COMPLETED":
                    // Handle payment completed
                    break;
                case "PAYMENT.CAPTURE.DENIED":
                    // Handle payment denied
                    break;
                // Handle other webhooks
                default:
                    break;
            }
            
            return Ok();*/
        }

        /// <summary>
        /// Get all quizes for one user
        /// </summary>
        /// <returns></returns>
        [HttpGet("{email}")]
        public IActionResult GetAllQuizesForOneUser(string email)
        {
            var quizesDTOs = _service.GetAllQuizesFromOneUser(email);

            if (quizesDTOs == null)
            {
                return null;
            }

            var quizesModels = new List<QuizViewModelResponse>();

            foreach (var quiz in quizesDTOs)
            {
                quizesModels.Add(QuizViewModelMapper.MapQuizDTOToQuizViewModel(quiz));
            }

            return Ok(quizesModels);
        }

        /// <summary>
        /// Add specific team to a specific quiz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTeamToQuiz(AddTeamToQuizViewModel model) {
            try
            {
                var addTeamToQuizDTO = AddTeamToQuizMapper.MapAddTeamToQuizViewModelToAddTeamToQuizDTO(model);
                var returnDTO = _service.AddTeamToQuiz(addTeamToQuizDTO);
                if (returnDTO.DidError) {
                    return BadRequest(returnDTO.Errors);
                }
                var quizReturn = AddTeamToQuizMapper.MapAddTeamToQuizDTOToAddTeamToQuizViewModel(returnDTO.DTO);
                return Ok(quizReturn);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete specific team from specific Quiz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteTeamFromQuiz(AddTeamToQuizViewModel model) {
            try
            {
                var deleteTeamFromQuizDTO = AddTeamToQuizMapper.MapAddTeamToQuizViewModelToAddTeamToQuizDTO(model);
                var response = _service.DeleteTeamFromQuiz(deleteTeamFromQuizDTO);
                if (response.DTO <= 0)
                {
                    return BadRequest("Error deleting team from quiz");
                }
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
        /// Add specific team to a specific quiz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRondeToQuiz(AddRondeToQuizViewModel model)
        {
            try
            {
                var addRondeToQuizDTO = AddRondeToQuizMapper.MapAddRondeToQuizViewModelToAddRondeToQuizDTO(model);
                var returnDTO = _service.AddRondeToQuiz(addRondeToQuizDTO);
                if (returnDTO.DidError) {
                    return BadRequest(returnDTO.Errors);
                }
                var quizReturn = AddRondeToQuizMapper.MapAddRondeToQuizDTOToAddRondeToQuizViewModel(returnDTO.DTO);
                return Ok(quizReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific ronde from a specific Quiz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteRondeFromQuiz(AddRondeToQuizViewModel model) {
            try
            {
                var deleteRondeFromQuizDTO = AddRondeToQuizMapper.MapAddRondeToQuizViewModelToAddRondeToQuizDTO(model);
                var response = _service.DeleteRondeFromQuiz(deleteRondeFromQuizDTO);
                if (response.DidError)
                {
                    return BadRequest(response.Errors); ;
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific quiz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var responseObject = _service.Delete(id);
                if (responseObject.DidError) {
                    return BadRequest(responseObject.Errors);
                }

                return Ok();
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Get a specific quiz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var response = _service.FindQuiz(id);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }
                var quizReturn = QuizViewModelMapper.MapQuizDTOToQuizViewModel(response.DTO);
                return Ok(quizReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        /// <summary>
        /// Update a specific quiz
        /// </summary>
        /// <param name="quizViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(QuizViewModelResponse quizViewModel)
        {
            try
            {
                var quizDTO = QuizViewModelMapper.MapQuizViewModelToQuizDTO(quizViewModel);
                var quiz = _service.Update(quizDTO);
                if (quiz == null)
                {
                    return BadRequest("Error updating quiz");
                }

                if (quiz.DidError)
                {
                    return BadRequest(quiz.Errors);
                }

                var quizReturn = QuizViewModelMapper.MapQuizDTOToQuizViewModel(quiz.DTO);
                return Created("/api/quiz/GetById/" + quizReturn.Id, quizReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Create a new quiz
        /// </summary>
        /// <param name="quizViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(QuizViewModelRequest quizViewModel)
        {
            try
            {
                var createDTO = QuizViewModelMapper.MapQuizViewModelRequestToQuizDTO(quizViewModel);
                var response = _service.AddQuiz(createDTO);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }


                var createdQuiz = QuizViewModelMapper.MapQuizDTOToQuizViewModel(response.DTO);

                return Created("/api/quiz/GetById/" + createdQuiz.Id, createdQuiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add round to quiz
        /// </summary>
        /// <param name="quizRoundViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRoundToQuiz(AddRondeToQuizViewModel quizRoundViewModel)
        {
            try
            {
                var createDTO = AddRondeToQuizMapper.MapAddRondeToQuizViewModelToAddRondeToQuizDTO(quizRoundViewModel);
                var response = _service.AddRondeToQuiz(createDTO);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }


                var createdQuiz = AddRondeToQuizMapper.MapAddRondeToQuizDTOToAddRondeToQuizViewModel(response.DTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Send email with invitations to teams
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SendInvitations(SendInvitationViewModel model)
        {  
            Console.WriteLine("");

            try
            {
                var response = _service.GetAllTeamsFromQuiz(model.QuizId);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }

                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3/sandbox3d7deafd31914a929d422e3699215cb8.mailgun.org/messages");
                client.Authenticator = new HttpBasicAuthenticator("api", "97cfda12723cac0322b1040bf1c133f3-ba042922-43f2ab8e");

                foreach (var team in response.DTO) {
                    var pin = Guid.NewGuid().ToString();
                    RestRequest request = new RestRequest();
                    request.AddParameter("domain", "sandbox3d7deafd31914a929d422e3699215cb8.mailgun.org", ParameterType.UrlSegment);
                    //request.Resource = "{domain}/messages";
                    request.AddParameter("from", "Excited User <mailgun@sandbox3d7deafd31914a929d422e3699215cb8.mailgun.org>");
                    request.AddParameter("to", team.Email);
                    //request.AddParameter("to", "YOU@sandbox3d7deafd31914a929d422e3699215cb8.mailgun.org");
                    request.AddParameter("subject", "Invitation to play in a quiz!");
                    var message = model.Message + "\n \nHere is you pin to get access to the quiz: " + pin;
                    if (model.PaypalLink != "") {
                        message += "\n \nHere is the link where you can pay before you can play the quiz: " + model.PaypalLink;
                    }
                    request.AddParameter("text", message);
                    request.Method = Method.POST;
                    var responseClient = client.Execute(request);

                    if (responseClient.IsSuccessful) {                       

                        team.PIN = pin;
                        team.QuizId = model.QuizId;


                        var teamResponse = _teamService.Update(team);
                        if (teamResponse.DidError)
                        {
                            return BadRequest(teamResponse.Errors);
                        }
                        var teamReturn = TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(teamResponse.DTO);
                        return Ok(teamReturn);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get All Teams From Quiz
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpGet("{quizId}")]
        public IActionResult GetAllTeamsFromQuiz(int quizId) {            
            try
            {
                var response = _service.GetAllTeamsFromQuiz(quizId);
                if (response.DidError)
                {
                    return BadRequest(response.Errors);
                }
                List<TeamViewModelResponse> responseList = new List<TeamViewModelResponse>();

                foreach (var item in response.DTO)
                {
                    responseList.Add(TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(item));
                }

                return Ok(responseList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        

        /// <summary>
        /// Save teams to quiz
        /// </summary>
        /// <param name="quizTeamViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTeamsToQuiz(TeamsToQuizViewModel quizTeamViewModel)
        {
            try
            {
                var response = _service.GetAllTeamsFromQuiz(quizTeamViewModel.QuizId);
                /*List<int> teamsInQuiz = new List<int>();
                foreach (var item in quizTeamViewModel.TeamsInQuiz)
                {
                    teamsInQuiz.Add(item.Id);
                }
                List<int> newTeamsToAddToQuiz = new List<int>();

                var result = response.DTO.Where(x => !quizTeamViewModel.TeamsInQuiz.Any(y => x.Id == y.Id));

                foreach (var item in result)
                {
                    var dto = new AddTeamToQuizDTO { QuizId = quizTeamViewModel.QuizId, TeamId = item.Id };
                    _service.DeleteTeamFromQuiz(dto);
                }

                foreach (var item in quizTeamViewModel.TeamsInQuiz)
                {

                    if (response.DTO.Contains(item)) { 
                    
                    }
                }

                foreach (var item in newTeamsToAddToQuiz) {
                    var dto = new AddTeamToQuizDTO { QuizId = quizTeamViewModel.QuizId, TeamId = item };
                    _service.AddTeamToQuiz(dto);
                }*/
                var rows = 0;
                foreach (var item in response.DTO) {
                    var dto = new AddTeamToQuizDTO { QuizId = quizTeamViewModel.QuizId, TeamId = item.Id };
                    _service.DeleteTeamFromQuiz(dto);
                }

                foreach (var item in quizTeamViewModel.TeamsInQuiz) {
                    var dto = new AddTeamToQuizDTO { QuizId = quizTeamViewModel.QuizId, TeamId = item.Id };
                    _service.AddTeamToQuiz(dto);
                }
                
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}