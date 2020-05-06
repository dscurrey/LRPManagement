using DTO;
using LRP.Players.Controllers;
using LRP.Players.Data.Players;
using LRP.Players.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Players.Tests.Controllers
{
    [TestClass]
    public class PlayersControllerTests
    {
        private static class TestData
        {
            public static List<Player> Players()
            {
                return new List<Player>
                {
                    new Player
                    {
                        Id = 1, IsActive = true, LastName = "Doe", FirstName = "John", DateJoined = DateTime.Now
                    },
                    new Player
                    {
                        Id = 2, IsActive = true, LastName = "Smith", FirstName = "John", DateJoined = DateTime.Now
                    },
                    new Player
                    {
                        Id = 3, IsActive = true, LastName = "Johnson", FirstName = "John", DateJoined = DateTime.Now
                    }
                };
            }
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
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as PlayerDTO;
            Assert.AreEqual(testItem.Id, retResult.Id);
            Assert.AreEqual(testItem.FirstName, retResult.FirstName);
            Assert.AreEqual(testItem.LastName, retResult.LastName);
        }

        [TestMethod]
        public async Task GetPlayerNotFoundTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var playerId = 100;

            // Act
            var result = await controller.GetPlayer(playerId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task GetAllPlayersTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);

            // Act
            var result = await controller.GetPlayer();

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as List<PlayerDTO>;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(TestData.Players().Count, retResult.Count);
            foreach (var player in retResult)
            {
                var testItem = TestData.Players().Find(p => p.Id == player.Id);
                Assert.AreEqual(testItem.FirstName, player.FirstName);
                Assert.AreEqual(testItem.LastName, player.LastName);
            }
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
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as PlayerDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(player, retResult);
        }

        [TestMethod]
        public async Task PostPlayerNoIdTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var player = new PlayerDTO
                {LastName = "Doe", FirstName = "Jane", DateJoined = DateTime.Now};

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
        public async Task PostPlayerInvalidNameTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var player = new PlayerDTO
                {LastName = "", DateJoined = DateTime.Now};

            // Act
            var result = await controller.PostPlayer(player);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
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

        [TestMethod]
        public async Task DeletePlayerNotFoundTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var playerId = 100;

            // Act
            var result = await controller.DeletePlayer(playerId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PutPlayerTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, NullLogger<PlayersController>.Instance);
            var playerId = 1;
            var player = new PlayerDTO
            {
                Id = playerId,
                DateJoined = DateTime.Now,
                FirstName = "FIRSTNAME",
                LastName = "LASTNAME"
            };

            // Act
            var result = await controller.PutPlayer(playerId, player);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPlayerNoMatchIdTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, NullLogger<PlayersController>.Instance);
            var playerId = 1;
            var player = new PlayerDTO
            {
                Id = playerId + 1,
                DateJoined = DateTime.Now,
                FirstName = "FIRSTNAME",
                LastName = "LASTNAME"
            };

            // Act
            var result = await controller.PutPlayer(playerId, player);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PutPlayerNotFoundTest()
        {
            // Arrange
            var repo = new FakePlayerRepository(TestData.Players());
            var controller = new PlayersController(repo, null);
            var playerId = 100;
            var player = new PlayerDTO
            {
                Id = playerId,
                DateJoined = DateTime.Now,
                FirstName = "FIRSTNAME",
                LastName = "LASTNAME"
            };

            // Act
            var result = await controller.PutPlayer(playerId, player);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }
    }
}