using Businessmodels.DTO_S;
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
    public class TeamTests
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
        public void GetAllTeamsCorrect()
        {
            var teams = new List<Team>();
            teams.Add(new Team
            {
                Id = 1,
                Naam = "Team A"
            }) ;

            IQueryable<Team> queryableTeams = teams.AsQueryable();

            var teamsDTO = new List<TeamDTO>();

            foreach (var team in teams)
            {
                teamsDTO.Add(TeamMapper.MapTeamModelToTeamDTO(team));
            }

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.GetAll()).Returns(queryableTeams);

            var teamService = new TeamService(teamRepo.Object);

            //Act
            var allTeams = teamService.GetAllTeams();

            //Assert
            Assert.That(allTeams.Count(), Is.EqualTo(teamsDTO.Count()));

            for (int i = 0; i < allTeams.Count(); i++)
            {
                Assert.That(allTeams.ToArray()[i].Id, Is.EqualTo(teamsDTO.ToArray()[i].Id));
                Assert.That(allTeams.ToArray()[i].Naam, Is.EqualTo(teamsDTO.ToArray()[i].Naam));
            }
        }

        [Test]
        public void AddTeamCorrect() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.Add(It.IsAny<Team>())).Returns(team);

            var teamService = new TeamService(teamRepo.Object);

            //Act
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };

            //Assert
            Assert.DoesNotThrow(() => teamService.AddTeam(teamDTO));

            
        }


    

        [Test]
        public void AddTeamNull() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.Add(It.IsAny<Team>())).Returns(team);

            var teamService = new TeamService(teamRepo.Object);

            //Assert
            Assert.IsTrue(teamService.AddTeam(null).DidError);
            Assert.IsNull(teamService.AddTeam(null).DTO);
        }

        [Test]
        public void UpdateTeamCorrect() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.Update(It.IsAny<Team>())).Returns(team);

            var teamService = new TeamService(teamRepo.Object);

            //Act
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };
            teamService.Update(teamDTO);

            //Assert
            Assert.DoesNotThrow(() => teamService.Update(teamDTO));
        }

        [Test]
        public void UpdateTeamNull() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.Update(It.IsAny<Team>())).Returns(team);

            var teamService = new TeamService(teamRepo.Object);

            //Assert
            Assert.IsTrue(teamService.Update(null).DidError);
        }

        [Test]
        public void DeleteTeamCorrect() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.Remove(1)).Returns(true);

            var teamService = new TeamService(teamRepo.Object);

            //Act
            var teamDTO = new TeamDTO
            {
                Id = 1,
                Naam = "Team A"
            };

            //Assert
            Assert.IsFalse(teamService.Delete(teamDTO.Id).DidError);

            Assert.IsTrue(teamService.FindTeam(team.Id).DidError);
            Assert.IsNull(teamService.FindTeam(team.Id).DTO);

        }

        [Test]
        public void FindTeamCorrect() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.GetById(1)).Returns(team);

            var teamService = new TeamService(teamRepo.Object);

            //Assert
            var teamDTO = teamService.FindTeam(1);
            Assert.That(team.Id, Is.EqualTo(teamDTO.DTO.Id));
            Assert.That(team.Naam, Is.EqualTo(teamDTO.DTO.Naam));
        }

        [Test]
        public void FindTeamWithIdThatDoesNotExistFails() {
            var team = new Team
            {
                Id = 1,
                Naam = "Team A"
            };

            //Arange
            var teamRepo = new Mock<ISQLRepository<Team>>();
            teamRepo.Setup(x => x.GetById(1)).Returns(team);

            var teamService = new TeamService(teamRepo.Object);

            //Assert
            Assert.IsTrue(teamService.FindTeam(5).DidError);
            Assert.IsNull(teamService.FindTeam(5).DTO);
        }
    }
}