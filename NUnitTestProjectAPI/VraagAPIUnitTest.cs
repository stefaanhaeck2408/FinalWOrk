using API.Controllers;
using API.Mappers;
using API.Viewmodels;
using API.Viewmodels.IngevoerdAntwoord;
using API.Viewmodels.Team;
using API.Viewmodels.Vragen;
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
    public class VraagAPIUnitTest
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
        
        //deze unittest nog afwerken
        [Test]
        public void GetAllVragenCorrect()
        {
            var vraagDTOs = new List<VraagDTO>();
            vraagDTOs.Add(new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            });

            IQueryable<VraagDTO> queryableTeamDTOs = vraagDTOs.AsQueryable();

            var vraagModels = new List<VragenViewModelReponse>();

            foreach (var vraag in vraagDTOs)
            {
                vraagModels.Add(VraagViewModelMapper.MapVraagDTOToVraagViewModel(vraag));
            }

            //Arange

            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.GetAllVragen()).Returns(queryableTeamDTOs);
            var controller = new VragenController(mockService.Object);


            //Act
            var alleVragen = controller.GetAll() as ObjectResult;

            var ListVragen = alleVragen.Value as List<VragenViewModelReponse>;


            //Assert
            Assert.That(ListVragen.Count(), Is.EqualTo(vraagModels.Count()));

            for (int i = 0; i < ListVragen.Count(); i++)
            {
                Assert.That(ListVragen.ToArray()[i].Id, Is.EqualTo(vraagModels.ToArray()[i].Id));
                Assert.That(ListVragen.ToArray()[i].JsonCorrecteAntwoord, Is.EqualTo(vraagModels.ToArray()[i].JsonCorrecteAntwoord));
                Assert.That(ListVragen.ToArray()[i].JsonMogelijkeAntwoorden, Is.EqualTo(vraagModels.ToArray()[i].JsonMogelijkeAntwoorden));
                Assert.That(ListVragen.ToArray()[i].MaxScoreVraag, Is.EqualTo(vraagModels.ToArray()[i].MaxScoreVraag));
                Assert.That(ListVragen.ToArray()[i].TypeVraagId, Is.EqualTo(vraagModels.ToArray()[i].TypeVraagId));
                Assert.That(ListVragen.ToArray()[i].VraagStelling, Is.EqualTo(vraagModels.ToArray()[i].VraagStelling));
            }
        }

        [Test]
        public void AddVraagCorrect()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<VraagDTO> { DTO = vraagDTO };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.AddVraag(It.IsAny<VraagDTO>())).Returns(response);
            var controller = new VragenController(mockService.Object);

            //Act            
            var vraagViewModel = new VragenViewModelRequest
            {
                //Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"
            };

            var addVraag = controller.Create(vraagViewModel) as ObjectResult;
            var entity = addVraag.Value as VragenViewModelReponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Create(vraagViewModel));
            Assert.That(entity.Id, Is.EqualTo(vraagDTO.Id));
            Assert.That(entity.JsonCorrecteAntwoord, Is.EqualTo(vraagDTO.JsonCorrecteAntwoord));
            Assert.That(entity.JsonMogelijkeAntwoorden, Is.EqualTo(vraagDTO.JsonMogelijkeAntwoorden));
            Assert.That(entity.MaxScoreVraag, Is.EqualTo(vraagDTO.MaxScoreVraag));
            Assert.That(entity.TypeVraagId, Is.EqualTo(vraagDTO.TypeVraagId));
            Assert.That(entity.VraagStelling, Is.EqualTo(vraagDTO.VraagStelling));

        }

        [Test]
        public void AddVraagNull()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<VraagDTO> { DTO = vraagDTO };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.AddVraag(It.IsAny<VraagDTO>())).Returns(response);
            var controller = new VragenController(mockService.Object);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Create(null));
        }

        [Test]
        public void UpdateVraagCorrect()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<VraagDTO> { DTO = vraagDTO };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.Update(It.IsAny<VraagDTO>())).Returns(response);
            var controller = new VragenController(mockService.Object);

            var vraagViewModel = new VragenViewModelReponse
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"
            };

            var updateVragen = controller.Update(vraagViewModel) as ObjectResult;
            var entity = updateVragen.Value as VragenViewModelReponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Update(vraagViewModel));
            Assert.That(entity.Id, Is.EqualTo(vraagViewModel.Id));
            Assert.That(entity.JsonCorrecteAntwoord, Is.EqualTo(vraagDTO.JsonCorrecteAntwoord));
            Assert.That(entity.JsonMogelijkeAntwoorden, Is.EqualTo(vraagDTO.JsonMogelijkeAntwoorden));
            Assert.That(entity.MaxScoreVraag, Is.EqualTo(vraagDTO.MaxScoreVraag));
            Assert.That(entity.TypeVraagId, Is.EqualTo(vraagDTO.TypeVraagId));
            Assert.That(entity.VraagStelling, Is.EqualTo(vraagDTO.VraagStelling));

        }


        [Test]
        public void UpdateVraagNull()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<VraagDTO> { DTO = vraagDTO };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.Update(It.IsAny<VraagDTO>())).Returns(response);
            var controller = new VragenController(mockService.Object);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Update(null));
        }

        [Test]
        public void DeleteVraagCorrect()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<int> { DTO = 1 };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.Delete(1)).Returns(response);
            var controller = new VragenController(mockService.Object);

            //Act            
            var vraagViewModel = new VragenViewModelReponse
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"
            };

            var deleteVraag = controller.Delete(vraagViewModel.Id) as ObjectResult;


            //Assert
            Assert.DoesNotThrow(() => controller.Delete(vraagViewModel.Id));

        }

        [Test]
        public void FindVraagCorrect()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<VraagDTO> { DTO = vraagDTO };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.FindVraag(1)).Returns(response);
            var controller = new VragenController(mockService.Object);

            //Act
            var foundVraag = controller.GetById(1) as ObjectResult;
            var entity = foundVraag.Value as VragenViewModelReponse;

            //Assert
            Assert.That(entity.Id, Is.EqualTo(vraagDTO.Id));
            Assert.That(entity.JsonCorrecteAntwoord, Is.EqualTo(vraagDTO.JsonCorrecteAntwoord));
            Assert.That(entity.JsonMogelijkeAntwoorden, Is.EqualTo(vraagDTO.JsonMogelijkeAntwoorden));
            Assert.That(entity.MaxScoreVraag, Is.EqualTo(vraagDTO.MaxScoreVraag));
            Assert.That(entity.TypeVraagId, Is.EqualTo(vraagDTO.TypeVraagId));
            Assert.That(entity.VraagStelling, Is.EqualTo(vraagDTO.VraagStelling));


        }

        [Test]
        public void FindVraagNull()
        {
            var vraagDTO = new VraagDTO
            {
                Id = 1,
                JsonCorrecteAntwoord = "Correct",
                JsonMogelijkeAntwoorden = "Mogelijk",
                MaxScoreVraag = 10,
                TypeVraagId = 1,
                VraagStelling = "Vraagstelling"

            };

            var response = new Response<VraagDTO> { DTO = vraagDTO };

            //Arrange
            var mockService = new Mock<IVragenService>();
            mockService.Setup(x => x.FindVraag(1)).Returns(response);
            var controller = new VragenController(mockService.Object);

            //Act
            var foundTeam = controller.GetById(5) as ObjectResult;

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), foundTeam);
        }


    }   
    
}