using API.Controllers;
using API.Mappers;
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
    public class IngevoerdeAntwoordenAPIUnitTest
    {

        [SetUp]
        public void Setup()
        {
        }
       
        [Test]
        public void GetAllIngevoerdAntwoordenCorrect()
        {
            var ingevoerdAntwoordenDTOs = new List<IngevoerdAntwoordDTO>();
            ingevoerdAntwoordenDTOs.Add(new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",                
                GescoordeScore = 5,
                TeamId = 1,
                VraagId = 1,
                
            });

            IQueryable<IngevoerdAntwoordDTO> queryableIngevoerdeAntwoordenDTOs = ingevoerdAntwoordenDTOs.AsQueryable();

            var antwoordenModels = new List<IngevoerdAntwoordViewModelResponse>();

            foreach (var antwoord in ingevoerdAntwoordenDTOs)
            {
                antwoordenModels.Add(IngevoerdAntwoordViewModelMapper.MapIngevoerdAntwoordDTOToIngevoerdAntwoordViewModelResponse(antwoord));
            }

            //Arange

            var mockService = new Mock<IIngevoerdAntwoordService> ();
            mockService.Setup(x => x.GetAllIngevoerdeAntwoord()).Returns(queryableIngevoerdeAntwoordenDTOs);
            var controller = new IngevoerdAntwoordController(mockService.Object);


            //Act
            var alleIngevoerdeAntwoorden = controller.GetAll() as ObjectResult;

            var ListVanAntwoorden = alleIngevoerdeAntwoorden.Value as List<IngevoerdAntwoordViewModelResponse>;


            //Assert
            Assert.That(ListVanAntwoorden.Count(), Is.EqualTo(antwoordenModels.Count()));

            for (int i = 0; i < ListVanAntwoorden.Count(); i++)
            {
                Assert.That(ListVanAntwoorden.ToArray()[i].Id, Is.EqualTo(antwoordenModels.ToArray()[i].Id));
                Assert.That(ListVanAntwoorden.ToArray()[i].JsonAntwoord, Is.EqualTo(antwoordenModels.ToArray()[i].JsonAntwoord));
                Assert.That(ListVanAntwoorden.ToArray()[i].GescoordeScore, Is.EqualTo(antwoordenModels.ToArray()[i].GescoordeScore));
                Assert.That(ListVanAntwoorden.ToArray()[i].JsonAntwoord, Is.EqualTo(antwoordenModels.ToArray()[i].JsonAntwoord));
                Assert.That(ListVanAntwoorden.ToArray()[i].VraagId, Is.EqualTo(antwoordenModels.ToArray()[i].VraagId));
                Assert.That(ListVanAntwoorden.ToArray()[i].TeamId, Is.EqualTo(antwoordenModels.ToArray()[i].TeamId));
            }
        }
        
        [Test]
        public void AddIngevoerdAntwoordCorrect()
        {
            
            var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",                
                GescoordeScore = 5,
                TeamId = 1,                


            };
            var response = new Response<IngevoerdAntwoordDTO> { DTO = antwoordDTO };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.AddIngevoerdAntwoord(It.IsAny<IngevoerdAntwoordDTO>())).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Act            
            var ingevoerdAntwoordenViewModel = new IngevoerdAntwoordViewModelRequest
            {
                //Id = 1,
                Antwoord = "JsonAntwoord",
                GescoordeScore = 5,
                VraagId = 1,
                TeamId = 1
            };

            var addIngevoerdAntwoord = controller.Create(ingevoerdAntwoordenViewModel) as ObjectResult;
            var entity = addIngevoerdAntwoord.Value as IngevoerdAntwoordViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Create(ingevoerdAntwoordenViewModel));
            Assert.That(entity.Id, Is.EqualTo(antwoordDTO.Id));
            Assert.That(entity.JsonAntwoord, Is.EqualTo(antwoordDTO.JsonAntwoord));
            Assert.That(entity.GescoordeScore, Is.EqualTo(antwoordDTO.GescoordeScore));
            Assert.That(entity.TeamId, Is.EqualTo(antwoordDTO.TeamId));


        }

        [Test]
        public void AddIngevoerdAntwoordNull()
        {
            var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",               
                GescoordeScore = 5,
                TeamId = 1,
                VraagId = 1
            };
            var response = new Response<IngevoerdAntwoordDTO> { DTO = antwoordDTO };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.AddIngevoerdAntwoord(It.IsAny<IngevoerdAntwoordDTO>())).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Create(null));
        }

        [Test]
        public void UpdateIngevoerdAntwoordCorrect()
        {
            var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",                
                GescoordeScore = 5,     
                TeamId = 1,
                VraagId = 1
            };

            var response = new Response<IngevoerdAntwoordDTO> { DTO = antwoordDTO };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.Update(It.IsAny<IngevoerdAntwoordDTO>())).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Act            
            var ingevoerdAntwoordenViewModel = new IngevoerdAntwoordViewModelResponse
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                GescoordeScore = 5,
                VraagId = 1,
                TeamId = 1
            };

            var updateIngevoerdAntwoord = controller.Update(ingevoerdAntwoordenViewModel) as ObjectResult;
            var entity = updateIngevoerdAntwoord.Value as IngevoerdAntwoordViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Update(ingevoerdAntwoordenViewModel));
            Assert.That(entity.Id, Is.EqualTo(antwoordDTO.Id));
            Assert.That(entity.JsonAntwoord, Is.EqualTo(antwoordDTO.JsonAntwoord));
            Assert.That(entity.GescoordeScore, Is.EqualTo(antwoordDTO.GescoordeScore));
            Assert.That(entity.TeamId, Is.EqualTo(antwoordDTO.TeamId));


    
        }
    

        [Test]
        public void UpdateIngevoerdAntwoordNull()
        {
            var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",                
                GescoordeScore = 5,               
                TeamId = 1, 
                VraagId = 1
            };

            var response = new Response<IngevoerdAntwoordDTO> { DTO = antwoordDTO };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.Update(It.IsAny<IngevoerdAntwoordDTO>())).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Update(null));
        }

        [Test]
        public void DeleteAntwoordCorrect()
        {
            /*var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",               
                GescoordeScore = 5,                
                TeamId = 1,
                VraagId = 1
            };*/
            var response = new Response<int> { DTO = 1 };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.Delete(1)).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Act            
            var ingevoerdAntwoordenViewModel = new IngevoerdAntwoordViewModelResponse
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",
                GescoordeScore = 5,
                VraagId = 1,
                TeamId = 1
            };

            var deleteIngevoerdAntwoord = controller.Delete(ingevoerdAntwoordenViewModel.Id) as ObjectResult;
            //var entity = deleteIngevoerdAntwoord.Value as IngevoerdAntwoordViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Delete(ingevoerdAntwoordenViewModel.Id));

        }

        [Test]
        public void FindAntwoordCorrect()
        {
            var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",               
                GescoordeScore = 5,               
                TeamId = 1,
                VraagId = 1
            };

            var response = new Response<IngevoerdAntwoordDTO> { DTO = antwoordDTO };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.FindIngevoerdAntwoord(1)).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Act
            var foundAntwoord = controller.GetById(1) as ObjectResult;
            var entity = foundAntwoord.Value as IngevoerdAntwoordViewModelResponse;

            //Assert
            Assert.That(entity.Id, Is.EqualTo(antwoordDTO.Id));
            Assert.That(entity.JsonAntwoord, Is.EqualTo(antwoordDTO.JsonAntwoord));



        }

        [Test]
        public void FindAntwoordNull()
        {
            var antwoordDTO = new IngevoerdAntwoordDTO
            {
                Id = 1,
                JsonAntwoord = "JsonAntwoord",               
                GescoordeScore = 5,                
                TeamId = 1,
                VraagId = 1
            };

            var response = new Response<IngevoerdAntwoordDTO> { DTO = antwoordDTO };

            //Arrange
            var mockService = new Mock<IIngevoerdAntwoordService>();
            mockService.Setup(x => x.FindIngevoerdAntwoord(1)).Returns(response);
            var controller = new IngevoerdAntwoordController(mockService.Object);

            //Act
            var foundAntwoord = controller.GetById(5) as ObjectResult;

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult),foundAntwoord);
        }


    }   
    
}