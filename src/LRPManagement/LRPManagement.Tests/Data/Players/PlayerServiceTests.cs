using DTO;
using LRPManagement.Data.Players;
using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LRPManagement.Tests.Data.Players
{
    [TestClass]
    public class PlayerServiceTests
    {
        private static class TestData
        {
            public static List<Player> Players()
            {
                return new List<Player>
                {
                    new Player {Id = 1, FirstName = "FirstName", LastName = "LastName", AccountRef = "", PlayerRef = 1},
                    new Player {Id = 2, FirstName = "FirstName", LastName = "LastName", AccountRef = "", PlayerRef = 1},
                    new Player {Id = 3, FirstName = "FirstName", LastName = "LastName", AccountRef = "", PlayerRef = 1},
                    new Player {Id = 4, FirstName = "FirstName", LastName = "LastName", AccountRef = "", PlayerRef = 1},
                    new Player {Id = 5, FirstName = "FirstName", LastName = "LastName", AccountRef = "", PlayerRef = 1},
                };
            }
        }

        private Mock<HttpMessageHandler> CreateHttpMock(HttpResponseMessage expected)
        {
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>
                (
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(expected)
                .Verifiable();
            return mock;
        }

        private HttpClient SetupMock_Player(int id)
        {
            var expectedHttpResp = TestData.Players().FirstOrDefault(b => b.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_Player()
        {
            var expectedHttpResp = TestData.Players();
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            // Arrange
            var client = SetupMock_Player();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["PlayersURL"]).Returns("https://localhost:1111/");
            var service = new PlayerService(null, config.Object, new NullLogger<PlayerService>())
                {Client = client};

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            foreach (var playerDto in result)
            {
                var testItem = TestData.Players().FirstOrDefault(p => p.Id == playerDto.Id);
                Assert.AreEqual(testItem.Id, playerDto.Id);
                Assert.AreEqual(testItem.LastName, playerDto.LastName);
                Assert.AreEqual(testItem.AccountRef, playerDto.AccountRef);
                Assert.AreEqual(testItem.FirstName, playerDto.FirstName);
            }
        }

        [TestMethod]
        public async Task GetPlayerTest()
        {
            // Arrange
            var playerId = 3;
            var client = SetupMock_Player(playerId);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["PlayersURL"]).Returns("https://localhost:1111/");
            var service = new PlayerService(null, config.Object, new NullLogger<PlayerService>())
                {Client = client};

            // Act
            var result = await service.GetPlayer(playerId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Players().FirstOrDefault(p => p.Id == result.Id);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.AreEqual(testItem.LastName, result.LastName);
            Assert.AreEqual(testItem.AccountRef, result.AccountRef);
            Assert.AreEqual(testItem.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task UpdatePlayerTest()
        {
            // Arrange
            var client = SetupMock_Player();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["PlayersURL"]).Returns("https://localhost:1111/");
            var service = new PlayerService(null, config.Object, new NullLogger<PlayerService>())
                {Client = client};

            // Act
            var updPlayer = new PlayerDTO
                {Id = 10, FirstName = "NEW", LastName = "NEW", AccountRef = "", DateJoined = DateTime.Now};
            var result = await service.UpdatePlayer(updPlayer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updPlayer.Id, result.Id);
            Assert.AreEqual(updPlayer.LastName, result.LastName);
            Assert.AreEqual(updPlayer.AccountRef, result.AccountRef);
            Assert.AreEqual(updPlayer.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task CreatePlayerTest()
        {
            // Arrange
            var client = SetupMock_Player();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["PlayersURL"]).Returns("https://localhost:1111/");
            var service = new PlayerService(null, config.Object, new NullLogger<PlayerService>())
                {Client = client};

            // Act
            var newPlayer = new PlayerDTO
            {
                Id = 12,
                AccountRef = "",
                DateJoined = DateTime.Now,
                FirstName = "NEWNAME",
                LastName = "NEWNAME"
            };
            var result = await service.CreatePlayer(newPlayer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newPlayer.Id, result.Id);
            Assert.AreEqual(newPlayer.LastName, result.LastName);
            Assert.AreEqual(newPlayer.AccountRef, result.AccountRef);
            Assert.AreEqual(newPlayer.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task DeletePlayerTest()
        {
            // Arrange
            var player = 2;
            var client = SetupMock_Player();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["PlayersURL"]).Returns("https://localhost:1111/");
            var service = new PlayerService(null, config.Object, new NullLogger<PlayerService>())
                {Client = client};

            // Act
            var result = await service.DeletePlayer(player);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(player, result);
        }
    }
}