using DL.Context;
using DL.Models;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject2DAL
{
    public class RondeDALTests

    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateRonde()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase1")
                .Options;
            int rows = 0;
            Ronde responseRonde = null;

            var ronde = new Ronde
            {
                Naam = "Ronde "
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                responseRonde = repository.Add(ronde);
                rows = repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.AreEqual(1, rows);
                Assert.IsNotNull(responseRonde.Id);
                Assert.IsNotEmpty(responseRonde.Id.ToString());
                Assert.That(ronde.Naam, Is.EqualTo(responseRonde.Naam));                

                Assert.AreEqual(1, context.Ronden.Count());
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
            Ronde responseAdd = null; 
            Ronde responseFirstOrDefault = null;

            var ronde = new Ronde
            {
                Naam = "Ronde 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                responseAdd = repository.Add(ronde);
                rows = repository.SaveChanges();
                responseFirstOrDefault = repository.FirstOrDefault(x => x.Id == 1);
            }

            //Assert
            using (var context = new DataContext(options))
            { 
                Assert.IsNotNull(responseFirstOrDefault.Id);
                Assert.IsNotEmpty(responseFirstOrDefault.Id.ToString());
                Assert.That(ronde.Naam, Is.EqualTo(responseFirstOrDefault.Naam));
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
            IEnumerable<Ronde> responseGetAll = null;
            Ronde firstResponse = null;
            

            var ronde = new Ronde
            {
                Naam = "Ronde 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                repository.Add(ronde);
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
                Assert.That(ronde.Naam, Is.EqualTo(firstResponse.Naam));                
            }
        }

        [Test]
        public void GetByID()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase4")
                .Options;
            Ronde responseGetById = null;

            var ronde = new Ronde
            {
                Naam = "Ronde 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                repository.Add(ronde);
                repository.SaveChanges();
                responseGetById = repository.GetById(1);
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetById);
                Assert.IsNotEmpty(responseGetById.Id.ToString());
                Assert.That(ronde.Naam, Is.EqualTo(responseGetById.Naam));
            }
        }

        [Test]
        public void GetWhere()
        {
            //Act
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase5")
                .Options;
            IQueryable<Ronde> responseGetWhere = null;
            Ronde reponseFirstOfWhere = null;

            var ronde = new Ronde
            {
                Naam = "Ronde 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                repository.Add(ronde);
                repository.SaveChanges();
                responseGetWhere = repository.GetWhere(x => x.Naam == "Ronde 1");
                reponseFirstOfWhere = responseGetWhere.FirstOrDefault();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.IsNotNull(responseGetWhere);
                Assert.IsNotEmpty(reponseFirstOfWhere.Id.ToString());
                Assert.That(ronde.Naam, Is.EqualTo(reponseFirstOfWhere.Naam));
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

            var ronde = new Ronde
            {
                Naam = "Quiz 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                var responseAdd = repository.Add(ronde);
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

            Ronde responseRondeUpdated = null;

            var ronde = new Ronde
            {
                Naam = "Ronde 1"
            };

            var updateRonde = new Ronde
            {
                Id = 1,
                Naam = "Ronde 1"
            };

            //Arrange
            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                var responseAdd = repository.Add(ronde);                
                repository.SaveChanges();
                
            }

            using (var context = new DataContext(options))
            {
                var repository = new SQLRepository<Ronde>(context);
                responseRondeUpdated = repository.Update(updateRonde);
                repository.SaveChanges();
            }

            //Assert
            using (var context = new DataContext(options))
            {
                Assert.That(updateRonde.Naam, Is.EqualTo(responseRondeUpdated.Naam));
            }
        }
    }
}