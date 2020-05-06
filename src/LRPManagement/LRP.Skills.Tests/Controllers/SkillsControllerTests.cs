using DTO;
using LRP.Skills.Controllers;
using LRP.Skills.Data.Skills;
using LRP.Skills.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Skills.Tests.Controllers
{
    [TestClass]
    public class SkillsControllerTests
    {
        private static class TestData
        {
            public static List<Skill> Skills()
            {
                return new List<Skill>
                {
                    new Skill {Id = 1, Name = "Test Skill 1"},
                    new Skill {Id = 2, Name = "Test Skill 2"},
                    new Skill {Id = 3, Name = "Test Skill 3"}
                };
            }
        }

        [TestMethod]
        public async Task GetAllSkillsTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);

            // Act
            var result = await controller.GetSkill();

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as List<SkillDTO>;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(TestData.Skills().Count, retResult.Count);
            foreach (var skill in retResult)
            {
                var testItem = TestData.Skills().Find(s => s.Id == skill.Id);
                Assert.AreEqual(testItem.Name, skill.Name);
            }
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
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as SkillDTO;
            Assert.IsNotNull(retResult);
            Assert.AreEqual(testItem.Id, retResult.Id);
            Assert.AreEqual(testItem.Name, retResult.Name);
        }

        [TestMethod]
        public async Task GetSkillNotFoundTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skillId = 76;

            // Act
            var result = await controller.GetSkill(skillId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as NotFoundResult;
            Assert.IsNotNull(objResult);
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
        public async Task PostSkillNoIdTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skill = new SkillDTO {Name = "TestSkill"};

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
        public async Task PostSkillInvalidNameTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skill = new SkillDTO {Id = 4, Name = ""};

            // Act
            var result = await controller.PostSkill(skill);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task DeleteSkillTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skillId = 1;

            // Act
            var result = await controller.DeleteSkill(skillId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(skillId, result.Value.Id);
        }

        [TestMethod]
        public async Task DeleteSkillNotFoundTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, null);
            var skillId = 74;

            // Act
            var result = await controller.DeleteSkill(skillId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PutSkillTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, NullLogger<SkillsController>.Instance);
            var skillId = 1;
            var skill = new SkillDTO
            {
                Id = skillId,
                Name = "Edited Skill"
            };

            // Act
            var result = await controller.PutSkill(skillId, skill);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutSkillNoMatchIdTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, NullLogger<SkillsController>.Instance);
            var skillId = 1;
            var skill = new SkillDTO
            {
                Id = skillId + 1,
                Name = "Edited Skill"
            };

            // Act
            var result = await controller.PutSkill(skillId, skill);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as BadRequestResult;
            Assert.IsNotNull(objResult);
        }

        [TestMethod]
        public async Task PutSkillNotFoundTest()
        {
            // Arrange
            var repo = new FakeSkillRepository(TestData.Skills());
            var controller = new SkillsController(repo, NullLogger<SkillsController>.Instance);
            var skillId = 72;
            var skill = new SkillDTO
            {
                Id = skillId,
                Name = "Edited Skill"
            };

            // Act
            var result = await controller.PutSkill(skillId, skill);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result as NotFoundResult;
            Assert.IsNotNull(objResult);
        }
    }
}