using DL.Context;
using DL.Models;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject2DAL
{
    public class VraagDALTests

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
            Vraag responseVraag = null;

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"

            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                responseVraag = repository.Add(vraag);
                rows = repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.AreEqual(1, rows);
                Assert.IsNotNull(responseVraag.Id);
                Assert.IsNotEmpty(responseVraag.Id.ToString());
                Assert.That(vraag.JsonCorrecteAntwoord, Is.EqualTo(responseVraag.JsonCorrecteAntwoord));
                Assert.That(vraag.JsonMogelijkeAntwoorden, Is.EqualTo(responseVraag.JsonMogelijkeAntwoorden));
                Assert.That(vraag.MaxScoreVraag, Is.EqualTo(responseVraag.MaxScoreVraag));
                Assert.That(vraag.TypeVraagId, Is.EqualTo(responseVraag.TypeVraagId));
                Assert.That(vraag.VraagStelling, Is.EqualTo(responseVraag.VraagStelling));
                                

                Assert.AreEqual(1, context.Vragen.Count());
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
            Vraag responseAdd = null; 
            Vraag responseFirstOrDefault = null;

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                responseAdd = repository.Add(vraag);
                rows = repository.SaveChanges();
                responseFirstOrDefault = repository.FirstOrDefault(x => x.Id == 1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                
                Assert.IsNotNull(responseFirstOrDefault.Id);
                Assert.IsNotEmpty(responseFirstOrDefault.Id.ToString());
                Assert.That(vraag.JsonCorrecteAntwoord, Is.EqualTo(responseFirstOrDefault.JsonCorrecteAntwoord));
                Assert.That(vraag.JsonMogelijkeAntwoorden, Is.EqualTo(responseFirstOrDefault.JsonMogelijkeAntwoorden));
                Assert.That(vraag.MaxScoreVraag, Is.EqualTo(responseFirstOrDefault.MaxScoreVraag));
                Assert.That(vraag.TypeVraagId, Is.EqualTo(responseFirstOrDefault.TypeVraagId));
                Assert.That(vraag.VraagStelling, Is.EqualTo(responseFirstOrDefault.VraagStelling));
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
            IEnumerable<Vraag> responseGetAll = null;
            Vraag firstResponse = null;
            

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                repository.Add(vraag);
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
                Assert.That(vraag.JsonCorrecteAntwoord, Is.EqualTo(firstResponse.JsonCorrecteAntwoord));
                Assert.That(vraag.JsonMogelijkeAntwoorden, Is.EqualTo(firstResponse.JsonMogelijkeAntwoorden));
                Assert.That(vraag.MaxScoreVraag, Is.EqualTo(firstResponse.MaxScoreVraag));
                Assert.That(vraag.TypeVraagId, Is.EqualTo(firstResponse.TypeVraagId));
                Assert.That(vraag.VraagStelling, Is.EqualTo(firstResponse.VraagStelling));
            }
        }

        [Test]
        public void GetByID()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase4")
                .Options;
            Vraag responseGetById = null;

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                repository.Add(vraag);
                repository.SaveChanges();
                responseGetById = repository.GetById(1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetById);
                Assert.IsNotEmpty(responseGetById.Id.ToString());
                Assert.That(vraag.JsonCorrecteAntwoord, Is.EqualTo(responseGetById.JsonCorrecteAntwoord));
                Assert.That(vraag.JsonMogelijkeAntwoorden, Is.EqualTo(responseGetById.JsonMogelijkeAntwoorden));
                Assert.That(vraag.MaxScoreVraag, Is.EqualTo(responseGetById.MaxScoreVraag));
                Assert.That(vraag.TypeVraagId, Is.EqualTo(responseGetById.TypeVraagId));
                Assert.That(vraag.VraagStelling, Is.EqualTo(responseGetById.VraagStelling));
            }
        }

        [Test]
        public void GetWhere()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase5")
                .Options;
            IQueryable<Vraag> responseGetWhere = null;
            Vraag reponseFirstOfWhere = null;

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                repository.Add(vraag);
                repository.SaveChanges();
                responseGetWhere = repository.GetWhere(x => x.VraagStelling == "Dit is de stelling van de vraag");
                reponseFirstOfWhere = responseGetWhere.FirstOrDefault();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetWhere);
                Assert.IsNotEmpty(reponseFirstOfWhere.Id.ToString());
                Assert.That(vraag.JsonCorrecteAntwoord, Is.EqualTo(reponseFirstOfWhere.JsonCorrecteAntwoord));
                Assert.That(vraag.JsonMogelijkeAntwoorden, Is.EqualTo(reponseFirstOfWhere.JsonMogelijkeAntwoorden));
                Assert.That(vraag.MaxScoreVraag, Is.EqualTo(reponseFirstOfWhere.MaxScoreVraag));
                Assert.That(vraag.TypeVraagId, Is.EqualTo(reponseFirstOfWhere.TypeVraagId));
                Assert.That(vraag.VraagStelling, Is.EqualTo(reponseFirstOfWhere.VraagStelling));
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

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                var responseAdd = repository.Add(vraag);
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

            Vraag responseVraagUpdated = null;

            var vraag = new Vraag
            {
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            var updateVraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 15,
                TypeVraagId = 2,
                VraagStelling = "Dit is de stelling van een andere vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord2",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden2"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                var responseAdd = repository.Add(vraag);                
                repository.SaveChanges();                
            }

            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Vraag>(context);
                responseVraagUpdated = repository.Update(updateVraag);
                repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(updateVraag.Id, Is.EqualTo(responseVraagUpdated.Id));
                Assert.That(updateVraag.JsonCorrecteAntwoord, Is.EqualTo(responseVraagUpdated.JsonCorrecteAntwoord));
                Assert.That(updateVraag.JsonMogelijkeAntwoorden, Is.EqualTo(responseVraagUpdated.JsonMogelijkeAntwoorden));
                Assert.That(updateVraag.MaxScoreVraag, Is.EqualTo(responseVraagUpdated.MaxScoreVraag));
                Assert.That(updateVraag.TypeVraagId, Is.EqualTo(responseVraagUpdated.TypeVraagId));
                Assert.That(updateVraag.VraagStelling, Is.EqualTo(responseVraagUpdated.VraagStelling));
                
            }
        }
    }
}