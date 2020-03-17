using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRP.Skills.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTO;
using LRP.Skills.Data.Skills;
using LRP.Skills.Models;
using Microsoft.AspNetCore.Mvc;

namespace LRP.Skills.Controllers.Tests
{
    [TestClass]
    public class SkillsControllerTests
    {
        private static class TestData
        {
            public static List<Skill> Skills() => new List<Skill>
            {
                new Skill { Id = 1, Name = "Test Skill 1"},
                new Skill { Id = 2, Name = "Test Skill 2"},
                new Skill { Id = 3, Name = "Test Skill 3"}
            };
        }

        [TestMethod]
        public async Task GetSkillTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skillId = 1;

            // Act
            var result = await controller.GetSkill(skillId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Skills().Find(s => s.Id == skillId);
            Assert.AreEqual(testItem.Id, result.Value.Id);
            Assert.AreEqual(testItem.Name, result.Value.Name);
        }

        [TestMethod]
        public async Task PostSkillTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skill = new SkillDTO {Id = 4, Name = "TestSkill"};

            // Act
            var result = await controller.PostSkill(skill);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as SkillDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(skill, retResult);
        }

        [TestMethod]
        public async Task DeleteSkillTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skillId = 1;

            // Act
            var result = await controller.DeleteSkill(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(skillId, result.Value.Id);
        }
    }
}