using DL.Context;
using DL.Models;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject2DAL
{
    public class QuizDALTests

    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateQuiz()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase1")
                .Options;
            int rows = 0;
            Quiz responseQuiz = null;

            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                responseQuiz = repository.Add(quiz);
                rows = repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.AreEqual(1, rows);
                Assert.IsNotNull(responseQuiz.Id);
                Assert.IsNotEmpty(responseQuiz.Id.ToString());
                Assert.That(quiz.EmailCreator, Is.EqualTo(responseQuiz.EmailCreator));
                Assert.That(quiz.Naam, Is.EqualTo(responseQuiz.Naam));                

                Assert.AreEqual(1, context.Quizen.Count());
            }
        }

        [Test]
        public void FirstOrDefault()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase2")
                .Options;
            int rows = 0;
            Quiz responseAdd = null; 
            Quiz responseFirstOrDefault = null;

            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                responseAdd = repository.Add(quiz);
                rows = repository.SaveChanges();
                responseFirstOrDefault = repository.FirstOrDefault(x => x.Id == 1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                
                Assert.IsNotNull(responseFirstOrDefault.Id);
                Assert.IsNotEmpty(responseFirstOrDefault.Id.ToString());
                Assert.That(quiz.Naam, Is.EqualTo(responseFirstOrDefault.Naam));
                Assert.That(quiz.EmailCreator, Is.EqualTo(responseFirstOrDefault.EmailCreator));                
            }
        }

        [Test]
        public void GetAll()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase3")
                .Options;
            int count = 0;
            IEnumerable<Quiz> responseGetAll = null;
            Quiz firstResponse = null;
            

            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                repository.Add(quiz);
                repository.SaveChanges();
                responseGetAll = repository.GetAll();
                count = responseGetAll.Count();
                firstResponse = responseGetAll.FirstOrDefault();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(1, Is.EqualTo(count));

                Assert.IsNotNull(firstResponse.Id);
                Assert.IsNotEmpty(firstResponse.Id.ToString());
                Assert.That(quiz.EmailCreator, Is.EqualTo(firstResponse.EmailCreator));
                Assert.That(quiz.Naam, Is.EqualTo(firstResponse.Naam));                
            }
        }

        [Test]
        public void GetByID()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase4")
                .Options;
            Quiz responseGetById = null;



            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                repository.Add(quiz);
                repository.SaveChanges();
                responseGetById = repository.GetById(1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetById);
                Assert.IsNotEmpty(responseGetById.Id.ToString());
                Assert.That(quiz.EmailCreator, Is.EqualTo(responseGetById.EmailCreator));
                Assert.That(quiz.Naam, Is.EqualTo(responseGetById.Naam));
            }
        }

        [Test]
        public void GetWhere()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase5")
                .Options;
            IQueryable<Quiz> responseGetWhere = null;
            Quiz reponseFirstOfWhere = null;

            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                repository.Add(quiz);
                repository.SaveChanges();
                responseGetWhere = repository.GetWhere(x => x.EmailCreator == "stefaan.haeck@allphi.eu");
                reponseFirstOfWhere = responseGetWhere.FirstOrDefault();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetWhere);
                Assert.IsNotEmpty(reponseFirstOfWhere.Id.ToString());
                Assert.That(quiz.EmailCreator, Is.EqualTo(reponseFirstOfWhere.EmailCreator));
                Assert.That(quiz.Naam, Is.EqualTo(reponseFirstOfWhere.Naam));
            }
        }

        [Test]
        public void Remove()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase6")
                .Options;
            bool reponseRemove;
            int countBeforeDelete = 0;
            int countAfterDelete = 0;

            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                var responseAdd = repository.Add(quiz);
                repository.SaveChanges();
                countBeforeDelete = repository.GetAll().Count();
                reponseRemove = repository.Remove(responseAdd.Id);
                repository.SaveChanges();
                countAfterDelete = repository.GetAll().Count();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(1, Is.EqualTo(countBeforeDelete));
                Assert.That(0, Is.EqualTo(countAfterDelete));                
                Assert.That(true, Is.EqualTo(reponseRemove));                
            }
        }

        [Test]
        public void Update()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase7")
                .Options;

            Quiz responseQuizUpdated = null;

            var quiz = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            var updateQuiz = new Quiz
            {
                Id = 1,
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                var responseAdd = repository.Add(quiz);                
                repository.SaveChanges();
                
            }

            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                responseQuizUpdated = repository.Update(updateQuiz);
                repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(updateQuiz.EmailCreator, Is.EqualTo(responseQuizUpdated.EmailCreator));
                Assert.That(updateQuiz.Naam, Is.EqualTo(responseQuizUpdated.Naam));
            }
        }
    }
}