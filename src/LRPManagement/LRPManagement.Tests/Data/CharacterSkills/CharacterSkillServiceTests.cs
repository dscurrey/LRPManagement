using LRPManagement.Data.CharacterSkills;
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

namespace LRPManagement.Tests.Data.CharacterSkills
{
    [TestClass]
    public class CharacterSkillServiceTests
    {
        private static class TestData
        {
            public static List<CharacterSkill> CharacterSkills() => new List<CharacterSkill>
            {
                new CharacterSkill
                {
                    Id = 1, SkillId = 1, CharacterId = 1
                },
                new CharacterSkill
                {
                    Id = 2, SkillId = 3, CharacterId = 4
                },
                new CharacterSkill
                {
                    Id = 3, CharacterId = 1, SkillId = 5
                }
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

        private HttpClient SetupMock_CharacterSkill(int id)
        {
            var expectedHttpResp = TestData.CharacterSkills().FirstOrDefault(b => b.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_CharacterSkill()
        {
            var expectedHttpResp = TestData.CharacterSkills();
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
            var client = SetupMock_CharacterSkill();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhost:1111");
            var service = new CharacterSkillService(null, config.Object, new NullLogger<CharacterSkillService>())
            { Client = client };

            // Act
            var newCharSkill = new CharacterSkill
            {
                Id = 5,
                CharacterId = 1,
                SkillId = 20
            };
            var result = await service.Create(newCharSkill);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCharSkill.Id, result.Id);
            Assert.AreEqual(newCharSkill.CharacterId, result.CharacterId);
            Assert.AreEqual(newCharSkill.SkillId, result.SkillId);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            var charSkill = 2;
            var client = SetupMock_CharacterSkill();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhost:1111/");
            var service = new CharacterSkillService(null, config.Object, new NullLogger<CharacterSkillService>())
            { Client = client };

            // Act
            var result = await service.Delete(charSkill);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetTest()
        {
            // Arrange
            var charSkill = 2;
            var client = SetupMock_CharacterSkill(charSkill);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhos:1111/");
            var service = new CharacterSkillService(null, config.Object, new NullLogger<CharacterSkillService>())
            { Client = client };

            // Act
            var result = await service.Get(charSkill);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.CharacterSkills().FirstOrDefault(c => c.Id == charSkill);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.AreEqual(testItem.CharacterId, result.CharacterId);
            Assert.AreEqual(testItem.SkillId, result.SkillId);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            // Arrange
            var client = SetupMock_CharacterSkill();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhos:1111/");
            var service = new CharacterSkillService(null, config.Object, new NullLogger<CharacterSkillService>())
            { Client = client };

            // Act
            var result = await service.Get();

            // Assert
            Assert.IsNotNull(result);
            foreach (var item in result)
            {
                var testItem = TestData.CharacterSkills().FirstOrDefault(c => c.Id == item.Id);
                Assert.AreEqual(testItem.Id, item.Id);
                Assert.AreEqual(testItem.CharacterId, item.CharacterId);
                Assert.AreEqual(testItem.SkillId, item.SkillId);
            }
        }
    }
}