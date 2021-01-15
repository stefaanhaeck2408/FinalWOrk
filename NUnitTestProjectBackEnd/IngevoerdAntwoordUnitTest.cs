using Businessmodels.DTO_S;
using Businessmodels.Models;
using DL.Models;
using DL.Repositories;
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
    public class IngevoerdAntwoordTests
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
        public void GetAllIngevoerdeAntwoordenCorrect()
        {
            var ingevoerdeAntwoorden = new List<IngevoerdAntwoord>();
            ingevoerdeAntwoorden.Add(new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,
                VraagId = 1
            });

            IQueryable<IngevoerdAntwoord> queryableIngevoerdeAntwoorden = ingevoerdeAntwoorden.AsQueryable();

            var ingevoerdeAntwoordenDTO = new List<IngevoerdAntwoordDTO>();

            foreach (var ingevoerdAntwoord in ingevoerdeAntwoorden)
            {
                ingevoerdeAntwoordenDTO.Add(IngevoerdAntwoordMapper.MapIngevoerdAntwoordModelToIngevoerdAntwoordDTO(ingevoerdAntwoord));
            }

            //Arange
            var ingevoerdeAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();
            ingevoerdeAntwoordRepo.Setup(x => x.GetAll()).Returns(queryableIngevoerdeAntwoorden);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdeAntwoordRepo.Object);

            //Act
            var allIngevoerdeAntwoorden = ingevoerdAntwoordService.GetAllIngevoerdeAntwoord();

            //Assert
            Assert.That(allIngevoerdeAntwoorden.Count(), Is.EqualTo(ingevoerdeAntwoordenDTO.Count()));

            for (int i = 0; i < allIngevoerdeAntwoorden.Count(); i++)
            {
                Assert.That(allIngevoerdeAntwoorden.ToArray()[i].Id, Is.EqualTo(ingevoerdeAntwoordenDTO.ToArray()[i].Id));
                Assert.That(allIngevoerdeAntwoorden.ToArray()[i].GescoordeScore, Is.EqualTo(ingevoerdeAntwoordenDTO.ToArray()[i].GescoordeScore));
                Assert.That(allIngevoerdeAntwoorden.ToArray()[i].JsonAntwoord, Is.EqualTo(ingevoerdeAntwoordenDTO.ToArray()[i].JsonAntwoord));
                Assert.That(allIngevoerdeAntwoorden.ToArray()[i].TeamId, Is.EqualTo(ingevoerdeAntwoordenDTO.ToArray()[i].TeamId));
                Assert.That(allIngevoerdeAntwoorden.ToArray()[i].VraagId, Is.EqualTo(ingevoerdeAntwoordenDTO.ToArray()[i].VraagId));
            }
        }

        [Test]
        public void AddIngevoerdAntwoordCorrect() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,
                VraagId = 1
                
            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.Add(It.IsAny<IngevoerdAntwoord>())).Returns(ingevoerdAntwoord);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Act
            var ingevoerdAntwoordDTO = new IngevoerdAntwoordDTO
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",                
                TeamId = 1,
                VraagId = 1
                
            };

            var response = ingevoerdAntwoordService.AddIngevoerdAntwoord(ingevoerdAntwoordDTO);

            //Assert
            Assert.IsFalse(response.DidError);
            Assert.That(response.DTO.GescoordeScore, Is.EqualTo(ingevoerdAntwoordDTO.GescoordeScore));
            Assert.That(response.DTO.JsonAntwoord, Is.EqualTo(ingevoerdAntwoordDTO.JsonAntwoord));
            Assert.That(response.DTO.TeamId, Is.EqualTo(ingevoerdAntwoordDTO.TeamId));
            Assert.That(response.DTO.VraagId, Is.EqualTo(ingevoerdAntwoordDTO.VraagId));
        }

        [Test]
        public void AddIngevoerdAntwoordNull() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,    
                VraagId = 1
            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.Add(It.IsAny<IngevoerdAntwoord>())).Returns(ingevoerdAntwoord);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Assert
            Assert.IsTrue(ingevoerdAntwoordService.AddIngevoerdAntwoord(null).DidError);
            Assert.IsNull(ingevoerdAntwoordService.AddIngevoerdAntwoord(null).DTO);
        }

        [Test]
        public void UpdateIngevoerdAntwoordCorrect() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonTestAntwoord",
                TeamId = 1,
                VraagId = 2

            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.Update(It.IsAny<IngevoerdAntwoord>())).Returns(ingevoerdAntwoord);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Act
            var ingevoerdAntwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonTestAntwoord",
                GescoordeScore = 5,               
                TeamId = 1,
                VraagId = 2
                
            };

            //Assert
            Assert.IsFalse(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DidError);
            Assert.NotNull(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DTO);
            Assert.That(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DTO.GescoordeScore, Is.EqualTo(ingevoerdAntwoordDTO.GescoordeScore));
            Assert.That(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DTO.Id, Is.EqualTo(ingevoerdAntwoordDTO.Id));
            Assert.That(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DTO.JsonAntwoord, Is.EqualTo(ingevoerdAntwoordDTO.JsonAntwoord));
            Assert.That(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DTO.TeamId, Is.EqualTo(ingevoerdAntwoordDTO.TeamId));
            Assert.That(ingevoerdAntwoordService.Update(ingevoerdAntwoordDTO).DTO.VraagId, Is.EqualTo(ingevoerdAntwoordDTO.VraagId));
        }

        [Test]
        public void UpdateIngevoerdAntwoordNull() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,
                VraagId = 1

            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.Update(It.IsAny<IngevoerdAntwoord>())).Returns(ingevoerdAntwoord);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Assert
            Assert.IsTrue(ingevoerdAntwoordService.Update(null).DidError);
            Assert.IsNull(ingevoerdAntwoordService.Update(null).DTO);
        }

        [Test]
        public void DeleteIngevoerdAntwoordCorrect() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,
                VraagId = 1

            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.Remove(1)).Returns(true);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Act
            var ingevoerdAntwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonTestAntwoord",
                GescoordeScore = 5,              
                TeamId = 1,
                
            };

            //Assert
            Assert.IsFalse(ingevoerdAntwoordService.Delete(ingevoerdAntwoordDTO.Id).DidError);
            Assert.IsTrue(ingevoerdAntwoordService.FindIngevoerdAntwoord(ingevoerdAntwoord.Id).DidError);

        }

        [Test]
        public void FindIngevoerdAntwoordCorrect() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,
                VraagId = 1

            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.GetById(1)).Returns(ingevoerdAntwoord);
            

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Assert
            var ingevoerdAntwoordDTO = ingevoerdAntwoordService.FindIngevoerdAntwoord(1);
            Assert.That(ingevoerdAntwoord.Id, Is.EqualTo(ingevoerdAntwoordDTO.DTO.Id));
            Assert.That(ingevoerdAntwoord.GescoordeScore, Is.EqualTo(ingevoerdAntwoordDTO.DTO.GescoordeScore));
            Assert.That(ingevoerdAntwoord.JsonAntwoord, Is.EqualTo(ingevoerdAntwoordDTO.DTO.JsonAntwoord));
            Assert.That(ingevoerdAntwoord.TeamId, Is.EqualTo(ingevoerdAntwoordDTO.DTO.TeamId));
            Assert.That(ingevoerdAntwoord.VraagId, Is.EqualTo(ingevoerdAntwoordDTO.DTO.VraagId));
            


        }

        [Test]
        public void FindIngevoerdAntwoordNull() {
            var ingevoerdAntwoord = new IngevoerdAntwoord
            {
                GescoordeScore = 5,
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                TeamId = 1,
                VraagId = 1

            };

            //Arrange
            var ingevoerdAntwoordRepo = new Mock<ISQLRepository<IngevoerdAntwoord>>();

            ingevoerdAntwoordRepo.Setup(x => x.GetById(1)).Returns(ingevoerdAntwoord);

            var ingevoerdAntwoordService = new IngevoerdAntwoordService(ingevoerdAntwoordRepo.Object);

            //Assert
            Assert.IsTrue(ingevoerdAntwoordService.FindIngevoerdAntwoord(-5).DidError);
        }
        
    }
}