using DTO;
using LRP.Characters.Controllers;
using LRP.Characters.Data.Characters;
using LRP.Characters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Characters.Tests.Controllers
{
    [TestClass]
    public class CharactersControllerTests
    {
        private static class TestData
        {
            public static List<Character> Characters()
            {
                return new List<Character>
                {
                    new Character {Id = 1, IsActive = true, IsRetired = false, Name = "Test Character 1", PlayerId = 1},
                    new Character {Id = 2, IsActive = true, IsRetired = false, Name = "Test Character 2", PlayerId = 2},
                    new Character {Id = 3, IsActive = false, IsRetired = true, Name = "Test Character 3", PlayerId = 1}
                };
            }
        }

        [TestMethod]
        public async Task GetCharacterTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var charId = 1;

            // Act
            var result = await controller.GetCharacter(charId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Characters().Find(c => c.Id == charId);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as CharacterDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(testItem.Id, retResult.Id);
            Assert.AreEqual(testItem.IsActive, retResult.IsActive);
            Assert.AreEqual(testItem.IsRetired, retResult.IsRetired);
            Assert.AreEqual(testItem.Name, retResult.Name);
            Assert.AreEqual(testItem.PlayerId, retResult.PlayerId);
        }

        [TestMethod]
        public async Task GetCharacterNotFoundTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var charId = 100;

            // Act
            var result = await controller.GetCharacter(charId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task GetAllCharactersTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);

            // Act
            var result = await controller.GetCharacter();

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as List<CharacterDTO>;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(TestData.Characters().Count, retResult.Count);
            foreach (var character in retResult)
            {
                var testItem = TestData.Characters().Find(c => c.Id == character.Id);
                Assert.AreEqual(testItem.IsActive, character.IsActive);
                Assert.AreEqual(testItem.Name, character.Name);
                Assert.AreEqual(testItem.PlayerId, character.PlayerId);
            }
        }

        [TestMethod]
        public async Task PostCharacterTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var character = new CharacterDTO
                {Id = 5, IsActive = true, IsRetired = false, PlayerId = 2, Name = "Created Character"};

            // Act
            var result = await controller.PostCharacter(character);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as CharacterDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(character, retResult);
        }

        [TestMethod]
        public async Task PostCharacterNoIdTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var character = new CharacterDTO
                {Id = 5, IsActive = true, IsRetired = false, PlayerId = 2, Name = "Created Character"};

            // Act
            var result = await controller.PostCharacter(character);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as CharacterDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(character, retResult);
        }

        [TestMethod]
        public async Task PostCharacterInvalidTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var character = new CharacterDTO {Id = 5, IsActive = true, IsRetired = false, PlayerId = 2, Name = ""};

            // Act
            var result = await controller.PostCharacter(character);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task DeleteCharacterTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var charId = 1;

            // Act
            var result = await controller.DeleteCharacter(charId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(charId, result.Value.Id);
        }

        [TestMethod]
        public async Task DeleteCharacterNotFoundTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var charId = 100;

            // Act
            var result = await controller.DeleteCharacter(charId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PutCharacterTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, NullLogger<CharactersController>.Instance);
            var charId = 1;
            var character = new CharacterDTO
            {
                Id = charId,
                IsRetired = true,
                IsActive = false,
                Name = "NAME",
                PlayerId = 1
            };

            // Act
            var result = await controller.PutCharacter(charId, character);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutCharacterNoMatchIdTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, NullLogger<CharactersController>.Instance);
            var charId = 1;
            var character = new CharacterDTO
            {
                Id = charId + 1,
                IsRetired = true,
                IsActive = false,
                Name = "NAME",
                PlayerId = 1
            };

            // Act
            var result = await controller.PutCharacter(charId, character);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PutCharacterNotFoundTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, NullLogger<CharactersController>.Instance);
            var charId = 100;
            var character = new CharacterDTO
            {
                Id = charId,
                IsRetired = true,
                IsActive = false,
                Name = "NAME",
                PlayerId = 1
            };

            // Act
            var result = await controller.PutCharacter(charId, character);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }
    }
}