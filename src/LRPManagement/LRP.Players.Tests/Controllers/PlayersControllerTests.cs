using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRP.Players.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using LRP.Players.Data.Players;
using LRP.Players.Models;
using Microsoft.AspNetCore.Mvc;

namespace LRP.Players.Controllers.Tests
{
    [TestClass]
    public class PlayersControllerTests
    {
        private static class TestData
        {
            public static List<Player> Players() => new List<Player>
            {
                new Player { Id = 1, IsActive = true, LastName = "Doe", FirstName = "John", DateJoined = DateTime.Now},
                new Player { Id = 2, IsActive = true, LastName = "Smith", FirstName = "John", DateJoined = DateTime.Now}
            };
        }

        [TestMethod]
        public async Task GetPlayerTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var playerId = 1;

            // Act
            var result = await controller.GetPlayer(playerId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Players().Find(c => c.Id == playerId);
            Assert.AreEqual(testItem.Id, result.Value.Id);
            Assert.AreEqual(testItem.FirstName, result.Value.FirstName);
            Assert.AreEqual(testItem.LastName, result.Value.LastName);
        }

        [TestMethod]
        public async Task PostPlayerTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var player = new PlayerDTO
                {Id = 3, LastName = "Doe", FirstName = "Jane", DateJoined = DateTime.Now};

            // Act
            var result = await controller.PostPlayer(player);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as PlayerDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(player, retResult);
        }

        [TestMethod]
        public async Task DeletePlayerTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var playerId = 1;

            // Act
            var result = await controller.DeletePlayer(playerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(playerId, result.Value.Id);
        }
    }
}