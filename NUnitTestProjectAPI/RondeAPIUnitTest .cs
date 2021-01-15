using API.Controllers;
using API.Mappers;
using API.Viewmodels;
using API.Viewmodels.IngevoerdAntwoord;
using API.Viewmodels.Rondes;
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
    public class RondeAPIUnitTest
    {
        RondeController controller;
        Mock<IRondeService> rondeService;

        [SetUp]
        public void Setup()
        {
            rondeService = new Mock<IRondeService>();
            controller = new RondeController(rondeService.Object);
        }


        [Test]
        public void GetAllRondesCorrect()
        {
            var rondeDTOs = new List<RondeDTO>();
            rondeDTOs.Add(new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"

            });

            IQueryable<RondeDTO> queryableRondeDTOs = rondeDTOs.AsQueryable();

            var rondeModels = new List<RondeViewModelResponse>();

            foreach (var ronde in rondeDTOs)
            {
                rondeModels.Add(RondeViewModelMapper.MapRondeDTOToRondeViewModelResponse(ronde));
            }

            //Arange
            rondeService.Setup(x => x.GetAllRondes()).Returns(queryableRondeDTOs);

            //Act
            var alleRondes = controller.GetAll() as ObjectResult;
            var ListRondes = alleRondes.Value as List<RondeViewModelResponse>;


            //Assert
            Assert.That(ListRondes.Count(), Is.EqualTo(rondeModels.Count()));

            for (int i = 0; i < ListRondes.Count(); i++)
            {
                Assert.That(ListRondes.ToArray()[i].Id, Is.EqualTo(rondeModels.ToArray()[i].Id));
                Assert.That(ListRondes.ToArray()[i].Naam, Is.EqualTo(rondeModels.ToArray()[i].Naam));
            }
        }

        [Test]
        public void AddRondeCorrect()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var response = new Response<RondeDTO> { DTO = rondeDTO };

            //Arrange
            rondeService.Setup(x => x.AddRonde(It.IsAny<RondeDTO>())).Returns(response);

            //Act            
            var rondeViewModel = new RondeViewModelRequest
            {
                Naam = "Ronde 1"
            };

            var addRonde = controller.Create(rondeViewModel) as ObjectResult;
            var entity = addRonde.Value as RondeViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Create(rondeViewModel));
            Assert.That(entity.Id, Is.EqualTo(rondeDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(rondeDTO.Naam));
        }

        [Test]
        public void AddRondeNull()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var response = new Response<RondeDTO> { DTO = rondeDTO };

            //Arrange
            rondeService.Setup(x => x.AddRonde(It.IsAny<RondeDTO>())).Returns(response);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Create(null));
        }

        [Test]
        public void UpdateRondeCorrect()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var response = new Response<RondeDTO> { DTO = rondeDTO };

            //Arrange
            rondeService.Setup(x => x.Update(It.IsAny<RondeDTO>())).Returns(response);

            //Act            
            var rondeViewModel = new RondeViewModelResponse
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var updateRonde = controller.Update(rondeViewModel) as ObjectResult;
            var entity = updateRonde.Value as RondeViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Update(rondeViewModel));
            Assert.That(entity.Id, Is.EqualTo(rondeDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(rondeDTO.Naam));
        }


        [Test]
        public void UpdateRondeNull()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };
            var response = new Response<RondeDTO> { DTO = rondeDTO };

            //Arrange
            rondeService.Setup(x => x.Update(It.IsAny<RondeDTO>())).Returns(response);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Update(null));
        }

        [Test]
        public void DeleteRondeCorrect()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var response = new Response<int> { DTO = 1 };

            //Arrange
            rondeService.Setup(x => x.Delete(1)).Returns(response);

            //Act            
            var rondeViewModel = new RondeViewModelResponse
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var deleteRonde = controller.Delete(rondeViewModel.Id) as ObjectResult;

            //Assert
            Assert.DoesNotThrow(() => controller.Delete(rondeViewModel.Id));

        }

        [Test]
        public void FindRondeCorrect()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var response = new Response<RondeDTO> { DTO = rondeDTO };

            //Arrange
            rondeService.Setup(x => x.FindRonde(1)).Returns(response);

            //Act
            var foundRonde = controller.GetById(1) as ObjectResult;
            var entity = foundRonde.Value as RondeViewModelResponse;

            //Assert
            Assert.That(entity.Id, Is.EqualTo(rondeDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(rondeDTO.Naam));
        }

        [Test]
        public void FindRondeNull()
        {
            var rondeDTO = new RondeDTO
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            var response = new Response<RondeDTO> { DTO = rondeDTO };

            //Arrange
            rondeService.Setup(x => x.FindRonde(1)).Returns(response);

            //Act
            var foundRonde = controller.GetById(5) as ObjectResult;

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), foundRonde);
        }


    }

}