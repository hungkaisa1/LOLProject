using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOLProject.Controllers;
using LOLProject.Data;
using LOLProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LOLProject_tests
{
    [TestClass]
    public class ChampionControllerTest
    {
        //connect to the DB? Create a mock object to simulate our db connection when testing

        //this is an In-Memory db context > microsoft.EntityFrameworkCore.Inmemory
        private ApplicationDbContext _context;

        //empty list of champions
        List<Champion> champions = new List<Champion>();
        Dictionary<int, Champion> dbMock;

        //declare the controller that will be tested
        ChampionsController controller;

        //How do I fill _context with data? or when?
        //Create a constructor? Rather, create an Initialize method

        [TestInitialize]
        public void TestInitialize()
        {
            //instantiate in-Memory db > similar to startup.cs
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            //create mock data in this db
            //create 1 role
            var role = new Role { RoleId = 10, RoleName = "Test Role", Description = "Test" };
            _context.Roles.Add(role);
            _context.SaveChanges();

            //create 1 position
            var position = new Position { PositionId = 10, PositionName = "Test Position", Description = "Test" };
            _context.Positions.Add(position);
            _context.SaveChanges();

            //Create 3 champions
            champions.Add(new Champion { ChampionId = 10, ChampionName = "Cassiopeia", Role = role, Price = 4800, Position = position });
            champions.Add(new Champion { ChampionId = 11, ChampionName = "Braum", Role = role, Price = 6300, Position = position });
            champions.Add(new Champion { ChampionId = 12, ChampionName = "Jayce", Role = role, Price = 4800, Position = position });
            foreach (var p in champions)
            {
                _context.Champions.Add(p);
            }
            this.dbMock = champions.ToDictionary(c => c.ChampionId, c => c);
            _context.SaveChanges();

            //instaciate the controller class with mock db context
            controller = new ChampionsController(_context);
        }


        [TestMethod]

        //Create (GET)
        public void CreateLoadsCreateView()
        {
            //Arrange

            //Act
            var result = controller.Create();
            var viewResult = (ViewResult)result;
            //Assert
            Assert.AreEqual("Create", viewResult.ViewName);
        }
        [TestMethod]
        public void CreateResultIsNotNull()
        {
            //Arrange

            //Act
            var result = controller.Create();
            var viewResult = (ViewResult)result;
            //Assert
            Assert.IsNotNull("Create", viewResult.ViewName);
        }


        //Edit (GET)
        [TestMethod]
        public async Task Edits()
        {
            //  Arrange
            var id = dbMock.Keys.First();
            var expectedResult = dbMock[id];

            // Act
            var actualResult = await controller.Edit(id);
            var actualViewResult = actualResult as ViewResult;
            var actualResultModel = actualViewResult.Model as Champion;

            // Assert
            Assert.IsNotNull(actualResultModel);
            Assert.AreEqual(expectedResult.ChampionId, actualResultModel.ChampionId);
        }
    }
}