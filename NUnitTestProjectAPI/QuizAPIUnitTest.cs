using API.Controllers;
using API.Mappers;
using API.Viewmodels;
using API.Viewmodels.IngevoerdAntwoord;
using Businessmodels.DTO_S;
using Businessmodels.Models;
using DL.Models;
using DL.Repositories;
using Facade.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NUnitTestProjectBackEnd
{
    public class QuizAPIUnitTest
    {
        QuizController controller;
        Mock<IQuizService> quizService;
        Mock<ITeamService> teamService;

        [SetUp]
        public void Setup()
        {
            quizService = new Mock<IQuizService>();
            teamService = new Mock<ITeamService>();            
            controller = new QuizController(quizService.Object, teamService.Object);
        }


        [Test]
        public void GetAllQuizenCorrect()
        {
            var quizDTOs = new List<QuizDTO>();
            quizDTOs.Add(new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"

            });

            IQueryable<QuizDTO> queryableQuizDTOs = quizDTOs.AsQueryable();

            var quizModels = new List<QuizViewModelResponse>();

            foreach (var quiz in quizDTOs)
            {
                quizModels.Add(QuizViewModelMapper.MapQuizDTOToQuizViewModel(quiz));
            }

            //Arange
            quizService.Setup(x => x.GetAllQuizen()).Returns(queryableQuizDTOs);

            //Act
            var alleQuizen = controller.GetAll() as ObjectResult;
            var ListQuizen = alleQuizen.Value as List<QuizViewModelResponse>;


            //Assert
            Assert.That(ListQuizen.Count(), Is.EqualTo(quizModels.Count()));

            for (int i = 0; i < ListQuizen.Count(); i++)
            {
                Assert.That(ListQuizen.ToArray()[i].Id, Is.EqualTo(quizModels.ToArray()[i].Id));
                Assert.That(ListQuizen.ToArray()[i].Naam, Is.EqualTo(quizModels.ToArray()[i].Naam));
            }
        }

        [Test]
        public void AddQuizCorrect()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var response = new Response<QuizDTO> { DTO = quizDTO };

            //Arrange
            quizService.Setup(x => x.AddQuiz(It.IsAny<QuizDTO>())).Returns(response);

            //Act            
            var quizViewModel = new QuizViewModelRequest
            {
                Naam = "Quiz 1"
            };

            var addQuiz = controller.Create(quizViewModel) as ObjectResult;
            var entity = addQuiz.Value as QuizViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Create(quizViewModel));
            Assert.That(entity.Id, Is.EqualTo(quizDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(quizDTO.Naam));
        }

        [Test]
        public void AddQuizNull()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var response = new Response<QuizDTO> { DTO = quizDTO };

            //Arrange
            quizService.Setup(x => x.AddQuiz(It.IsAny<QuizDTO>())).Returns(response);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Create(null));
        }

        [Test]
        public void UpdateQuizCorrect()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var response = new Response<QuizDTO> { DTO = quizDTO };

            //Arrange
            quizService.Setup(x => x.Update(It.IsAny<QuizDTO>())).Returns(response);

            //Act            
            var quizViewModel = new QuizViewModelResponse
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var updateQuiz = controller.Update(quizViewModel) as ObjectResult;
            var entity = updateQuiz.Value as QuizViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Update(quizViewModel));
            Assert.That(entity.Id, Is.EqualTo(quizDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(quizDTO.Naam));
        }


        [Test]
        public void UpdateQuizNull()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };
            var response = new Response<QuizDTO> { DTO = quizDTO };

            //Arrange
            quizService.Setup(x => x.Update(It.IsAny<QuizDTO>())).Returns(response);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Update(null));
        }

        [Test]
        public void DeleteQuizCorrect()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var response = new Response<int> { DTO = 1 };

            //Arrange
            quizService.Setup(x => x.Delete(1)).Returns(response);

            //Act            
            var quizViewModel = new QuizViewModelResponse
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var deleteQuiz = controller.Delete(quizViewModel.Id) as ObjectResult;
            //var entity = deleteQuiz.Value as QuizViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Delete(quizViewModel.Id));

        }

        [Test]
        public void FindAntwoordCorrect()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var response = new Response<QuizDTO> { DTO = quizDTO };

            //Arrange
            quizService.Setup(x => x.FindQuiz(1)).Returns(response);

            //Act
            var foundQuiz = controller.GetById(1) as ObjectResult;
            var entity = foundQuiz.Value as QuizViewModelResponse;

            //Assert
            Assert.That(entity.Id, Is.EqualTo(quizDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(quizDTO.Naam));
        }

        [Test]
        public void FindAntwoordNull()
        {
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            var response = new Response<QuizDTO> { DTO = quizDTO };

            //Arrange
            quizService.Setup(x => x.FindQuiz(1)).Returns(response);

            //Act
            var foundQuiz = controller.GetById(5) as ObjectResult;

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), foundQuiz);
        }


    }

}