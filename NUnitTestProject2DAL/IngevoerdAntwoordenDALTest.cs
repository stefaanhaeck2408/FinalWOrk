using DL.Context;
using DL.Models;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject2DAL
{
    public class IngevoerdAntwoordenDALTests

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
        public void CreateAntwoord()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase1")
                .Options;
            int rows = 0;
            IngevoerdAntwoord responseAdd = null;

            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                responseAdd = repository.Add(antwoord);
                rows = repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.AreEqual(1, rows);
                Assert.IsNotNull(responseAdd.Id);
                Assert.IsNotEmpty(responseAdd.Id.ToString());
                Assert.That(antwoord.GescoordeScore, Is.EqualTo(responseAdd.GescoordeScore));
                Assert.That(antwoord.JsonAntwoord, Is.EqualTo(responseAdd.JsonAntwoord));                
                Assert.That(antwoord.TeamId, Is.EqualTo(responseAdd.TeamId));

                Assert.AreEqual(1, context.IngevoerdAntwoorden.Count());
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
            IngevoerdAntwoord responseAdd = null; 
            IngevoerdAntwoord responseFirstOrDefault = null;

            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                JsonAntwoord = "JsonAntwoord",  
                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                responseAdd = repository.Add(antwoord);
                rows = repository.SaveChanges();
                responseFirstOrDefault = repository.FirstOrDefault(x => x.TeamId == 1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                
                Assert.IsNotNull(responseFirstOrDefault.Id);
                Assert.IsNotEmpty(responseFirstOrDefault.Id.ToString());
                Assert.That(antwoord.GescoordeScore, Is.EqualTo(responseFirstOrDefault.GescoordeScore));
                Assert.That(antwoord.JsonAntwoord, Is.EqualTo(responseFirstOrDefault.JsonAntwoord));                
                Assert.That(antwoord.TeamId, Is.EqualTo(responseFirstOrDefault.TeamId));

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
            IEnumerable<IngevoerdAntwoord> responseGetAll = null;
            IngevoerdAntwoord firstResponse = null;
            

            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                repository.Add(antwoord);
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
                Assert.That(antwoord.GescoordeScore, Is.EqualTo(firstResponse.GescoordeScore));
                Assert.That(antwoord.JsonAntwoord, Is.EqualTo(firstResponse.JsonAntwoord));                
                Assert.That(antwoord.TeamId, Is.EqualTo(firstResponse.TeamId));
            }
        }

        [Test]
        public void GetByID()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase4")
                .Options;
            IngevoerdAntwoord responseGetById = null;



            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                repository.Add(antwoord);
                repository.SaveChanges();
                responseGetById = repository.GetById(1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetById);
                Assert.IsNotEmpty(responseGetById.Id.ToString());
                Assert.That(antwoord.GescoordeScore, Is.EqualTo(responseGetById.GescoordeScore));
                Assert.That(antwoord.JsonAntwoord, Is.EqualTo(responseGetById.JsonAntwoord));
                Assert.That(antwoord.TeamId, Is.EqualTo(responseGetById.TeamId));
            }
        }

        [Test]
        public void GetWhere()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase5")
                .Options;
            IQueryable<IngevoerdAntwoord> responseGetWhere = null;
            IngevoerdAntwoord reponseFirstOfWhere = null;



            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 6,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                repository.Add(antwoord);
                repository.SaveChanges();
                responseGetWhere = repository.GetWhere(x => x.GescoordeScore > 5);
                reponseFirstOfWhere = responseGetWhere.FirstOrDefault();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetWhere);
                Assert.IsNotEmpty(reponseFirstOfWhere.Id.ToString());
                Assert.That(antwoord.GescoordeScore, Is.EqualTo(reponseFirstOfWhere.GescoordeScore));
                Assert.That(antwoord.JsonAntwoord, Is.EqualTo(reponseFirstOfWhere.JsonAntwoord));
                Assert.That(antwoord.TeamId, Is.EqualTo(reponseFirstOfWhere.TeamId));
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



            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 6,
                JsonAntwoord = "JsonAntwoord",               

                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                var responseAdd = repository.Add(antwoord);
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

            IngevoerdAntwoord responseAntwoordUpdated = null;



            var antwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 6,
                JsonAntwoord = "JsonAntwoord",                
                TeamId = 1
            };

            var updateAntwoord = new IngevoerdAntwoord
            {
                Id = 1,
                GescoordeScore = 3,
                JsonAntwoord = "JsonAntwoordUpdate",                
                TeamId = 1
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                var responseAdd = repository.Add(antwoord);                
                repository.SaveChanges();
                
            }

            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<IngevoerdAntwoord>(context);
                responseAntwoordUpdated = repository.Update(updateAntwoord);
                repository.SaveChanges();

            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(updateAntwoord.GescoordeScore, Is.EqualTo(responseAntwoordUpdated.GescoordeScore));
                Assert.That(updateAntwoord.JsonAntwoord, Is.EqualTo(responseAntwoordUpdated.JsonAntwoord));
                Assert.That(updateAntwoord.TeamId, Is.EqualTo(responseAntwoordUpdated.TeamId));
            }
        }
    }
}