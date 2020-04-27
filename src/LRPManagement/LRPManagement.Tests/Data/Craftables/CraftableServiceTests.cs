using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DTO;
using LRPManagement.Data.Craftables;
using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace LRPManagement.Tests.Data.Craftables
{
    [TestClass]
    public class CraftableServiceTests
    {
        private static class TestData
        {
            public static List<Craftable> Craftables() => new List<Craftable>
            {
                new Craftable { Id = 1, Name = "Item 1", Requirement = "Requirement 1", Materials = "Materials 1", Effect = "Effect 1", Form = "Form 1"},
                new Craftable { Id = 2, Name = "Item 2", Requirement = "Requirement 2", Materials = "Materials 2", Effect = "Effect 2", Form = "Form 2"},
                new Craftable { Id = 3, Name = "Item 3", Requirement = "Requirement 3", Materials = "Materials 3", Effect = "Effect 3", Form = "Form 3"},
                new Craftable { Id = 4, Name = "Item 4", Requirement = "Requirement 4", Materials = "Materials 4", Effect = "Effect 4", Form = "Form 4"}
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

        private HttpClient SetupMock_Craftable(int id)
        {
            var expectedHttpResp = TestData.Craftables().FirstOrDefault(b => b.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_Craftable()
        {
            var expectedHttpResp = TestData.Craftables();
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        [TestMethod]
        public void CreateCraftableTest()
        {
            // Arrange
            var client = SetupMock_Craftable();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CraftableService(null, config.Object, new NullLogger<CraftableService>())
                {Client = client};
        }

        [TestMethod]
        public async Task DeleteCraftableTest()
        {
            // Arrange
            var item = 2;
            var client = SetupMock_Craftable();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CraftableService(null, config.Object, new NullLogger<CraftableService>())
                {Client = client};

            // Act
            var result = await service.DeleteCraftable(item);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(item, result);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            // Arrange
            var client = SetupMock_Craftable();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CraftableService(null, config.Object, new NullLogger<CraftableService>())
                { Client = client };

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            foreach (var item in result)
            {
                var testItem = TestData.Craftables().FirstOrDefault(c => c.Id == item.Id);
                Assert.AreEqual(testItem.Id, item.Id);
                Assert.AreEqual(testItem.Name, item.Name);
                Assert.AreEqual(testItem.Requirement, item.Requirement);
                Assert.AreEqual(testItem.Form, item.Form);
                Assert.AreEqual(testItem.Effect, item.Effect);
                Assert.AreEqual(testItem.Materials, item.Materials);
            }
        }

        [TestMethod]
        public async Task GetCraftableTest()
        {
            // Arrange
            var itemId = 2;
            var client = SetupMock_Craftable(itemId);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CraftableService(null, config.Object, new NullLogger<CraftableService>())
                { Client = client };

            // Act
            var result = await service.GetCraftable(itemId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Craftables().FirstOrDefault(c => c.Id == itemId);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.AreEqual(testItem.Name, result.Name);
            Assert.AreEqual(testItem.Requirement, result.Requirement);
            Assert.AreEqual(testItem.Form, result.Form);
            Assert.AreEqual(testItem.Effect, result.Effect);
            Assert.AreEqual(testItem.Materials, result.Materials);
        }

        [TestMethod]
        public async Task UpdateCraftableObjTest()
        {
            // Arrange
            var client = SetupMock_Craftable();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CraftableService(null, config.Object, new NullLogger<CraftableService>())
                { Client = client };

            // Act
            var updItem = new Craftable
            {
                Id = 2, Name = "Updated Name", Requirement = "Updated", Materials = "Updated", Effect = "Updated", Form = "Updated"
            };
            var result = await service.UpdateCraftable(updItem);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updItem.Id, result.Id);
            Assert.AreEqual(updItem.Name, result.Name);
            Assert.AreEqual(updItem.Requirement, result.Requirement);
            Assert.AreEqual(updItem.Form, result.Form);
            Assert.AreEqual(updItem.Effect, result.Effect);
            Assert.AreEqual(updItem.Materials, result.Materials);
        }

        [TestMethod]
        public async Task UpdateCraftableTest()
        {
            // Arrange
            var client = SetupMock_Craftable();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CraftableService(null, config.Object, new NullLogger<CraftableService>())
                { Client = client };

            // Act
            var updItem = new CraftableDTO
            {
                Id = 2,
                Name = "Updated Name",
                Requirement = "Updated",
                Materials = "Updated",
                Effect = "Updated",
                Form = "Updated"
            };
            var result = await service.UpdateCraftable(updItem);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updItem.Id, result.Id);
            Assert.AreEqual(updItem.Name, result.Name);
            Assert.AreEqual(updItem.Requirement, result.Requirement);
            Assert.AreEqual(updItem.Form, result.Form);
            Assert.AreEqual(updItem.Effect, result.Effect);
            Assert.AreEqual(updItem.Materials, result.Materials);
        }
    }
}