using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LRPManagement.Data.Bonds;
using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace LRPManagement.Tests.Data.Bonds
{
    [TestClass]
    public class BondServiceTests
    {
        private static class TestData
        {
            public static List<Bond> Bonds() => new List<Bond>
            {
                new Bond {Id = 1, CharacterId = 1, ItemId = 1},
                new Bond { Id = 2, CharacterId = 1, ItemId = 22},
                new Bond { Id = 3, CharacterId = 2, ItemId = 4}
            };
        }

        private Mock<HttpMessageHandler> CreateHttpMock(HttpResponseMessage expected)
        {
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expected)
                .Verifiable();
            return mock;
        }

        private HttpClient SetupMock_Bond(int id)
        {
            var expectedHttpResp = TestData.Bonds().FirstOrDefault(b => b.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_Bond()
        {
            var expectedHttpResp = TestData.Bonds();
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        [TestMethod]
        public async Task CreateTest()
        {
            // Arrange
            var client = SetupMock_Bond();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var bondRepo = new FakeBondRepository(TestData.Bonds());
            var service = new BondService(null, config.Object, new NullLogger<BondService>())
                { Client = client };

            // Act
            var newBond = new Bond {Id = 5, ItemId = 7, CharacterId = 2};
            var result = await service.Create(newBond);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newBond.CharacterId, result.CharacterId);
            Assert.AreEqual(newBond.ItemId, result.ItemId);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            var bondId = 1;
            var client = SetupMock_Bond(bondId);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var bondRepo = new FakeBondRepository(TestData.Bonds());
            var service = new BondService(null, config.Object, new NullLogger<BondService>())
                { Client = client };

            // Act
            var result = await service.Delete(bondId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            // Arrange
            var client = SetupMock_Bond();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var bondRepo = new FakeBondRepository(TestData.Bonds());
            var service = new BondService(null, config.Object, new NullLogger<BondService>())
                {Client = client};

            // Act
            var result = await service.Get();

            // Assert
            Assert.IsNotNull(result);
            foreach (var bond in result)
            {
                var testItem = TestData.Bonds().FirstOrDefault(b => b.Id == bond.Id);
                Assert.AreEqual(testItem.CharacterId, bond.CharacterId);
                Assert.AreEqual(testItem.ItemId, bond.ItemId);
            }
        }

        [TestMethod]
        public async Task GetTest()
        {
            // Arrange
            var bondId = 2;
            var client = SetupMock_Bond(bondId);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var bondRepo = new FakeBondRepository(TestData.Bonds());
            var service = new BondService(null, config.Object, new NullLogger<BondService>())
                { Client = client };

            // Act
            var result = await service.Get(bondId);

            // Arrange
            Assert.IsNotNull(result);
            var testItem = TestData.Bonds().FirstOrDefault(b => b.Id == bondId);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.AreEqual(testItem.ItemId, result.ItemId);
            Assert.AreEqual(testItem.CharacterId, result.CharacterId);
        }
    }
}