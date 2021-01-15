using Businessmodels.DTO_S;
using DL.Models;
using DL.Repositories;
using DL.UnitOfWork;
using Moq;
using NUnit.Framework;
using Services.Mappers;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NUnitTestProjectBackEnd
{
    public class QuizTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        
        [Test]
        public void GetAllQuizsCorrect()
        {
            var quizs = new List<Quiz>();
            quizs.Add(new Quiz
            {
                Id = 1,
                Naam = "Quiz 1"
            }) ;

            IQueryable<Quiz> queryableQuizs = quizs.AsQueryable();

            var quizsDTO = new List<QuizDTO>();

            foreach (var quiz in quizs)
            {
                quizsDTO.Add(QuizMapper.MapQuizModelToQuizDTO(quiz));
            }

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.GetAll()).Returns(queryableQuizs);

            var quizService = new QuizService(unitOfWork.Object);

            //Act
            var allQuizs = quizService.GetAllQuizen();

            //Assert
            Assert.That(allQuizs.Count(), Is.EqualTo(quizsDTO.Count()));

            for (int i = 0; i < allQuizs.Count(); i++)
            {
                Assert.That(allQuizs.ToArray()[i].Id, Is.EqualTo(quizsDTO.ToArray()[i].Id));
                Assert.That(allQuizs.ToArray()[i].Naam, Is.EqualTo(quizsDTO.ToArray()[i].Naam));
            }
        }

        [Test]
        public void AddQuizCorrect() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1",
                EmailCreator = "stefaan@test.be"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.Add(It.IsAny<Quiz>())).Returns(quiz);
            var quizService = new QuizService(unitOfWork.Object);

            //Act
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1",
                EmailCreator = "stefaan@test.be"
            };

            //Assert
            Assert.IsFalse(quizService.AddQuiz(quizDTO).DidError);
            Assert.That(quizService.AddQuiz(quizDTO).DTO.EmailCreator, Is.EqualTo(quizDTO.EmailCreator));
            Assert.That(quizService.AddQuiz(quizDTO).DTO.Id, Is.EqualTo(quizDTO.Id));
            Assert.That(quizService.AddQuiz(quizDTO).DTO.Naam, Is.EqualTo(quizDTO.Naam));
        }

        [Test]
        public void AddQuizNull() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.Add(It.IsAny<Quiz>())).Returns(quiz);
            var quizService = new QuizService(unitOfWork.Object);

            //Assert
            Assert.IsTrue(quizService.AddQuiz(null).DidError);
            Assert.IsNull(quizService.AddQuiz(null).DTO);
        }

        [Test]
        public void UpdateQuizCorrect() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1",
                EmailCreator = "stefaan@test.be"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.Update(It.IsAny<Quiz>())).Returns(quiz);
            var quizService = new QuizService(unitOfWork.Object);

            //Act
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1",
                EmailCreator = "stefaan@test.be"
            };

            //Assert
            Assert.IsFalse(quizService.Update(quizDTO).DidError);
            Assert.That(quizService.Update(quizDTO).DTO.Id, Is.EqualTo(quizDTO.Id));
            Assert.That(quizService.Update(quizDTO).DTO.Naam, Is.EqualTo(quizDTO.Naam));
            Assert.That(quizService.Update(quizDTO).DTO.EmailCreator, Is.EqualTo(quizDTO.EmailCreator));
        }

        [Test]
        public void UpdateQuizNull() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.Update(It.IsAny<Quiz>())).Returns(quiz);
            var quizService = new QuizService(unitOfWork.Object);

            //Assert
            Assert.IsTrue(quizService.Update(null).DidError);
            Assert.IsNull(quizService.Update(null).DTO);
        }

        [Test]
        public void DeleteQuizCorrect() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.Remove(1)).Returns(true);
            var quizService = new QuizService(unitOfWork.Object);

            //Act
            var quizDTO = new QuizDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            //Assert
            Assert.IsFalse(quizService.Delete(1).DidError);

            Assert.IsTrue(quizService.FindQuiz(quiz.Id).DidError);
            Assert.IsNull(quizService.FindQuiz(quiz.Id).DTO);
        }

        [Test]
        public void FindQuizCorrect() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.GetById(1)).Returns(quiz);
            var quizService = new QuizService(unitOfWork.Object);

            //Assert
            var response = quizService.FindQuiz(1);
            Assert.That(quiz.Id, Is.EqualTo(response.DTO.Id));
            Assert.That(quiz.Naam, Is.EqualTo(response.DTO.Naam));
            Assert.That(quiz.EmailCreator, Is.EqualTo(response.DTO.EmailCreator));
        }

        [Test]
        public void FindQuizNull() {
            var quiz = new Quiz
            {
                Id = 1,
                Naam = "Quiz 1"
            };

            //Arange
            var unitOfWork = new Mock<ITeamQuizRondeUnitOfWork>();
            unitOfWork.Setup(x => x.QuizRepository.GetById(1)).Returns(quiz);
            var quizService = new QuizService(unitOfWork.Object);

            //Assert
            Assert.IsTrue(quizService.FindQuiz(-5).DidError);
        }
    }
}