using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DTO;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace LRPManagement.Tests.Data.Skills
{
    [TestClass]
    public class SkillServiceTests
    {
        private static class TestData
        {
            public static List<Skill> Skills() => new List<Skill>
            {
                new Skill { Id = 1, Name = "Skill 1", SkillRef = 1, XpCost = 2},
                new Skill { Id = 2, Name = "Skill 2", SkillRef = 2, XpCost = 2},
                new Skill { Id = 3, Name = "Skill 3", SkillRef = 3, XpCost = 8}
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

        private HttpClient SetupMock_Skill(int id)
        {
            var expectedHttpResp = TestData.Skills().FirstOrDefault(b => b.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_Skill()
        {
            var expectedHttpResp = TestData.Skills();
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        [TestMethod]
        public async Task CreateSkillTest()
        {
            // Arrange
            var client = SetupMock_Skill();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhost:1111/");
            var service = new SkillService(null, new NullLogger<SkillService>(), config.Object)
                {Client = client};

            // Act
            var newSkill = new SkillDTO
            {
                Id = 9, XpCost = 5, Name = "NEWSKILL"
            };
            var result = await service.CreateSkill(newSkill);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newSkill.Id, result.Id);
            Assert.AreEqual(newSkill.Name, result.Name);
            Assert.AreEqual(newSkill.XpCost, result.XpCost);
        }

        [TestMethod]
        public async Task DeleteSkillTest()
        {
            // Arrange
            var skillId = 3;
            var client = SetupMock_Skill();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhost:1111/");
            var service = new SkillService(null, new NullLogger<SkillService>(), config.Object)
                { Client = client };

            // Act
            var result = await service.DeleteSkill(skillId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(skillId, result);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            // Arrange
            var client = SetupMock_Skill();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["SkillsURL"]).Returns("https://localhost:1111/");
            var service = new SkillService(null, new NullLogger<SkillService>(), config.Object)
                { Client = client };

            // Act
            var result = await service.GetAll();

            // Assert
            foreach (var skill in result)
            {
                var testItem = TestData.Skills().FirstOrDefault(s => s.Id == skill.Id);
            }
        }
    }
}