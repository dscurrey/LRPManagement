using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRP.Characters.Controllers;
using LRP.Characters.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LRP.Characters.Data.Characters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LRP.Characters.Controllers.Tests
{
    [TestClass]
    public class CharactersControllerTests
    {
        
        private static class TestData
        {
            public static List<Character> Characters() => new List<Character>
            {
                new Character { Id = 1, IsActive = true, IsRetired = false, Name = "Test Character 1", PlayerId = 1},
                new Character { Id = 2, IsActive = true, IsRetired = false, Name = "Test Character 2", PlayerId = 2},
                new Character { Id = 3, IsActive = false, IsRetired = true, Name = "Test Character 3", PlayerId = 1}
            };
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
            Assert.AreEqual(testItem.Id, result.Value.Id);
            Assert.AreEqual(testItem.IsActive, result.Value.IsActive);
            Assert.AreEqual(testItem.IsRetired, result.Value.IsRetired);
            Assert.AreEqual(testItem.Name, result.Value.Name);
            Assert.AreEqual(testItem.PlayerId, result.Value.PlayerId);
        } 


        [TestMethod]
        public async Task PostCharacterTest()
        {
            // Arrange
            var repo = new FakeCharacterRepository(TestData.Characters());
            var controller = new CharactersController(repo, null);
            var character = new Character { Id = 5, IsActive = true, IsRetired = false, PlayerId = 2, Name = "Created Character" };

            // Act
            var result = await controller.PostCharacter(character);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as Character;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(character, retResult);
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
    }
}