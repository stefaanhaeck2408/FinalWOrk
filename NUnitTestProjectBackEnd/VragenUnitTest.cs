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
    public class VragenTests
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
        public void GetAllVragenCorrect()
        {
            var vragen = new List<Vraag>();
            vragen.Add(new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            }) ;

            IQueryable<Vraag> queryableVragen = vragen.AsQueryable();

            var vragenDTO = new List<VraagDTO>();

            foreach (var vraag in vragen)
            {
                vragenDTO.Add(VraagMapper.MapVraagModelToVraagDTO(vraag));
            }

            //Arange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.GetAll()).Returns(queryableVragen);

            var vraagService = new VraagService(unitOfWork.Object);

            //Act
            var allVragen = vraagService.GetAllVragen();

            //Assert
            Assert.That(allVragen.Count(), Is.EqualTo(vragenDTO.Count()));

            for (int i = 0; i < allVragen.Count(); i++)
            {
                Assert.That(allVragen.ToArray()[i].Id, Is.EqualTo(vragenDTO.ToArray()[i].Id));
                Assert.That(allVragen.ToArray()[i].MaxScoreVraag, Is.EqualTo(vragenDTO.ToArray()[i].MaxScoreVraag));
                Assert.That(allVragen.ToArray()[i].TypeVraagDTO, Is.EqualTo(vragenDTO.ToArray()[i].TypeVraagDTO));
                Assert.That(allVragen.ToArray()[i].TypeVraagId, Is.EqualTo(vragenDTO.ToArray()[i].TypeVraagId));
                Assert.That(allVragen.ToArray()[i].VraagStelling, Is.EqualTo(vragenDTO.ToArray()[i].VraagStelling));
                Assert.That(allVragen.ToArray()[i].JsonCorrecteAntwoord, Is.EqualTo(vragenDTO.ToArray()[i].JsonCorrecteAntwoord));
                Assert.That(allVragen.ToArray()[i].JsonMogelijkeAntwoorden, Is.EqualTo(vragenDTO.ToArray()[i].JsonMogelijkeAntwoorden));

            }
        }

        [Test]
        public void AddVraagCorrect() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.Add(It.IsAny<Vraag>())).Returns(vraag);

            var vraagService = new VraagService(unitOfWork.Object);

            //Act
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraagDTO = TypeVraagDTO.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Assert
            var response = vraagService.AddVraag(vraagDTO);
            Assert.IsFalse(response.DidError);
            Assert.That(response.DTO.Id, Is.EqualTo(vraagDTO.Id));
            Assert.That(response.DTO.JsonCorrecteAntwoord, Is.EqualTo(vraagDTO.JsonCorrecteAntwoord));
            Assert.That(response.DTO.JsonMogelijkeAntwoorden, Is.EqualTo(vraagDTO.JsonMogelijkeAntwoorden));
            Assert.That(response.DTO.MaxScoreVraag, Is.EqualTo(vraagDTO.MaxScoreVraag));
            Assert.That(response.DTO.TypeVraagId, Is.EqualTo(vraagDTO.TypeVraagId));
            Assert.That(response.DTO.VraagStelling, Is.EqualTo(vraagDTO.VraagStelling));
        }

        [Test]
        public void AddVraagNull() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.Add(It.IsAny<Vraag>())).Returns(vraag);

            var vraagService = new VraagService(unitOfWork.Object);

            //Assert
            var response = vraagService.AddVraag(null);
            Assert.IsTrue(response.DidError);
            Assert.IsNull(response.DTO);
        }

        [Test]
        public void UpdateVraagCorrect() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.Update(It.IsAny<Vraag>())).Returns(vraag);

            var vraagService = new VraagService(unitOfWork.Object);

            //Act
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraagDTO = TypeVraagDTO.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };
            vraagService.Update(vraagDTO);

            //Assert
            var response = vraagService.Update(vraagDTO);
            Assert.IsFalse(response.DidError);
            Assert.That(response.DTO.Id, Is.EqualTo(vraagDTO.Id));
            Assert.That(response.DTO.JsonCorrecteAntwoord, Is.EqualTo(vraagDTO.JsonCorrecteAntwoord));
            Assert.That(response.DTO.JsonMogelijkeAntwoorden, Is.EqualTo(vraagDTO.JsonMogelijkeAntwoorden));
            Assert.That(response.DTO.MaxScoreVraag, Is.EqualTo(vraagDTO.MaxScoreVraag));
            Assert.That(response.DTO.TypeVraagId, Is.EqualTo(vraagDTO.TypeVraagId));
            Assert.That(response.DTO.VraagStelling, Is.EqualTo(vraagDTO.VraagStelling));
        }

        [Test]
        public void UpdateVraagNull() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.Update(It.IsAny<Vraag>())).Returns(vraag);

            var vraagService = new VraagService(unitOfWork.Object);

            //Assert
            var response = vraagService.Update(null);
            Assert.IsTrue(response.DidError);
            Assert.IsNull(response.DTO);
        }

        [Test]
        public void DeleteVraagCorrect() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.Remove(vraag.Id)).Returns(true);

            var vraagService = new VraagService(unitOfWork.Object);

            //Act
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraagDTO = TypeVraagDTO.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Assert
            var response = vraagService.Delete(vraagDTO.Id);
            Assert.IsFalse(response.DidError);
        }

        [Test]
        public void FindVraagCorrect() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.GetById(1)).Returns(vraag);

            var vraagService = new VraagService(unitOfWork.Object);

            //Assert
            var response = vraagService.FindVraag(1);
            Assert.That(response.DTO.Id, Is.EqualTo(vraag.Id));
            Assert.That(response.DTO.JsonCorrecteAntwoord, Is.EqualTo(vraag.JsonCorrecteAntwoord));
            Assert.That(response.DTO.JsonMogelijkeAntwoorden, Is.EqualTo(vraag.JsonMogelijkeAntwoorden));
            Assert.That(response.DTO.MaxScoreVraag, Is.EqualTo(vraag.MaxScoreVraag));
            Assert.That(response.DTO.TypeVraagId, Is.EqualTo(vraag.TypeVraagId));
            Assert.That(response.DTO.VraagStelling, Is.EqualTo(vraag.VraagStelling));

        }

        [Test]
        public void FindVraagNull() {
            var vraag = new Vraag
            {
                Id = 1,
                MaxScoreVraag = 10,
                //TypeVraag = TypeVraag.Meerkeuze,
                TypeVraagId = 1,
                VraagStelling = "Dit is de stelling van de vraag",
                JsonCorrecteAntwoord = "JsonCorrecteAntwoord",
                JsonMogelijkeAntwoorden = "JsonMogelijkeAntwoorden"
            };

            //Arrange
            var unitOfWork = new Mock<IRondeVraagUnitOfWork>();
            unitOfWork.Setup(x => x.VraagRepository.GetById(1)).Returns(vraag);

            var vraagService = new VraagService(unitOfWork.Object);

            //Assert
            Assert.IsTrue(vraagService.FindVraag(5).DidError);
            Assert.IsNull(vraagService.FindVraag(5).DTO);
        }
    }
}