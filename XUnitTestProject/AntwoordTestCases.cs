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
using Xunit;

namespace XUnitTestProject
{
    public class AntwoordTestCases
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Fact]
        public void GetAllAntwoordenCorrect() {
            var antwoorden = new List<Antwoord>();
            antwoorden.Add(new Antwoord
            {
                JsonAntwoord = "testAntwoord",
                Id = 1,
                Vraag = new Vraag { Id = 1, MaxScoreVraag = 10, TypeVraag = TypeVraag.Open, TypeVraagId = 2, VraagStelling = "Dit is de vraag die gesteld word aan de quizers"},
                VraagId = 1
            });

            IQueryable<Antwoord> queryableAntwoorden = antwoorden.AsQueryable();

            var antwoordenDTO = new List<AntwoordDTO>();

            foreach (var antwoord in antwoorden) {
                antwoordenDTO.Add(AntwoordMapper.MapAntwoordModelToAntwoordDTO(antwoord));
            }

            //Arange
            var antwoordRepo = new Mock<ISQLRepository<Antwoord>>();
            antwoordRepo.Setup(x => x.GetAll()).Returns(queryableAntwoorden);

            var antwoordService = new AntwoordService(antwoordRepo.Object);

            //Act
            var allAntwoorden = antwoordService.GetAllAntwoorden();

            //Assert
            Assert.That(allAntwoorden.Count(), Is.EqualTo(antwoordenDTO.Count()));

            for (int i = 0; i < allAntwoorden.Count(); i++) {
                Assert.That(allAntwoorden.ToArray()[i].Id, Is.EqualTo(antwoordenDTO.ToArray()[i].Id));
                Assert.That(allAntwoorden.ToArray()[i].JsonAntwoord, Is.EqualTo(antwoordenDTO.ToArray()[i].JsonAntwoord));
                Assert.That(allAntwoorden.ToArray()[i].VraagId, Is.EqualTo(antwoordenDTO.ToArray()[i].VraagId));
                Assert.That(allAntwoorden.ToArray()[i].VraagDTO.Id, Is.EqualTo(antwoordenDTO.ToArray()[i].VraagDTO.Id));
                Assert.That(allAntwoorden.ToArray()[i].VraagDTO.MaxScoreVraag, Is.EqualTo(antwoordenDTO.ToArray()[i].VraagDTO.MaxScoreVraag));
                Assert.That(allAntwoorden.ToArray()[i].VraagDTO.TypeVraagDTO, Is.EqualTo(antwoordenDTO.ToArray()[i].VraagDTO.TypeVraagDTO));
                Assert.That(allAntwoorden.ToArray()[i].VraagDTO.VraagStelling, Is.EqualTo(antwoordenDTO.ToArray()[i].VraagDTO.VraagStelling));
            }
        }
    }
}
