using DL.Context;
using DL.Models;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject2DAL
{
    public class TeamDALTests

    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateTeam()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase1")
                .Options;
            int rows = 0;
            Team responseTeam = null;

            var team = new Team
            {
                Naam = "Quiz 1",
                EmailCreator = "stefaan.haeck@allphi.eu",
                Email = "test@allphi.eu"

            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                responseTeam = repository.Add(team);
                rows = repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.AreEqual(1, rows);
                Assert.IsNotNull(responseTeam.Id);
                Assert.IsNotEmpty(responseTeam.Id.ToString());
                Assert.That(team.EmailCreator, Is.EqualTo(responseTeam.EmailCreator));
                Assert.That(team.Naam, Is.EqualTo(responseTeam.Naam));                
                Assert.That(team.Email, Is.EqualTo(responseTeam.Email));                

                Assert.AreEqual(1, context.Teams.Count());
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
            Team responseAdd = null; 
            Team responseFirstOrDefault = null;

            var team = new Team
            {
                Naam = "Quiz 1",
                EmailCreator = "stefaan.haeck@allphi.eu",
                Email = "test@allphi.eu"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                responseAdd = repository.Add(team);
                rows = repository.SaveChanges();
                responseFirstOrDefault = repository.FirstOrDefault(x => x.Id == 1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                
                Assert.IsNotNull(responseFirstOrDefault.Id);
                Assert.IsNotEmpty(responseFirstOrDefault.Id.ToString());
                Assert.That(team.Naam, Is.EqualTo(responseFirstOrDefault.Naam));
                Assert.That(team.EmailCreator, Is.EqualTo(responseFirstOrDefault.EmailCreator));                
                Assert.That(team.Email, Is.EqualTo(responseFirstOrDefault.Email));                
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
            IEnumerable<Team> responseGetAll = null;
            Team firstResponse = null;
            

            var team = new Team
            {
                Naam = "Quiz 1",
                EmailCreator = "stefaan.haeck@allphi.eu",
                Email = "test@allphi.eu"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                repository.Add(team);
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
                Assert.That(team.EmailCreator, Is.EqualTo(firstResponse.EmailCreator));
                Assert.That(team.Naam, Is.EqualTo(firstResponse.Naam));                
                Assert.That(team.Email, Is.EqualTo(firstResponse.Email));                
            }
        }

        [Test]
        public void GetByID()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase4")
                .Options;
            Team responseGetById = null;

            var team = new Team
            {
                Naam = "Quiz 1",
                EmailCreator = "stefaan.haeck@allphi.eu",
                Email = "test@allphi.eu"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                repository.Add(team);
                repository.SaveChanges();
                responseGetById = repository.GetById(1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetById);
                Assert.IsNotEmpty(responseGetById.Id.ToString());
                Assert.That(team.EmailCreator, Is.EqualTo(responseGetById.EmailCreator));
                Assert.That(team.Naam, Is.EqualTo(responseGetById.Naam));
                Assert.That(team.Email, Is.EqualTo(responseGetById.Email));
            }
        }

        [Test]
        public void GetWhere()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase5")
                .Options;
            IQueryable<Team> responseGetWhere = null;
            Team reponseFirstOfWhere = null;

            var team = new Team
            {
                Naam = "Quiz 1",
                EmailCreator = "stefaan.haeck@allphi.eu",
                Email = "test@allphi.eu"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                repository.Add(team);
                repository.SaveChanges();
                responseGetWhere = repository.GetWhere(x => x.EmailCreator == "stefaan.haeck@allphi.eu");
                reponseFirstOfWhere = responseGetWhere.FirstOrDefault();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetWhere);
                Assert.IsNotEmpty(reponseFirstOfWhere.Id.ToString());
                Assert.That(team.EmailCreator, Is.EqualTo(reponseFirstOfWhere.EmailCreator));
                Assert.That(team.Naam, Is.EqualTo(reponseFirstOfWhere.Naam));
                Assert.That(team.Email, Is.EqualTo(reponseFirstOfWhere.Email));
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

            var team = new Quiz
            {
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Quiz>(context);
                var responseAdd = repository.Add(team);
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

            Team responseQuizUpdated = null;

            var team = new Team
            {
                Naam = "Quiz 1",
                EmailCreator = "stefaan.haeck@allphi.eu",
                Email = "test@allphi.eu"
            };

            var updateTeam = new Team
            {
                Id = 1,
                EmailCreator = "stefaan.haeck@allphi.eu",
                Naam = "Quiz 1",
                Email = "test@allphi.eu"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                var responseAdd = repository.Add(team);                
                repository.SaveChanges();
                
            }

            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Team>(context);
                responseQuizUpdated = repository.Update(updateTeam);
                repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(updateTeam.EmailCreator, Is.EqualTo(responseQuizUpdated.EmailCreator));
                Assert.That(updateTeam.Naam, Is.EqualTo(responseQuizUpdated.Naam));
                Assert.That(updateTeam.Email, Is.EqualTo(responseQuizUpdated.Email));
            }
        }
    }
}