using DTO;
using LRP.Items.Controllers;
using LRP.Items.Data.Craftables;
using LRP.Items.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Items.Tests.Controllers
{
    [TestClass]
    public class CraftablesControllerTests
    {
        private static class TestData
        {
            public static List<Craftable> Items()
            {
                return new List<Craftable>
                {
                    new Craftable
                    {
                        Id = 1, Name = "Item 1", Requirement = "Requirement 1", Materials = "Material 1",
                        Effect = "Effect 1", Form = "Form 1"
                    },
                    new Craftable
                    {
                        Id = 2, Name = "Item 2", Requirement = "Requirement 2", Materials = "Material 2",
                        Effect = "Effect 2", Form = "Form 2"
                    },
                    new Craftable
                    {
                        Id = 3, Name = "Item 3", Requirement = "Requirement 3", Materials = "Material 3",
                        Effect = "Effect 3", Form = "Form 3"
                    },
                    new Craftable
                    {
                        Id = 4, Name = "Item 4", Requirement = "Requirement 4", Materials = "Material 4",
                        Effect = "Effect 4", Form = "Form 4"
                    },
                    new Craftable
                    {
                        Id = 5, Name = "Item 5", Requirement = "Requirement 5", Materials = "Material 5",
                        Effect = "Effect 5", Form = "Form 5"
                    }
                };
            }
        }

        [TestMethod]
        public async Task GetCraftableTest()
        {
            // Arrange
            var repo = new FakeCraftableRepository(TestData.Items());
            var controller = new CraftablesController(repo);
            var itemId = 3;

            // Act
            var result = await controller.GetCraftable(itemId);

            // Assert
            Assert.IsNotNull(result);
            var testItem = TestData.Items().Find(i => i.Id == itemId);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var valResult = objResult.Value as CraftableDTO;
            Assert.IsNotNull(valResult);

            Assert.AreEqual(testItem.Id, valResult.Id);
            Assert.AreEqual(testItem.Name, valResult.Name);
            Assert.AreEqual(testItem.Requirement, valResult.Requirement);
            Assert.AreEqual(testItem.Materials, valResult.Materials);
            Assert.AreEqual(testItem.Form, valResult.Form);
            Assert.AreEqual(testItem.Effect, valResult.Effect);
        }

        [TestMethod]
        public async Task GetCraftablesTest()
        {
            // Arrange
            var repo = new FakeCraftableRepository(TestData.Items());
            var controller = new CraftablesController(repo);

            // Act
            var result = await controller.GetCraftables();

            // Assert
            Assert.IsNotNull(result);
            var list = result.Value;
            Assert.IsNotNull(list);
            Assert.AreEqual(TestData.Items().Count, list.Count());
            foreach (var item in list)
            {
                var testItem = TestData.Items().Find(i => i.Id == item.Id);
                Assert.AreEqual(testItem.Id, item.Id);
                Assert.AreEqual(testItem.Name, item.Name);
                Assert.AreEqual(testItem.Requirement, item.Requirement);
                Assert.AreEqual(testItem.Materials, item.Materials);
                Assert.AreEqual(testItem.Form, item.Form);
                Assert.AreEqual(testItem.Effect, item.Effect);
            }
        }

        [TestMethod]
        public async Task PutCraftableTest()
        {
            // Arrange
            var repo = new FakeCraftableRepository(TestData.Items());
            var controller = new CraftablesController(repo);
            var itemId = 1;
            var item = new Craftable
            {
                Id = itemId,
                Effect = "UPDATEDEFFECT",
                Form = "UPDATEDFORM",
                Materials = "UPDATEDMATERIALS",
                Name = "UPDATEDNAME",
                Requirement = "UPDATEDREQUIREMENT"
            };

            // Act
            //var prev = await controller.GetCraftable(itemId);
            var result = await controller.PutCraftable(itemId, item);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PostCraftableTest()
        {
            // Arrange
            var repo = new FakeCraftableRepository(TestData.Items());
            var controller = new CraftablesController(repo);
            var item = new Craftable
            {
                Id = 10,
                Effect = "NEWEFFECT",
                Form = "NEWFORM",
                Materials = "NEWMATERIALS",
                Name = "NEWNAME",
                Requirement = "NEWREQUIREMENT"
            };

            // Act
            var result = await controller.PostCraftable(item);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(objResult);
            var valResult = objResult.Value as Craftable;
            Assert.IsNotNull(valResult);
            Assert.AreEqual(item, valResult);
        }

        [TestMethod]
        public async Task DeleteCraftableTest()
        {
            // Arrange
            var repo = new FakeCraftableRepository(TestData.Items());
            var controller = new CraftablesController(repo);
            var charId = 3;

            // Act
            var result = await controller.DeleteCraftable(charId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var valResult = objResult.Value as Craftable;
            Assert.IsNotNull(valResult);
            Assert.AreEqual(charId, valResult.Id);
        }
    }
}