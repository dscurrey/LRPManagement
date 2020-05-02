using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRP.Items.Controllers;
using LRP.Items.Data.Bonds;
using LRP.Items.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LRP.Items.Tests.Controllers
{
    [TestClass]
    public class BondsControllerTests
    {
        private static class TestData
        {
            public static List<Bond> Bonds()
            {
                return new List<Bond>
                {
                    new Bond {Id = 1, ItemId = 1, CharacterId = 1},
                    new Bond {Id = 2, ItemId = 2, CharacterId = 1},
                    new Bond {Id = 3, ItemId = 3, CharacterId = 1},
                    new Bond {Id = 4, ItemId = 1, CharacterId = 2},
                    new Bond {Id = 5, ItemId = 5, CharacterId = 2},
                };
            }
        }

        [TestMethod]
        public async Task GetBondsTest()
        {
            // Arrange
            var mockRepo = new Mock<IBondRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(Task.FromResult(TestData.Bonds()));
            var controller = new BondsController(mockRepo.Object);

            // Act
            var result = await controller.GetBonds();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            var objResult = result.Value;
            foreach (var bond in objResult)
            {
                var testBond = TestData.Bonds().FirstOrDefault(b => b.Id == bond.Id);
                Assert.AreEqual(testBond.ItemId, bond.ItemId);
                Assert.AreEqual(testBond.CharacterId, bond.CharacterId);
            }
        }

        [TestMethod]
        public async Task GetBondTest()
        {
            // Arrange
            var mockRepo = new Mock<IBondRepository>();
            var bondId = 3;
            mockRepo.Setup(repo => repo.Get(bondId))
                .Returns(Task.FromResult(TestData.Bonds().FirstOrDefault(b => b.Id == bondId)));
            var controller = new BondsController(mockRepo.Object);

            // Act
            var result = await controller.GetBond(bondId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            var bond = result.Value;
            var testItem = TestData.Bonds().FirstOrDefault(b => b.Id == bondId);
            Assert.AreEqual(testItem.CharacterId, bond.CharacterId);
            Assert.AreEqual(testItem.ItemId, bond.ItemId);
        }

        [TestMethod]
        public void PostBondTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteBondTest()
        {
            throw new NotImplementedException();
        }
    }
}