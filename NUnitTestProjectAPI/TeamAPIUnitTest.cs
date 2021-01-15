using API.Controllers;
using API.Mappers;
using API.Viewmodels;
using API.Viewmodels.IngevoerdAntwoord;
using API.Viewmodels.Team;
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
    public class TeamAPIUnitTest
    {

        [SetUp]
        public void Setup()
        {
        }
      
        [Test]
        public void GetAllTeamsCorrect()
        {
            var teamDTOs = new List<TeamDTO>();
            teamDTOs.Add(new TeamDTO
            {
                Id = 1,
                Naam = "Team A"

            });

            IQueryable<TeamDTO> queryableTeamDTOs = teamDTOs.AsQueryable();

            var teamModels = new List<TeamViewModelResponse>();

            foreach (var team in teamDTOs)
            {
                teamModels.Add(TeamViewModelMapper.MapTeamDTOToTeamViewModelResponse(team));
            }

            //Arange

            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.GetAllTeams()).Returns(queryableTeamDTOs);
            var controller = new TeamController(mockService.Object);


            //Act
            var alleTeam = controller.GetAll() as ObjectResult;

            var ListQuizen = alleTeam.Value as List<TeamViewModelResponse>;


            //Assert
            Assert.That(ListQuizen.Count(), Is.EqualTo(teamModels.Count()));

            for (int i = 0; i < ListQuizen.Count(); i++)
            {
                Assert.That(ListQuizen.ToArray()[i].Id, Is.EqualTo(teamModels.ToArray()[i].Id));
                Assert.That(ListQuizen.ToArray()[i].Naam, Is.EqualTo(teamModels.ToArray()[i].Naam));
            }
        }

        [Test]
        public void AddTeamCorrect()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"                 
            };

            var response = new Response<TeamDTO> { DTO = teamDTO };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.AddTeam(It.IsAny<TeamDTO>())).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Act            
            var teamViewModel = new TeamViewModelRequest
            {
                //Id = 1,
                Naam = "Team A"
            };

            var addTeam = controller.Create(teamViewModel) as ObjectResult;
            var entity = addTeam.Value as TeamViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Create(teamViewModel));
            Assert.That(entity.Id, Is.EqualTo(teamDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(teamDTO.Naam));
        }

        [Test]
        public void AddTeamNull()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };

            var response = new Response<TeamDTO> { DTO = teamDTO };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.AddTeam(It.IsAny<TeamDTO>())).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Create(null));
        }

        [Test]
        public void UpdateTeamCorrect()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };
            var response = new Response<TeamDTO> { DTO = teamDTO };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.Update(It.IsAny<TeamDTO>())).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Act            
            var teamViewModel = new TeamViewModelResponse
            {
                Id = 1,
                Naam = "Team A"
            };

            var updateTeam = controller.Update(teamViewModel) as ObjectResult;
            var entity = updateTeam.Value as TeamViewModelResponse;

            //Assert
            Assert.DoesNotThrow(() => controller.Update(teamViewModel));
            Assert.That(entity.Id, Is.EqualTo(teamViewModel.Id));
            Assert.That(entity.Naam, Is.EqualTo(teamViewModel.Naam));
        }


        [Test]
        public void UpdateTeamNull()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };

            var response = new Response<TeamDTO> { DTO = teamDTO };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.Update(It.IsAny<TeamDTO>())).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(controller.Update(null));
        }

        [Test]
        public void DeleteTeamCorrect()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };

            var response = new Response<int> { DTO = 1 };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.Delete(1)).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Act            
            var teamViewModel = new TeamViewModelResponse
            {
                Id = 1,
                Naam = "Team A"
            };

            var deleteTeam = controller.Delete(teamViewModel.Id) as ObjectResult;


            //Assert
            Assert.DoesNotThrow(() => controller.Delete(teamViewModel.Id));

        }

        [Test]
        public void FindTeamCorrect()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Quiz 1"
            };
            var response = new Response<TeamDTO> { DTO = teamDTO };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.FindTeam(1)).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Act
            var foundTeam = controller.GetById(1) as ObjectResult;
            var entity = foundTeam.Value as TeamViewModelResponse;

            //Assert
            Assert.That(entity.Id, Is.EqualTo(teamDTO.Id));
            Assert.That(entity.Naam, Is.EqualTo(teamDTO.Naam));
        }

        [Test]
        public void FindTeamNull()
        {
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };
            var response = new Response<TeamDTO> { DTO = teamDTO };

            //Arrange
            var mockService = new Mock<ITeamService>();
            mockService.Setup(x => x.FindTeam(1)).Returns(response);
            var controller = new TeamController(mockService.Object);

            //Act
            var foundTeam = controller.GetById(5) as ObjectResult;

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), foundTeam);
        }


    }   
    
}