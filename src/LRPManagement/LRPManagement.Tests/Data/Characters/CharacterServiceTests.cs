using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DTO;
using LRPManagement.Data.Characters;
using LRPManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace LRPManagement.Tests.Data.Characters
{
    [TestClass]
    public class CharacterServiceTests
    {
        private static class TestData
        {
            public static List<Character> Characters() => new List<Character>
            {
                new Character {Id = 1, Name = "Character 1", IsActive = true, IsRetired = false, CharacterRef = 2, PlayerId = 1, Xp = 8},
                new Character {Id = 2, IsActive = true, IsRetired = false, Xp = 8, CharacterRef = 1, Name = "Character 2", PlayerId = 2},
                new Character {Id = 3, IsActive = true, IsRetired = false, Xp = 8, CharacterRef = 3, Name = "Character 3", PlayerId = 2}
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

        private HttpClient SetupMock_Character(int id)
        {
            var expectedHttpResp = TestData.Characters().FirstOrDefault(b => b.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_Character()
        {
            var expectedHttpResp = TestData.Characters();
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            // Arrange
            var client = SetupMock_Character();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["ItemsURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>());
        }

        [TestMethod]
        public async Task GetAllCharacterTest()
        {
            // Arrange
            var client = SetupMock_Character();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["CharactersURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>())
                {Client = client};

            // Act
            var result = await service.GetAll();

            // Assert
            foreach (var character in result)
            {
                var testItem = TestData.Characters().FirstOrDefault(c => c.Id == character.Id);
                Assert.AreEqual(testItem.Id, character.Id);
                Assert.AreEqual(testItem.Name, character.Name);
                Assert.AreEqual(testItem.IsActive, character.IsActive);
                Assert.AreEqual(testItem.IsRetired, character.IsRetired);
                Assert.AreEqual(testItem.PlayerId, character.PlayerId);
                Assert.AreEqual(testItem.Xp, character.Xp);
            }
        }

        [TestMethod]
        public async Task GetCharacterTest()
        {
            // Arrange
            var charId = 2;
            var client = SetupMock_Character(charId);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["CharactersURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>())
                { Client = client };

            // Act
            var result = await service.GetCharacter(charId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Characters().FirstOrDefault(c => c.Id == charId);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.AreEqual(testItem.Name, result.Name);
            Assert.AreEqual(testItem.IsActive, result.IsActive);
            Assert.AreEqual(testItem.IsRetired, result.IsRetired);
            Assert.AreEqual(testItem.PlayerId, result.PlayerId);
            Assert.AreEqual(testItem.Xp, result.Xp);
        }

        [TestMethod]
        public async Task UpdateCharacterTest()
        {
            // Arrange
            var client = SetupMock_Character();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["CharactersURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>())
                { Client = client };

            // Act
            var updChar = new CharacterDTO
            {
                Id = 1, Name = "Character 1", IsActive = true, IsRetired = false, PlayerId = 1, Xp = 8
            };
            var result = await service.UpdateCharacter(updChar);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updChar.Id, result.Id);
            Assert.AreEqual(updChar.Name, result.Name);
            Assert.AreEqual(updChar.IsActive, result.IsActive);
            Assert.AreEqual(updChar.IsRetired, result.IsRetired);
            Assert.AreEqual(updChar.PlayerId, result.PlayerId);
            Assert.AreEqual(updChar.Xp, result.Xp);
        }

        [TestMethod]
        public async Task UpdateCharacterObjTest()
        {
            // Arrange
            var client = SetupMock_Character();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111/");
            config.SetupGet(s => s["CharactersURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>())
                { Client = client };

            // Act
            var updChar = new Character
            {
                Id = 1,
                Name = "Character 1",
                IsActive = true,
                IsRetired = false,
                PlayerId = 1,
                CharacterRef = 3,
                Xp = 8
            };
            var result = await service.UpdateCharacter(updChar);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updChar.Name, result.Name);
            Assert.AreEqual(updChar.IsActive, result.IsActive);
            Assert.AreEqual(updChar.IsRetired, result.IsRetired);
            Assert.AreEqual(updChar.PlayerId, result.PlayerId);
            Assert.AreEqual(updChar.Xp, result.Xp);
        }

        [TestMethod]
        public async Task CreateCharacterTest()
        {
            // Arrange
            var client = SetupMock_Character();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["CharactersURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>())
                {Client = client};

            // Act
            var newChar = new CharacterDTO
            {
                Id = 5,
                Name = "Character 5",
                IsActive = true,
                IsRetired = false,
                PlayerId = 1,
                Xp = 8
            };
            var result = await service.CreateCharacter(newChar);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newChar.Id, result.Id);
            Assert.AreEqual(newChar.Name, result.Name);
            Assert.AreEqual(newChar.IsActive, result.IsActive);
            Assert.AreEqual(newChar.IsRetired, result.IsRetired);
            Assert.AreEqual(newChar.PlayerId, result.PlayerId);
            Assert.AreEqual(newChar.Xp, result.Xp);
        }

        [TestMethod]
        public async Task DeleteCharacterTest()
        {
            // Arrange
            var charId = 3;
            var client = SetupMock_Character();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("https://localhost:1111");
            config.SetupGet(s => s["CharactersURL"]).Returns("https://localhost:1111/");
            var service = new CharacterService(null, config.Object, new NullLogger<CharacterService>())
                { Client = client };

            // Act
            var result = await service.DeleteCharacter(charId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(charId, result);
        }
    }
}