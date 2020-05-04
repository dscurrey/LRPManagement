using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Controllers;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LRPManagement.Tests.Controllers
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
                    new Skill {Id = 1, Name = "Skill 1", XpCost = 2, SkillRef = 1},
                    new Skill {Id = 2, Name = "Skill 2", XpCost = 3, SkillRef = 2},
                    new Skill {Id = 3, Name = "Skill 3", XpCost = 2, SkillRef = 3},
                };
            }
        }

        [TestMethod]
        public async Task IndexTest()
        {
            var mockRepo = new Mock<ISkillRepository>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(TestData.Skills);
            var mockServ = new Mock<ISkillService>();
            var controller = new SkillsController(mockServ.Object, mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            var skillId = 2;
            var mockRepo = new Mock<ISkillRepository>();
            mockRepo.Setup(repo => repo.GetSkill(skillId)).ReturnsAsync(TestData.Skills().FirstOrDefault(s => s.Id == skillId));
            var mockServ = new Mock<ISkillService>();
            var controller = new SkillsController(mockServ.Object, mockRepo.Object);

            // Act
            var result = await controller.Details(skillId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model;
            Assert.IsNotNull(model);
            var skill = model as Skill;
            Assert.IsNotNull(skill);

            var testSkill = TestData.Skills().FirstOrDefault(s => s.Id == skillId);
            Assert.AreEqual(testSkill.Id, skill.Id);
            Assert.AreEqual(testSkill.Name, skill.Name);
            Assert.AreEqual(testSkill.XpCost, skill.XpCost);
            Assert.AreEqual(testSkill.SkillRef, skill.SkillRef);
        }
    }
}